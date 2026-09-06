using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace uChat_Desktop.Lang {
    /// <summary>
    /// LangsManager:
    ///     Provides multi-langs management.
    ///     The only thing we do is to change application's culture info.
    ///     Langs.resx will auto decide the lang to display.
    /// </summary>
    internal static class LangsManager {
        public static CultureInfo userCulture { get; set; } = CultureInfo.CurrentCulture;

        public static void SetLang(string cultureName) {
            var culture = CultureInfo.GetCultureInfo(cultureName);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            userCulture = culture;
        }
    }
}
