using AntdUI;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using uChat_Desktop.DataModel;
using uChat_Desktop.Extention;
using uChat_Desktop.Lang;
using uChat_Desktop.MainApp;

namespace uChat_Desktop.Service {
    internal interface ChatService {
        public static async Task<GetChatResult?> GetChats() {
            try {
                return await ApiService.ApiClient
                    .GetFromJsonAsync<GetChatResult>($"api/chat/get-chats/{AccountService.LoggedAccount!.AccountId}");
            }
            catch (HttpRequestException) {
                uMessageBox.Show(
                    FormMain.instance,
                    Langs.network_problem,
                    TType.Error);
                return null;
            }
            catch (Exception ex) {
                uMessageBox.Show(
                    FormMain.instance,
                    Langs.unknown_problem + ex.Message,
                    TType.Error);
                return null;
            }
        }

        /// <summary>
        /// Return GeneralChatResult when api & internet are working. Otherwise, null.
        /// </summary>
        public static async Task<GeneralChatResult?> OpenChatFromUser(Guid target) {
            try {
                var resp = await ApiService.ApiClient.PostAsJsonAsync("api/chat/open-by-user",
                    new GeneralChatRequest() {
                        Requester = AccountService.LoggedAccount!.AccountId,
                        Target = target,
                    }
                );

                return await resp.Content.ReadFromJsonAsync<GeneralChatResult>();
            }
            catch (HttpRequestException) {
                uMessageBox.Show(
                    FormMain.instance,
                    Langs.network_problem,
                    TType.Error);
                return null;
            }
            catch (Exception ex) {
                uMessageBox.Show(FormMain.instance, "Unknown problem occurred. Please report it to use.\n" +
                    $"Exception: {ex.Message}", AntdUI.TType.Error);
                return null;
            }
        }

        private static readonly SemaphoreSlim _wsLock = new(1, 1);

        public static async Task<ChatWSResult?> LoadHistory(Guid chatId) {
            await _wsLock.WaitAsync();

            try {
                while (ApiService.ChatWebSocket == null)
                    await Task.Delay(100);

                var ws = ApiService.ChatWebSocket;

                if (ws.State != WebSocketState.Open)
                    return null;

                var rq = new ChatWSRequest() {
                    AccountId = AccountService.LoggedAccount!.AccountId,
                    ChatId = chatId,
                    RequestType = "open_chat"
                };

                var json = JsonSerializer.Serialize(rq);
                var bytes = Encoding.UTF8.GetBytes(json);

                await ws.SendAsync(
                    new ArraySegment<byte>(bytes),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );

                using var ms = new MemoryStream();
                var buffer = new byte[8192];

                WebSocketReceiveResult result;

                do {
                    result = await ws.ReceiveAsync(
                        new ArraySegment<byte>(buffer),
                        CancellationToken.None
                    );

                    if (result.MessageType != WebSocketMessageType.Text)
                        return null;

                    ms.Write(buffer, 0, result.Count);

                } while (!result.EndOfMessage);

                return JsonSerializer.Deserialize<ChatWSResult>(
                    ms.ToArray()
                );
            }
            catch {
                return null;
            }
            finally {
                _wsLock.Release();
            }
        }

        /// <summary>
        /// Send message by websocket.
        /// </summary>
        public static async Task<bool> SendMessage(Guid chatId, Guid memberId, Guid messageId, string textContent) {
            var rq = new ChatWSRequest() {
                AccountId = AccountService.LoggedAccount!.AccountId,
                ChatId = chatId,
                ChatMemberId = memberId,
                MessageId = messageId,
                RequestType = "send_message",
                MessageContext = textContent
            };

            var json = JsonSerializer.Serialize(rq);
            var bytes = Encoding.UTF8.GetBytes(json);

            try {
                while (ApiService.ChatWebSocket == null)
                    await Task.Delay(100);

                await ApiService.ChatWebSocket!.SendAsync(
                    new ArraySegment<byte>(bytes),
                    WebSocketMessageType.Text,
                    true, CancellationToken.None
                );

                var ws = ApiService.ChatWebSocket;

                using var ms = new MemoryStream();
                var buffer = new byte[8192];

                WebSocketReceiveResult result;

                do {
                    result = await ws.ReceiveAsync(
                        new ArraySegment<byte>(buffer),
                        CancellationToken.None
                    );

                    if (result.MessageType != WebSocketMessageType.Text)
                        return false;

                    ms.Write(buffer, 0, result.Count);

                } while (!result.EndOfMessage);

                return true;
            }
            catch {
                return false;
            }
        }

        private static DateTimeOffset _LastChatRequestTime = DateTimeOffset.Now;
        public async Task<MessageResult?> GetMessages(Guid? chatId, Guid? memberId) {
            var rq = new ChatWSRequest() {
                AccountId = AccountService.LoggedAccount!.AccountId,
                ChatId = chatId,
                ChatMemberId = memberId,
                RequestType = "get_update",
                LastReadTime = _LastChatRequestTime
            };

            var json = JsonSerializer.Serialize(rq);
            var bytes = Encoding.UTF8.GetBytes(json);

            try {
                await ApiService.ChatWebSocket!.SendAsync(
                    new ArraySegment<byte>(bytes),
                    WebSocketMessageType.Text,
                    true, CancellationToken.None
                );

                var buffer = new byte[1024*16];

                var result = await ApiService.ChatWebSocket!.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    CancellationToken.None
                );

                if (result.MessageType != WebSocketMessageType.Text)
                    return null;

                var responseJson = Encoding.UTF8.GetString(buffer, 0, result.Count);

                return JsonSerializer.Deserialize<MessageResult>(responseJson);
            }
            catch {
                return null;
            }
        }
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
        DeleteMessages
    }
}
