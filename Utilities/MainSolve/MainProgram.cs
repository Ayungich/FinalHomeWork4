using Utilities.DataModels;
using Utilities.Handlers;

namespace Utilities.MainSolve
{
    public class MainProgram
    {
        public static void ProgramRun()
        {
            do
            {
                HandlersForProgramRun.SetConsoleSettings(ConsoleColor.DarkCyan, cursorVisible: false);

                // Даем пользователю возможность выхода на данном этапе.
                Assistance.Display("Нажмите Enter для продолжения или Escape для выхода...\n", ConsoleColor.DarkCyan);
                ConsoleKey exitKey = Console.ReadKey(true).Key;

                // Если пользователь нажимает Escape, то осуществляется немедленный выход из программы.
                if (exitKey == ConsoleKey.Escape) MenuChoices.ExitingFromProgram();

                // Если пользователь нажал Enter, то её работа продолжается.
                else if (exitKey == ConsoleKey.Enter)
                {
                    bool loggingIsEnabled;
                    CustomLogger.LoggingStart(out loggingIsEnabled);

                    try
                    {
                        // Массивы данных.
                        List<MarriageRegistry> dataFromCsvFile;
                        string? filePath;

                        HandlersForProgramRun.LoadDataFromCsvFile(out filePath, out dataFromCsvFile);
                        HandlersForProgramRun.ShowMenu(in filePath);

                        ConsoleKey choiceKey = Console.ReadKey(true).Key;
                        switch (choiceKey)
                        {
                            case ConsoleKey.D1:
                                MenuChoices.MenuFirstChoice(in dataFromCsvFile);
                                break;
                            case ConsoleKey.D2:
                                MenuChoices.MenuSecondChoice(in dataFromCsvFile);
                                break;
                            case ConsoleKey.D3:
                                MenuChoices.MenuThirdChoice(in dataFromCsvFile);
                                break;
                            case ConsoleKey.D4:
                                MenuChoices.MenuFourthChoice(in dataFromCsvFile);
                                break;
                            case ConsoleKey.D5:
                                MenuChoices.ExitingFromProgram();
                                break;
                            case ConsoleKey.P:
                                Assistance.DrawHeart();
                                break;
                            default:
                                Assistance.Display("\nВы ничего не выбрали", 5, ConsoleColor.Red);
                                break;
                        }
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        HandlersForProgramRun.DisplayErrorMessageAngLog(loggingIsEnabled, ex);
                    }
                    catch (ArgumentException ex)
                    {
                        HandlersForProgramRun.DisplayErrorMessageAngLog(loggingIsEnabled, ex);
                    }
                    catch (Exception ex)
                    {
                        HandlersForProgramRun.DisplayErrorMessageAngLog(loggingIsEnabled, ex);
                    }
                    // Спрашиваем, хочет ли пользователь очистить(удалить) log-файл.
                    CustomLogger.LoggingStop(in loggingIsEnabled);
                    Assistance.Display("\n\nPress any key to restart program or \'Escape\' to close...", ConsoleColor.Green);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
