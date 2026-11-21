namespace uChat_API.Helper {
    public static class uPrint {
        /// <summary>
        /// Print info type message
        /// </summary>
        public static void Info(bool newLine, params string[] args) {
            Console.Write($"[Info] {string.Join(" ", args)}.{(newLine ? "\n" : "")}");
        }

        /// <summary>
        /// Print warning type message
        /// </summary>
        public static void Warning(bool newLine, params string[] args) {
            Console.Write($"[Warning] {string.Join(" ", args).TrimEnd('.')}.{(newLine ? "\n" : "")}");
        }

        /// <summary>
        /// Print error type message
        /// </summary>
        public static void Error(bool newLine, params string[] args) {
            Console.Write($"[Error] {string.Join(" ", args)}.{(newLine ? "\n" : "")}");
        }
    }
}
