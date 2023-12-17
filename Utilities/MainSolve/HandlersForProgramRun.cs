using Utilities.DataModels;
using Utilities.DataModels.MainFunctionality;
using Utilities.Handlers;

namespace Utilities.MainSolve
{
    public class HandlersForProgramRun
    {
        // Метод, устанавливающий нужные настройки консоли.
        public static void SetConsoleSettings(ConsoleColor color, bool cursorVisible)
        {
            Console.Clear();
            Console.ForegroundColor = color;
            Console.CursorVisible = cursorVisible;
        }
        // Метод, который загружает данные из .сsv файла и сообщает об этом.
        public static void LoadDataFromCsvFile(out string? filePath, out List<MarriageRegistry> dataFromCsvFile)
        {
            filePath = Checkers.StringIsNullOrEmptyCheck("Введите абсолютный путь к .csv файлу:\n-> ", ConsoleColor.Cyan);
            // Для загрузки данных используем метод LoadData из DataModels.MainFunctionality.
            dataFromCsvFile = WorkWithFiles.LoadData(filePath);

            Assistance.Display("\n[Данные успешно загружены]", 15, ConsoleColor.Green);
            Console.Clear();
        }
        // Выводим пользователю меню.
        public static void ShowMenu(in string? filePath)
        {
            if (string.IsNullOrEmpty(filePath) || filePath is null)
                throw new ArgumentNullException("File path is not set.");

            Console.WriteLine($"[*]Вы работаетe с файлом: {Path.GetFileName(filePath)}\n");
            Assistance.Display("Выберите одно из пунктов меню: \n", ConsoleColor.Cyan);
            Console.WriteLine($"1. Посмотреть N первых или N последних записей из файла\n" +
                              $"2. Выполнить сортировку по AdmArea \n" +
                              $"3. Выполнить фильтрацию по AdmAreaCode \n" +
                              $"4. Выполнить фильтрацию по AdmArea \n" +
                              $"5. Выйти из программы\n");
        }
        /* Выводим пользователю сообщение об ошибке, если таковая возникла, 
           и логгируем ее, если пользователь включил логгирование. */
        public static void DisplayErrorMessageAngLog(bool loggingIsEnabled, Exception ex)
        {
            Assistance.Display($"\n{ex.Message}", 10, ConsoleColor.Red);
            // Производим логгирование ошибок, если пользователь включил его. 
            CustomLogger.Log(loggingIsEnabled, ex);
        }
        // Метод, который предоставляет выбор и функционал вывода/дальнейшей работы с данными из файла.
        public static void FileSavingModes(in List<MarriageRegistry> data)
        {
            if (data.Count == 0 || data is null)
                throw new ArgumentNullException("Some data is null.");

            Console.WriteLine("\n1. Вывести данные\n" +
                              "2. Записать данные в файл\n\n");
            ConsoleKey choiceKey3_1 = Console.ReadKey(true).Key;
            switch (choiceKey3_1)
            {
                case ConsoleKey.D1:
                    Assistance.ListDataDisplay(in data, 0, mode: Assistance.DisplayMode.Default);
                    break;
                case ConsoleKey.D2:
                    Assistance.Display("Режимы сохранения в файл:\n", ConsoleColor.Cyan);
                    Console.WriteLine("1. Создание нового файла\n" +
                                      "2. Замена содержимого уже существующего файла\n" +
                                      "3. Добавление сохраняемых данных к содержимому существующего файла \n-> ");
                    ConsoleKey choiceKey2_1_1 = Console.ReadKey(true).Key;
                    switch (choiceKey2_1_1)
                    {
                        case ConsoleKey.D1:
                            string? newPath = Checkers.StringIsNullOrEmptyCheck("\nВведите путь к файлу:\n-> ", ConsoleColor.Cyan);
                            WorkWithFiles.WriteDataToFile(in data, newPath, newFileFlag: false);
                            Assistance.Display("\n[Данные успешно записаны в файл] \n" +
                                $"\nФайл с записанными данными доступен по пути: {newPath}", ConsoleColor.Green);
                            break;
                        case ConsoleKey.D2:
                            string? newFilePath = Checkers.StringIsNullOrEmptyCheck("\nВведите путь к файлу:\n-> ", ConsoleColor.Cyan);
                            WorkWithFiles.WriteDataToFile(in data, newFilePath, newFileFlag: false);
                            Assistance.Display("\n[Данные перезаписаны в файл] \n" +
                                $"\nФайл с перезаписанными данными доступен по пути: {newFilePath}", ConsoleColor.Green);
                            break;
                        case ConsoleKey.D3:
                            string? fPath = Checkers.StringIsNullOrEmptyCheck("\nВведите путь к файлу:\n-> ", ConsoleColor.Cyan);
                            WorkWithFiles.WriteDataToFile(in data, fPath, newFileFlag: true);
                            Assistance.Display("\n[Данные успешно добавлены в файл] \n" +
                                $"\nОбновленный файл доступен по пути: {fPath}", ConsoleColor.Green);
                            break;
                        default:
                            Assistance.Display("\nВы ничего не выбрали...", ConsoleColor.Red);
                            break;
                    }
                    break;
            }
        }
    }
}
