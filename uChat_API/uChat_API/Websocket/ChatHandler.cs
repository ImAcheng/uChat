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
                    await ReadHistory(ws, db, request);
                    break;

                case "get_update":
                    await GetUpdates(ws, db, request);
                    break;

                case "get_olds":
                    // this is planned
                    // which means, the api return the whole chat at once now :D
                    break;

                case "send_message":
                    await SendMessage(ws, db, request);
                    break;

                case "del_message":
                    break;

                default:
                    await SendDataBack(ws, new ChatWSResult() {
                        Success = true,
                        Action = ChatWSActionType.ProProcessing
                    });
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
        private async Task ReadHistory(WebSocket ws, UChatDbContext db, ChatWSRequest data) {
            var result = await db.ChatMessage
                    .AsNoTracking()
                    .Where(x => x.ChatRoomId == data.ChatId)
                    .OrderBy(x => x.CreateDateTime)
                    .Select(x => new MessageDTO(x, x.ChatRoomMember.Account.Username))
                    .ToListAsync();

            await SendDataBack(ws, new ChatWSResult() {
                Success = true,
                Data = result
            });
        }

        private async Task SendMessage(WebSocket ws, UChatDbContext db, ChatWSRequest data) {
            var msg = new ChatMessage() {
                ChatMessageId = (Guid)data.MessageId!,
                ChatRoomMemberId = data.ChatMemberId,
                AffiliatedType = 0,
                ChatRoomId = (Guid)data.ChatId!,
                TextContent = data.MessageContext,
                CreateDateTime = DateTimeOffset.Now
            };

            try {
                await using var trans = await db.Database.BeginTransactionAsync();

                await db.ChatMessage.AddAsync(msg);
                await db.SaveChangesAsync();

                await db.ChatRoom
                    .Where(x => x.ChatRoomId == msg.ChatRoomId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(x => x.UpdateDateTime, DateTimeOffset.Now));

                await db.SaveChangesAsync();
                await trans.CommitAsync();
            }
            catch {
                await db.Database.RollbackTransactionAsync();

                await SendDataBack(ws, new ChatWSResult() {
                    Success = false,
                    Message = "DB_SAVE_FAILED",
                    Action = ChatWSActionType.SendMessage
                });

                return;
            }

            var sender = msg.ChatRoomMember?.Account.Username ?? "DATA_LOST";

            try {
                await SendDataBack(ws, new ChatWSResult() {
                    Success = true,
                    Action = ChatWSActionType.SendMessage
                });
            }
            catch {
                await SendDataBack(ws, new ChatWSResult() {
                    Success = false,
                    Message = "SEND_FAILED",
                    Action = ChatWSActionType.SendMessage
                });
            }
        }

        private async Task GetUpdates(WebSocket ws, UChatDbContext db, ChatWSRequest data) {
            // when ChatID & ChatMemberId is null
            // it means user is not in a chat (technically)
            // so we only return new message data
            // otherwise we return all stuff

            var newMessages = await db.ChatMessage
                    .AsNoTracking()
                    .Where(x =>
                            x.ChatRoom.ChatRoomMembers.Any(m => m.AccountId == data.AccountId) &&
                            x.CreateDateTime > data.LastReadTime)
                    .Select(x => new MessageDTO(x, x.ChatRoomMember.Nickname ?? x.ChatRoomMember.Account.Username))
                    .OrderBy(x => x.CreateDateTime)
                    .ToListAsync();

            await SendDataBack(ws, new ChatWSResult() {
                Success = true,
                Action = ChatWSActionType.NewMessages,
                Data = newMessages
            });

            if (data.ChatId == null) return;

            var newUpdates = await db.ChatMessage
                .AsNoTracking()
                .Where(x =>
                        x.ChatRoomId == data.ChatId &&
                        x.UpdateDateTime > data.LastReadTime &&
                        x.ModifyStatus != 0)
                .Select(x => new MessageDTO(x, x.ChatRoomMember.Nickname ?? x.ChatRoomMember.Account.Username))
                .ToListAsync();

            await SendDataBack(ws, new ChatWSResult() {
                Success = true,
                Action = ChatWSActionType.UpdateMessages,
                Data = newUpdates
            });
        }
        #endregion
    }

    public class ChatWSRequest {
        public Guid AccountId { get; set; }
        public Guid? ChatId { get; set; }
        public Guid? ChatMemberId { get; set; }
        public Guid? MessageId { get; set; }
        public string RequestType { get; set; } = string.Empty;

        public string? MessageContext { get; set; }
        public DateTimeOffset? LastReadTime { get; set; }
    }

    public class ChatWSResult {
        public bool Success { get; set; }
        public string Message { get; set; } = "OK";
        public ChatWSActionType Action { get; set; }
        public List<MessageDTO>? Data { get; set; }
    }

    public enum ChatWSActionType {
        SendMessage,
        NewMessages,
        UpdateMessages,
        ProProcessing
    }
}
