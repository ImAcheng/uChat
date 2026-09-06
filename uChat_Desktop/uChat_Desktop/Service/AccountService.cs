using AntdUI;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using uChat_Desktop.DataModel;
using uChat_Desktop.Lang;
using uChat_Desktop.MainApp;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace uChat_Desktop.Service {
    internal class AccountService {
        public static AccountDTO? LoggedAccount = null;

        private readonly static ISerializer _YamlSerializer =
                new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

        private readonly static IDeserializer _YamlDeserializer = 
                new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

        /// <summary>
        ///     Check if there's a saved login.
        ///     Return false when no,
        ///     return true when yes and login works.
        ///     (Also processs login works.)
        /// </summary>
        public static async Task<bool> CheckSavedLogin() {
            string fp = FileService.MakePath("SavedLogin.yml");
            if (!FileService.ConfirmFile(fp)) {
                return false;
            }

            using var sr = new StreamReader(fp);
            string yaml = await sr.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(yaml))
                return false;

            var content = _YamlDeserializer.Deserialize<SavedAccount>(yaml);
            return await _DirectLogin(content);
        }

        // APIs

        /// <summary>
        /// Try to login with username and password.
        /// This method checks almost any kinds of exception.
        /// return an AccountResult(bool success, string message, AccountDTO? account)
        /// </summary>
        public static async Task<AccountResult?> LoginTo(string username, string password) {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) {
                return new AccountResult() {
                    success = false,
                    message = Langs.uname_pwd_empty
                };
            }

            var rqContent = new {
                username,
                password
            };

            try {
                using var client = new HttpClient() { BaseAddress = ApiService.ApiUri };
                using var resp = await client.PostAsJsonAsync("api/account/login", rqContent);

                var content = await resp.Content.ReadFromJsonAsync<AccountResult>();

                if (content == null || !content.success) {
                    Debug.WriteLine("[Debug] Login failed at data is null or not success.");
                    return new AccountResult() {
                        success = false,
                        message = _GetMessage(content?.message ?? null)
                    };
                }
                
                if (content.account == null) {
                    Debug.WriteLine("[Debug] Login failed at account is null.");

                    return new AccountResult() {
                        success = false,
                        message = Langs.data_failed
                    };
                }

                LoggedAccount = content.account;

                await _TrySaveAccount(new SavedAccount() { accountId = content.account.AccountId, password = password });

                return new AccountResult() {
                    success = content.success,
                    message = _GetMessage(content.message),
                    account = content.account
                };
            }
            catch (JsonException) {
                return new AccountResult() {
                    success = false,
                    message = Langs.data_failed
                };
            }
            catch (HttpRequestException) {
                return new AccountResult() {
                    success = false,
                    message = Langs.network_problem
                };
            }
        }

        /// <summary>
        ///     Call api to login with saved config.
        ///     This should be called when program start.
        /// </summary>
        private static async Task<bool> _DirectLogin(SavedAccount account) {
            try {
                using HttpClient client = new HttpClient() { BaseAddress = ApiService.ApiUri };
                using var resp = await client.PostAsJsonAsync("api/account/d-login", account);
                var content = await resp.Content.ReadFromJsonAsync<AccountResult>();

                if (!content?.success ?? true) {
                    return false;
                }

                return true;
            }
            catch (JsonException) {
                Debug.WriteLine("[Debug] Json failed");
                return false;
            }
            catch (HttpRequestException) {
                Debug.WriteLine("[Debug] Connection failed.");
                return false;
            }
        }

        /// <summary>
        ///     Call api to create an account.
        /// </summary>
        public static async Task<bool> CallCreateAccount(CreateAccountRequest rq) {
            using HttpClient client = new HttpClient() { BaseAddress = new Uri("http://localhost:5331") };
            using var resp = await client.PostAsJsonAsync("api/account/create", rq);

            var content = await resp.Content.ReadFromJsonAsync<AccountResult>();

            try {
                
            }
            catch {
                Notification.error(FormMain.instance, "uChat", 
                    "A network related problem occurred.\nPlease try again later.", autoClose: 3);
                return false;
            }

            return true;
        }

        /// <summary>
        /// return a string gets from Langs.resx
        /// return the key itself when not found
        /// </summary>
        private static string _GetMessage(string? key) {
            if (key == null) return Langs.data_failed;

            Dictionary<string, string> dict = new Dictionary<string, string>() {
                { "EMPTY_DATA", Langs.uname_pwd_empty },
                { "ACC_NOT_FOUND", Langs.acc_not_found },
                { "PWD_FAIL", Langs.pwd_failed },
                { "BAD_EMAIL", Langs.bad_email },
                { "EXISTED_DATA", Langs.acc_existed },
                { "OK", "OK" }
            };

            if (dict.TryGetValue(key, out var val))
                return val;

            return key;
        }

        private static async Task<bool> _TrySaveAccount(SavedAccount account) {
            if (account == null) return false;

            string yaml = _YamlSerializer.Serialize(account);

            string fp = FileService.MakePath("SavedLogin.yml");
            bool fileCheck = FileService.ConfirmFile(fp);

            if (!fileCheck) {
                Console.WriteLine("[Debug] Account config file was not existed. Auto created one.");
            }

            await File.WriteAllTextAsync(fp, yaml, Encoding.UTF8);

            return true;
        }
    }

    internal class AccountResult {
        public bool success { get; set; }
        public string message { get; set; }
        public AccountDTO? account { get; set; }
    }

    public class CreateAccountRequest {
        public string email { get; set; }
        public string username { get; set; }
        public string? nickname { get; set; }
        public string? accountBio { get; set; }
        public byte genderCode { get; set; }
    }

    public class SavedAccount {
        public Guid accountId { get; set; }
        public string password { get; set; }
    }
}
