using System.Globalization;
using System.Text;

namespace Utilities.Handlers
{
    // Класс, содержащий различные методы для проверки данных.
    public class Checkers
    {
        // Проверка введенных целочисленных значений на корректность.
        public static int IntegerCheck(string outputText, ConsoleColor color)
        {
            Assistance.Display(outputText, color);

            bool isCorrect;
            int data;
            CultureInfo culture = CultureInfo.InvariantCulture;

            do
            {
                isCorrect = int.TryParse(Console.ReadLine(), NumberStyles.Any, culture, out data);

                if (!isCorrect || data < 0)
                {
                    Assistance.Display("\nIncorrect data, please try again:\n-> ", ConsoleColor.Red);
                }
            } while (!isCorrect || data < 0);

            return data;
        }
        // Метод, для проверки корректности абсолютного пути к файлу.
        public static bool IsValidAbsolutePath(string? filePath, bool needFileExisting)
        {
            if (string.IsNullOrEmpty(filePath)) return false;
            if (!Path.IsPathRooted(filePath)) return false;

            if (needFileExisting == true)
            {
                if (!File.Exists(filePath)) return false;
            }

            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var directoryPath = Path.GetDirectoryName(filePath);

            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(directoryPath)) return false;

            byte[] pathBytes = Encoding.Default.GetBytes(filePath);
            // Проверка наличия кириллических символов в пути
            foreach (byte b in pathBytes)
            {
                // Диапазон кодов для кириллических символов
                if (b >= 0xC0 && b <= 0xFF) return true;
            }

            foreach (var symbol in Path.GetInvalidFileNameChars())
            {
                if (fileName.Contains(symbol)) return false;
            }

            foreach (var symbol in Path.GetInvalidPathChars())
            {
                if (directoryPath.Contains(symbol)) return false;
            }

            return true;
        }
        // Метод для проверки расширения файла.
        public static bool FileExtensionCheck(string? fileName, string? fileExtension)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(fileExtension))
                throw new ArgumentNullException("The file name is null or empty.");

            if (!fileName.EndsWith(fileExtension)) return false;

            return true;
        }
        // Метод для проверки корректности введённых строковых данных.
        public static string StringIsNullOrEmptyCheck(string outputText, ConsoleColor color) // Проверка для строк
        {
            Assistance.Display(outputText, color);
            bool IsNullOrEmpty;
            string output;

            do
            {
                output = Console.ReadLine()!;
                IsNullOrEmpty = string.IsNullOrEmpty(output);
                if (IsNullOrEmpty)
                {
                    Assistance.Display("\nIncorrect data, please try again:\n-> ", ConsoleColor.DarkCyan);
                }
            } while (IsNullOrEmpty);
            return output;
        }
    }
}
