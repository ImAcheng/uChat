using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using uChat_API.Controllers;
using uChat_API.DbModel;

namespace uChat_API.Websocket {
    /// <summary>
    /// This handler mainly process accounts' real-time status
    /// </summary>
    public class AccountHandler {
        private readonly IServiceScopeFactory _scopeFactory;

        public AccountHandler(IServiceScopeFactory scope) {
            _scopeFactory = scope;
        }

        public async Task HandleAsync(HttpContext context) {
            if (!context.WebSockets.IsWebSocketRequest) {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("CONN_TYPE_WRONG: Must be websocket.");
                return;
            }

            using var websocket = await context.WebSockets.AcceptWebSocketAsync();

            var buffer = new byte[1024*8];

            Console.WriteLine("[Debug] A client connected to Account Websocket.");

            try {
                while (websocket.State == WebSocketState.Open) {
                    var result = await websocket.ReceiveAsync(buffer, CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close) {
                        await websocket.CloseAsync(
                                WebSocketCloseStatus.NormalClosure,
                                "CONN_CLOSED",
                                CancellationToken.None
                        );

                        Console.WriteLine("[Debug] A client disconnected from Account Websocket.");

                        break;
                    }

                    if (result.MessageType != WebSocketMessageType.Text) continue;

                    var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    await ProcessTask(websocket, json);
                }
            }
            catch (WebSocketException) {
                Console.WriteLine("[Debug] A client disconnected from Account Websocket (Forced).");
            }
            catch (OperationCanceledException) {
                Console.WriteLine("[Debug] A client disconnected from Account Websocket (Forced).");
            }
        }

        private async Task ProcessTask(WebSocket ws, string json) {
            // define request as null to prevent intellisence from annoying :D
            AccountWSRequest? request = null;

            try {
                request = JsonSerializer.Deserialize<AccountWSRequest>(json);

                if (request == null || string.IsNullOrWhiteSpace(request.RequestType))
                    throw new JsonException();
            }
            catch (JsonException) {
                await SendDataBack(ws, new { success = false, message = "DATA_FAIL" });
                return;
            }
            catch (Exception ex) {
                await SendDataBack(ws, new { success = false, message = $"UNKNOWN:{ex.Message}" });
                return;
            }

            await using var scope = _scopeFactory.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<UChatDbContext>();

            var acc = await db.Account.FirstOrDefaultAsync(u => u.AccountId == request.AccountId);

            if (acc == null) {
                await SendDataBack(ws, new { success = false, message = AccountMessage.AccountNotFound });
                return;
            }

            switch (request.RequestType) {
                case "online_update":
                    acc.LastOnlineDateTime = DateTimeOffset.Now;
                    await db.SaveChangesAsync();
                    await SendDataBack(ws, new { success = true, message = AccountMessage.Success });
                    break;

                default:
                    await SendDataBack(ws, new { success = false, message = "UNKNOWN_TASK" });
                    break;
            }
        }

        /// <summary>
        ///     Send data back to client.
        /// </summary>
        private static async Task SendDataBack(WebSocket ws, object data) {
            var json = JsonSerializer.Serialize(data);
            var bytes = Encoding.UTF8.GetBytes(json);

            await ws.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }

    public class AccountWSRequest {
        public Guid AccountId { get; set; }
        public string RequestType { get; set; } = string.Empty;

        // use data below depends on what clients request
        public DateTimeOffset? RequestTime { get; set; }
    }
}
