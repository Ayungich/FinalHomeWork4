namespace Utilities.Handlers
{
    // Логгер ошибок.
    public class CustomLogger
    {
        static string? fullLoggingFilePath;
        public static void SetFilePath(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("File path is null or empty.");

            fullLoggingFilePath = filePath;
        }
        public async Task LogAsync(bool loggingIsEnabled, Exception? exception)
        {
            if (exception is null || !loggingIsEnabled)
                return;

            var logMessage = $"{DateTime.Now}: {exception.Message}; {exception.Source}; {exception.StackTrace}";

            using StreamWriter sw = new StreamWriter(fullLoggingFilePath!, true);
            await sw.WriteLineAsync(logMessage + Environment.NewLine);
        }
        // Метод для старта логгирования.
        public static void LoggingStart(out bool loggingIsEnabled)
        {
            Thread.Sleep(150);
            Console.Clear();
            Console.WriteLine("[*]Enable error logging?(Y|N)");

            ConsoleKeyInfo enableLoggingKey = Console.ReadKey(true);
            loggingIsEnabled = false;

            if (enableLoggingKey.Key == ConsoleKey.Y)
            {
                SetFilePath(Path.Combine("../../../../", "Logs.txt"));
                loggingIsEnabled = true;
                Assistance.Display("[Logging is enabled]", 5, ConsoleColor.Green);
            }

            else Assistance.Display("[Logging is not enabled]", 5, ConsoleColor.Red);

            Thread.Sleep(500);
            Console.Clear();
        }
        // Метод для остановки логгирования.
        public static void LoggingStop(in bool loggingIsEnabled)
        {
            if (loggingIsEnabled)
            {
                Assistance.Display("\n\nDo you want to clear the log file?(Y|N)", 5, ConsoleColor.Cyan);

                ConsoleKeyInfo clearKey = Console.ReadKey(true);

                if (clearKey.Key == ConsoleKey.Y)
                {
                    LogFileClear();
                    Assistance.Display("\n[The log file has been successfully deleted]", 5, ConsoleColor.Green);
                }
            }
        }
        // Метод для лога ошибки.
        public static void Log(bool loggingIsEnabled, Exception exception)
        {
            if (!loggingIsEnabled) return;
            CustomLogger log = new();
            Task.Run(async () => await log.LogAsync(loggingIsEnabled, exception)).Wait();
        }

        public static void LogFileClear() => File.Delete(fullLoggingFilePath!);
    }
}
