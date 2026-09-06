using Microsoft.EntityFrameworkCore;
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

            while (websocket.State == System.Net.WebSockets.WebSocketState.Open) {
                var result = await websocket.ReceiveAsync(buffer, CancellationToken.None);

                if (result.MessageType == System.Net.WebSockets.WebSocketMessageType.Close) {
                    await websocket.CloseAsync(
                            System.Net.WebSockets.WebSocketCloseStatus.NormalClosure,
                            "CONN_CLOSED",
                            CancellationToken.None
                        );
                }

                if (result.MessageType != System.Net.WebSockets.WebSocketMessageType.Text) continue;

                var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

                await ProcessTask(websocket, json);
            }
        }

        private async Task ProcessTask(WebSocket ws, string json) {
            // define request as null to prevent compiler from annoying :D
            AccountRequest? request = null;

            try {
                request = JsonSerializer.Deserialize<AccountRequest>(json);

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
                    acc.LastOnlineDateTime = request.RequestTime;
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

    public class AccountRequest {
        public Guid AccountId { get; set; }
        public string RequestType { get; set; } = string.Empty;

        // use data below depends on what clients request
        public DateTimeOffset? RequestTime { get; set; }
    }
}
