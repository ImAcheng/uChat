using AntdUI;
using System.Net.Http.Json;
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
    }
}
