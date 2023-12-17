namespace Utilities.Handlers
{
    public class Assistance
    {
        public static void DrawHeart() // Рисуем сердечко
        {
            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("  /\\  /\\");
            Console.WriteLine(" /  \\/  \\");
            Console.WriteLine("/        \\");
            Console.WriteLine("\\        /");
            Console.WriteLine(" \\      /");
            Console.WriteLine("  \\    /");
            Console.WriteLine("   \\  /");
            Console.WriteLine("    \\/");
            Console.ResetColor();
            Console.ForegroundColor = defaultColor;
        }
        // Удобный метод для цветного вывода, не меняющий стандартную цветовую схему в консоли.
        public static void Display(string? inputText, int threadSleepTime, ConsoleColor color)
        {
            if (string.IsNullOrEmpty(inputText))
                throw new ArgumentNullException("Input text is null or empty.");

            if (threadSleepTime < 0)
                throw new ArgumentException("Sleep time must be greater than zero");

            var defaulColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            foreach (var c in inputText)
            {
                Console.Write(c);
                Thread.Sleep(threadSleepTime);
            }

            Console.ForegroundColor = defaulColor;
        }
        // Перегрузка вышеописанного метода, но без Thread.Sleep().
        public static void Display(string? inputText, ConsoleColor color)
        {
            if (string.IsNullOrEmpty(inputText))
                throw new ArgumentNullException("Input text is null or empty.");

            var defaulColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(inputText);
            Console.ForegroundColor = defaulColor;
        }
        // Метод для удаления повторяющихся данных из списка.
        public static void TrimListFromIndex<T>(int startIndex, in List<T> data)
        {
            if (startIndex < 0) throw new ArgumentOutOfRangeException("Index is low than zero.");
            if (startIndex < data.Count) data.RemoveRange(startIndex, data.Count - startIndex);
        }
        // Перечисление, содержащее режимы работы вывода списка с данными.
        public enum DisplayMode
        {
            Top,
            Bottom,
            Default
        }
        /* Вывод данных из списка, имеющий следующие режимы работы:
         * 1. Cтандартный вывод данных, т.е с 0 индекса, режим работы - Default.
         * 2. Вывод index-первых значений из списка, режим работы - Top.
         * 3. Вывод index-последних значений из списка, режим работы - Bottom.
         */
        public static void ListDataDisplay<T>(in List<T> list, int index, DisplayMode mode)
        {
            if (list is null || list.Count == 0)
                throw new ArgumentNullException("List is null or empty.");

            if (index < 0) throw new ArgumentOutOfRangeException("Index is low than zero.");

            if (index > list.Count) throw new ArgumentOutOfRangeException("Index is greater than list lenght.");

            switch (mode)
            {
                case DisplayMode.Default:
                    foreach (var data in list)
                    {
                        if (data is null)
                            throw new ArgumentException("Some data is null.");
                        Console.WriteLine(data);
                    }
                    break;
                case DisplayMode.Top:
                    for (int i = 0; i < index; i++)
                    {
                        if (list[i] is null)
                            throw new ArgumentException("Some data is null.");
                        Console.WriteLine(list[i]);
                    }
                    break;
                case DisplayMode.Bottom:
                    for (int i = list.Count - index; i < list.Count; i++)
                    {
                        if (list[i] is null)
                            throw new ArgumentException("Some data is null.");
                        Console.WriteLine(list[i]);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("You don't choosen anything.");
            }
        }
    }
}
