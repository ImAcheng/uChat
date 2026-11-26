using System.Net.WebSockets;
using System.Diagnostics;

namespace uChat_Desktop.Service {
    internal interface ApiService {
        public static string ApiAddress = "localhost:5331";
        public static Uri RestApiUri = new Uri($"http://localhost:5331");
        public static HttpClient ApiClient;

        public static bool IsConnected = false;

        // WebSockets
        public static ClientWebSocket? AccountWebSocket;
        public static ClientWebSocket? ChatWebSocket;

        /// <summary>
        /// Check if application can open all ws connection to api.
        /// If one failed then reset all.
        /// 
        /// This should only be called when init (or connection test).
        /// </summary>
        public static async Task<bool> TryStartWebSockets() {
            await CloseAllWebSockets();

            IsConnected = false;

            if (!await _StartAccountWebSocket() ||
                !await _StartChatWebSocket())
                return false;

            ChatService.StartChatReceiveLoop();

            IsConnected = true;
            return true;
        }

        public static async Task CloseAllWebSockets() {
            await _CloseAccountWebSocket();
            await _CloseChatWebSocket();
        }

        /// <summary>
        /// Try to convert a provided IP in .:{port?} format to localhost:{port?} format.
        /// Return true if provided IP is in correct format.
        /// </summary>
        public static bool TryConvertIP(string src, out string? result) {
            src = src.Trim();

            if (string.IsNullOrWhiteSpace(src)) {
                result = null;
                return false;
            }

            var parts = src.Split(':');

            // check for no port first
            if (parts.Length == 1) {
                if (src == ".") {
                    result = null;
                    return false;
                }

                result = src;
                return true;
            }

            var (ip, port) = (parts[0], parts[1]);
            if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(port)) {
                result = null;
                return false;
            }

            ip = (ip == ".") ? "localhost" : ip;

            result = $"{ip}:{port}";
            return true;
        }

        private static async Task<bool> _StartAccountWebSocket() {
            try {
                AccountWebSocket = new ClientWebSocket();
                await AccountWebSocket.ConnectAsync(new Uri($"ws://{ApiAddress}/api/ws/account"), CancellationToken.None);
            }
            catch (Exception ex) {
                Debug.WriteLine($"[Debug] {ex.Message}");
                return false;
            }

            return true;
        }

        private static async Task _CloseAccountWebSocket() {
            if (AccountWebSocket == null) return;

            try {
                if (AccountWebSocket.State == WebSocketState.Open)
                    await AccountWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
            }
            catch { }

            AccountWebSocket?.Dispose();
        }

        private static async Task<bool> _StartChatWebSocket() {
            try {
                ChatWebSocket = new ClientWebSocket();
                await ChatWebSocket.ConnectAsync(new Uri($"ws://{ApiAddress}/api/ws/chat"), CancellationToken.None);
            }
            catch (Exception ex) {
                Debug.WriteLine($"[Debug] {ex.Message}");
                return false;
            }

            return true;
        }

        private static async Task _CloseChatWebSocket() {
            if (ChatWebSocket == null) return;
            
            try {
                if (ChatWebSocket.State == WebSocketState.Open)
                    await ChatWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                ChatWebSocket?.Dispose();
            }
            catch { }
        }
    }
}
