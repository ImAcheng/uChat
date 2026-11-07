using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace uChat_Desktop.Service {
    internal class FileService {
        private readonly static string _AppFileRoot = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ".uChat");

        /// <summary>
        ///     Call this when program start.
        ///     This check if all import appdata exists.
        ///     Automatically create one when lost.
        /// </summary>
        public static void Init() {
            Debug.WriteLine($"[Debug] Application data saved at {_AppFileRoot}");

            ConfirmDir(_AppFileRoot);
            ConfirmDir(MakePath("Config"));
            ConfirmDir(MakePath("Chat"));
        }

        /// <summary>
        ///     Return a path string with _AppFileRoot
        /// </summary>
        public static string MakePath(params string[] paths) {
            var _pList = paths.ToList();
            _pList.Insert(0, _AppFileRoot);

            return Path.Combine(_pList.ToArray());
        }

        /// <summary>
        ///     Check if dirPath is exists.
        ///     Automatically create one if not.
        /// </summary>
        public static bool ConfirmDir(string dirPath) {
            if (!Directory.Exists(dirPath)) {
                Directory.CreateDirectory(dirPath);
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Check if filePath is exists.
        ///     Automatically create one if not.
        /// </summary>
        public static bool ConfirmFile(string filePath) {
            if (!File.Exists(filePath)) {
                File.Create(filePath).Close();
                return false;
            }

            return true;
        }
    }
}
