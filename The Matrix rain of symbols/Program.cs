namespace The_Matrix_rain_of_symbols
{
    class Program
    {
        static void Main()
        {
            Console.CursorVisible = false;                              // выключение видимости курсора
            Intro.WriteIntro(20);                                       // вступление
            Thread.Sleep(1000);                                         // пауза

            GetRainy getRainy = new(1000, 50, 5000, 100);               // инстанцирование дождя
            getRainy.LetsGetWet();                                      // старт
        }
    }
}