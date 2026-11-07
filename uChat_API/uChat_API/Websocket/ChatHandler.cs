using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using uChat_API.Controllers;
using uChat_API.DbModel;
using Microsoft.EntityFrameworkCore;

namespace uChat_API.Websocket {
    public class ChatHandler {
        private readonly IServiceScopeFactory _scopeFactory;

        public ChatHandler(IServiceScopeFactory scope) {
            _scopeFactory = scope;
        }

        public async Task HandleAsync(HttpContext context) {
            if (!context.WebSockets.IsWebSocketRequest) {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("CONN_TYPE_WRONG: Must be websocket.");
                return;
            }

            using var websocket = await context.WebSockets.AcceptWebSocketAsync();

            var buffer = new byte[1024 * 8];

            Console.WriteLine("[Debug] A client connected to Chat Websocket.");

            try {
                while (websocket.State == WebSocketState.Open) {
                    var result = await websocket.ReceiveAsync(buffer, CancellationToken.None);

                    if (result.MessageType == WebSocketMessageType.Close) {
                        await websocket.CloseAsync(
                                WebSocketCloseStatus.NormalClosure,
                                "CONN_CLOSED",
                                CancellationToken.None
                        );

                        Console.WriteLine("[Debug] A client disconnected from Chat Websocket.");

                        break;
                    }

                    if (result.MessageType != WebSocketMessageType.Text) continue;

                    var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    await ProcessTask(websocket, json);
                }
            }
            catch (WebSocketException) {
                Console.WriteLine("[Debug] A client disconnected from Chat Websocket (Forced).");
            }
            catch (OperationCanceledException) {
                Console.WriteLine("[Debug] A client disconnected from Chat Websocket (Forced).");
            }
        }

        private async Task ProcessTask(WebSocket ws, string json) {
            // define request as null to prevent intellisence from annoying :D
            ChatWSRequest? request = null;

            try {
                request = JsonSerializer.Deserialize<ChatWSRequest>(json);

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
                case "open_chat":
                    await OpenChat(ws, db, request.AccountId, request.ChatId);
                    break;

                case "read_new_messages":
                    break;

                case "read_older_messages":
                    break;

                case "send_message":
                    break;

                case "del_message":
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

        #region Functions
        private async Task OpenChat(WebSocket ws, UChatDbContext db, Guid AccountId, Guid ChatId) {
            var chatMb = await db.ChatRoomMember
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ChatRoomId == ChatId && r.AccountId == AccountId);

            if (chatMb == null) {
                await SendDataBack(ws, new { success = false, message = "NO_ACCESS" });
                return;
            }

            var chat = await db.ChatRoom.AsNoTracking().FirstOrDefaultAsync(c => c.ChatRoomId == ChatId);
            
        }
        #endregion
    }

    public class ChatWSRequest {
        public Guid AccountId { get; set; }
        public Guid ChatId { get; set; }
        public Guid MessageId { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public string? MessageContext { get; set; }
    }

    public class MessageDTO {
        public long MessageNo { get; set; }
        public string SenderNickname { get; set; } = "User";
        public Guid? SenderAvatarId { get; set; }   // client get this through FileController
        public string? TextContent { get; set; }
        public List<MessageAttachmentDTO>? Attachments { get; set; }
    }

    public class MessageAttachmentDTO {
        public Guid FileDataId { get; set; }
        public byte FileType { get; set; }
        public byte[]? Thumbnail { get; set; }

        // if the link wasn't generated, then redirect user to rick roll anyways
        public string DownloadLink { get; set; } = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";   
    }
}
