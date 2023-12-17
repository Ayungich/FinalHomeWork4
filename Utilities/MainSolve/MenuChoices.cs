using Utilities.DataModels.MainFunctionality;
using Utilities.DataModels;
using Utilities.Handlers;

namespace Utilities.MainSolve
{
    public class MenuChoices
    {
        public static void MenuFirstChoice(in List<MarriageRegistry> dataFromCsvFile)
        {
            Console.WriteLine($"\n[*] Всего записей в файле: {dataFromCsvFile.Count}\n" +
                                                  "1. Посмотреть N первых записей\n" +
                                                  "2. Посмотреть N последних записей\n-> ");
            ConsoleKey choiceKey1 = Console.ReadKey(true).Key;
            switch (choiceKey1)
            {
                case ConsoleKey.D1:
                    int indexTop = Checkers.IntegerCheck("Введите номер записи, до которой вы хотите просмотреть:\n-> ",
                        ConsoleColor.DarkCyan);
                    Assistance.ListDataDisplay(in dataFromCsvFile, indexTop, mode: Assistance.DisplayMode.Top);
                    break;
                case ConsoleKey.D2:
                    int indexBottom = Checkers.IntegerCheck("Введите номер записи, c которой вы хотите просмотреть:\n-> ",
                        ConsoleColor.DarkCyan);
                    Assistance.ListDataDisplay(in dataFromCsvFile, indexBottom, mode: Assistance.DisplayMode.Bottom);
                    break;
                default:
                    Assistance.Display("Вы ничего не выбрали...", ConsoleColor.Red);
                    break;
            }
        }

        public static void MenuSecondChoice(in List<MarriageRegistry> dataFromCsvFile)
        {
            Console.WriteLine("1. Сортировать по алфавиту(прямой порядок)\n" +
                              "2. Сортировать в обратном порядке\n-> ");
            ConsoleKey choiceKey2 = Console.ReadKey(true).Key;
            switch (choiceKey2)
            {
                case ConsoleKey.D1:
                    var sortedData = SortingAndFiltering.SortingByAdmArea(in dataFromCsvFile, isSortingReverse: false);
                    HandlersForProgramRun.FileSavingModes(in sortedData);
                    break;

                case ConsoleKey.D2:
                    var sortedDataReverse = SortingAndFiltering.SortingByAdmArea(in dataFromCsvFile, isSortingReverse: true);
                    Console.WriteLine("1. Вывести данные\n" +
                                      "2. Записать данные в файл\n-> ");
                    ConsoleKey choiceKey2_2 = Console.ReadKey(true).Key;
                    switch (choiceKey2_2)
                    {
                        case ConsoleKey.D1:
                            Assistance.ListDataDisplay(in sortedDataReverse, 0, mode: Assistance.DisplayMode.Default);
                            break;
                        case ConsoleKey.D2:
                            Assistance.Display("Режимы сохранения в файл:\n", 0, ConsoleColor.Cyan);
                            Console.WriteLine("1. Создание нового файла\n" +
                                              "2. Замена содержимого уже существующего файла\n" +
                                              "3. Добавление сохраняемых данных к содержимому существующего файла \n-> ");
                            ConsoleKey choiceKey2_1_1 = Console.ReadKey(true).Key;
                            switch (choiceKey2_1_1)
                            {
                                case ConsoleKey.D1:
                                    string? newPath = Checkers.StringIsNullOrEmptyCheck("\nВведите путь к файл:\n-> ", ConsoleColor.Cyan);
                                    WorkWithFiles.WriteDataToFile(in sortedDataReverse, newPath, newFileFlag: false);
                                    Assistance.Display("\n[Данные успешно записаны в файл] \n" +
                                        $"\nФайл с записанными данными доступен по пути: {newPath}", 10, ConsoleColor.Green);
                                    break;
                                case ConsoleKey.D2:
                                    string? newFilePath = Checkers.StringIsNullOrEmptyCheck("\nВведите путь к файл:\n-> ", ConsoleColor.Cyan);
                                    WorkWithFiles.WriteDataToFile(in sortedDataReverse, newFilePath, newFileFlag: false);
                                    Assistance.Display("\n[Данные перезаписаны в файл] \n" +
                                        $"\nФайл с перезаписанными данными доступен по пути: {newFilePath}", 10, ConsoleColor.Green);
                                    break;
                                case ConsoleKey.D3:
                                    string? fPath = Checkers.StringIsNullOrEmptyCheck("\nВведите путь к файл:\n-> ", ConsoleColor.Cyan);
                                    WorkWithFiles.WriteDataToFile(in sortedDataReverse, fPath, newFileFlag: true);
                                    Assistance.Display("\n[Данные успешно добавлены в файл] \n" +
                                        $"\nОбновленный файл доступен по пути: {fPath}", 10, ConsoleColor.Green);
                                    break;
                                default:
                                    Assistance.Display("\nВы ничего не выбрали...", 0, ConsoleColor.Red);
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        public static void MenuThirdChoice(in List<MarriageRegistry> dataFromCsvFile)
        {
            string? filterAdmAreaCode = Checkers.StringIsNullOrEmptyCheck("\nВведите значение для фильтрации: \n-> ", ConsoleColor.Cyan);
            var filteringByAdmAreaCode = SortingAndFiltering.FilteringByAdmAreaOrAdmAreaCode(in dataFromCsvFile, $"0{filterAdmAreaCode}", choice: false);

            HandlersForProgramRun.FileSavingModes(in filteringByAdmAreaCode);
        }

        public static void MenuFourthChoice(in List<MarriageRegistry> dataFromCsvFile)
        {
            string? filterAdmArea = Checkers.StringIsNullOrEmptyCheck("Введите значение для фильтрации: \n-> ", ConsoleColor.Cyan);
            var filteringByAdmArea = SortingAndFiltering.FilteringByAdmAreaOrAdmAreaCode(in dataFromCsvFile, filterAdmArea, choice: true);

            HandlersForProgramRun.FileSavingModes(in filteringByAdmArea);
            
        }

        public static void ExitingFromProgram()
        {
            Assistance.Display("Выход.......", 10, ConsoleColor.Cyan);
            Thread.Sleep(100);
            Environment.Exit(0);
        }
    }
}
