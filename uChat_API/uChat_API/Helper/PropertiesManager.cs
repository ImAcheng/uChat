namespace uChat_API.Helper {
    public static class PropertiesManager {
        public static Dictionary<string, string> Properties =
            new Dictionary<string, string>() {
                { "targetIp", "0.0.0.0" },
                { "port", "5000" },
                { "dbDataSource", "." },
                { "dbIntegratedSecurity", "True" },
                { "dbEncrypt", "True" },
                { "dbTrustServerCertificate", "True" },
                { "databaseName", "uChat" }
            };

        public static async Task<bool> Init(string fp) {
            try {
                using var sw = new StreamWriter(fp);
                await sw.WriteAsync(string.Join("\n", Properties.Select(x => $"{x.Key}={x.Value}")));
            }
            catch {
                return false;
            }

            return true;
        }

        public static async Task<bool> Check(string fp) {
            var _prop = new Dictionary<string, string>();

            try {
                using var sr = new StreamReader(fp);
                var ctx = await sr.ReadToEndAsync();

                foreach (var item in ctx.Split('\n')) {
                    var pair = item.Split('=');
                    var (key, val) = (pair[0], pair[1]);
                    _prop.Add(key, val);
                }
            }
            catch {
                return false;
            }

            // verify if the dict is correct by counting the len & matching the keys
            Properties = _prop;
            return _prop.Count == Properties.Count && _prop.Keys.All(Properties.ContainsKey);
        }
    }
}
