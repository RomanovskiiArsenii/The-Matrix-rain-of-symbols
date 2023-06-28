namespace The_Matrix_rain_of_symbols
{
    /// <summary>
    /// Класс организации дождевых капель
    /// </summary>
    internal class Rain
    {
        private const string CHARS = "QWERTYUIOPASDFGHJKLZXCVBNM!@#$%^&*1234567890";    // последовательность символов
        private readonly int tick;                                                      // интервал передачи символа 
        private readonly int deleteTime;                                                // время до удаления строки
        private readonly Random random;                                                 // рандомайзер
        static readonly object locker = new();                                          // локкер
        private int lenght;                                                             // длина последовательности символов
        private readonly ConsoleColor[] consoleColors;                                  // массив цветов символов
        private readonly int min = 6;
        private readonly int max = 12;

        public int LimitWidth { get; private set; }             // пределы ширины окна
        public int LimitHeight { get; private set; }            // пределы высоты окна
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tick">интервал передачи символа</param>
        public Rain(int tick, int deleteTime)
        {
            random = new Random();
            lenght = random.Next(min, max);
            consoleColors = new ConsoleColor[]
            {
                ConsoleColor.White,
                ConsoleColor.Green,
                ConsoleColor.DarkGreen
            };
            LimitHeight = Console.WindowHeight;
            LimitWidth = Console.WindowWidth;
            this.tick = tick;
            this.deleteTime = deleteTime;
        }
        /// <summary>
        /// Метод возвращает случайный символ из предоставленной последовательности
        /// </summary>
        /// <returns>случайный символ</returns>
        private char RandomChar()
        {
            char ch = CHARS[random.Next(CHARS.Length)];
            return ch;
        }
        /// <summary>
        /// метод проверки на изменение границ окна
        /// </summary>
        private void CheckBorders()
        {
            if (LimitWidth != Console.WindowWidth) LimitWidth = Console.WindowWidth;
            if (LimitHeight != Console.WindowHeight) LimitHeight = Console.WindowHeight;
        }
        /// <summary>
        /// Метод написания случайного символа (RandomChar()) на заданной позиции консоли
        /// </summary>
        /// <param name="LeftPos">отступ слева</param>
        /// <param name="TopPos">оступ сверху</param>
        /// <param name="Color">цвет символа по индексу в массиве цветов</param>
        private void WriteSymbol(int LeftPos, int TopPos, int Color)
        {
            Console.SetCursorPosition(LeftPos, TopPos);
            Console.ForegroundColor = consoleColors[Color];
            Console.Write(RandomChar());
        }

        /// <summary>
        /// Метод вывода на экран стекающей капли из случайных символов (WriteSymbol())
        /// </summary>
        public void RainDrop()
        {
            try             // обход ошибок при ручном изменении размера окна
            {
                int TopPos = random.Next(Console.WindowHeight - lenght - 1);    // получение позиции от верха
                int LeftPos = random.Next(Console.WindowWidth);                 // получение позиции от левого края

                for (int i = 0; i < lenght; i++)                                // перебор последовательности символов                                
                {
                    lock (locker)                                               // критическая секция
                    {
                        WriteSymbol(LeftPos, TopPos + i, 0);                    // вывод белого символа
                    }
                    Thread.Sleep(tick);                                         // интервал
                    lock (locker)                                               // критическая секция
                    {
                        WriteSymbol(LeftPos, TopPos + i + 1, 0);                // вывод белого символа на следующей позиции
                    }
                    Thread.Sleep(tick);                                         // интервал
                    lock (locker)                                               // критическая секция
                    {
                        WriteSymbol(LeftPos, TopPos + i, 1);                    // вывод зеленого символа на предыдущую позицию
                    }
                    Thread.Sleep(tick);                                         // интервал   
                    if (i > 0)
                    {
                        lock (locker)                                           // критическая секция
                        {
                            WriteSymbol(LeftPos, TopPos + i - 1, 2);            // вывод темно-зеленого символа на третью позицию
                        }
                        Thread.Sleep(tick);                                     // интервал
                    }
                }

                Thread.Sleep(deleteTime);                                       // задержка до удаления последовательности

                for (int i = 0; i <= lenght; i++)                               // перебор по длине последовательности
                {
                    lock (locker)                                               // критическая секция
                    {
                        Console.SetCursorPosition(LeftPos, TopPos + i);         // перевод курсора на нулевой символ
                        Console.Write(' ');                                     // заполнения пробелом           
                    }
                    Thread.Sleep(tick);                                         // интервал
                }
            }
            catch (Exception)
            {
                CheckBorders();                                                 // уточнение границ
            }
            lenght = random.Next(min, max);                                     // новая длина последовательности символов
        }
    }
}
