using AntdUI;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using uChat_Desktop.DataModel;
using uChat_Desktop.Extention;
using uChat_Desktop.Lang;
using uChat_Desktop.MainApp;

namespace uChat_Desktop.Service {
    public interface ChatService {
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
        private static TaskCompletionSource<ChatWSResult?>? _pendingResponse;

        // this should only be called once when connected
        public static void StartChatReceiveLoop() {
            var ws = ApiService.ChatWebSocket;

            if (ws == null || ws.State != WebSocketState.Open)
                return;

            _ = ReceiveLoop(ws);
        }

        private static async Task ReceiveLoop(ClientWebSocket ws) {
            try {
                while (ws.State == WebSocketState.Open) {
                    var buffer = new byte[8192];
                    using var ms = new MemoryStream();

                    WebSocketReceiveResult result;
                    bool isText = true;

                    do {
                        result = await ws.ReceiveAsync(
                            new ArraySegment<byte>(buffer),
                            CancellationToken.None
                        );

                        if (result.MessageType == WebSocketMessageType.Close)
                            return;

                        if (result.MessageType != WebSocketMessageType.Text)
                            isText = false;
                        else if (isText)
                            ms.Write(buffer, 0, result.Count);

                    } while (!result.EndOfMessage);

                    if (!isText)
                        continue;

                    var response = JsonSerializer.Deserialize<ChatWSResult>(
                        ms.ToArray()
                    );

                    _pendingResponse?.TrySetResult(response);
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
            }
            finally {
                _pendingResponse?.TrySetResult(null);
            }
        }

        private static async Task<ChatWSResult?> SendRequest(ChatWSRequest request) {

            await _wsLock.WaitAsync();

            TaskCompletionSource<ChatWSResult?>? tcs = null;

            try {
                var ws = ApiService.ChatWebSocket;

                if (ws == null || ws.State != WebSocketState.Open)
                    return null;

                tcs = new TaskCompletionSource<ChatWSResult?>(
                    TaskCreationOptions.RunContinuationsAsynchronously
                );

                _pendingResponse = tcs;

                var bytes = Encoding.UTF8.GetBytes(
                    JsonSerializer.Serialize(request)
                );

                await ws.SendAsync(
                    new ArraySegment<byte>(bytes),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );

                return await tcs.Task.WaitAsync(
                    TimeSpan.FromSeconds(15)
                );
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                return null;
            }
            finally {
                if (ReferenceEquals(_pendingResponse, tcs))
                    _pendingResponse = null;

                _wsLock.Release();
            }
        }

        public static Task<ChatWSResult?> LoadHistory(Guid chatId) {
            return SendRequest(new ChatWSRequest {
                AccountId = AccountService.LoggedAccount!.AccountId,
                ChatId = chatId,
                RequestType = "open_chat"
            });
        }

        public static Task<ChatWSResult?> SendMessage(Guid chatId, Guid memberId, Guid messageId, string textContent) {
            return SendRequest(new ChatWSRequest {
                AccountId = AccountService.LoggedAccount!.AccountId,
                ChatId = chatId,
                ChatMemberId = memberId,
                MessageId = messageId,
                RequestType = "send_message",
                MessageContext = textContent
            });
        }

        private static DateTimeOffset _LastChatRequestTime = DateTimeOffset.Now;
        public static async Task<ChatWSResult?> GetMessages(Guid? chatId, Guid? memberId) {
            var response = await SendRequest(new ChatWSRequest {
                AccountId = AccountService.LoggedAccount!.AccountId,
                ChatId = chatId,
                ChatMemberId = memberId,
                RequestType = "get_update",
                LastReadTime = _LastChatRequestTime
            });

            if (response?.Success == true)
                _LastChatRequestTime = DateTimeOffset.Now;

            return response;
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
