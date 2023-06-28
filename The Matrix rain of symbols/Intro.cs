namespace The_Matrix_rain_of_symbols
{
    /// <summary>
    /// Вступление
    /// </summary>
    internal static class Intro
    {
        private static readonly string firstIntro = "The Matrix rain of symbols has a distinct aesthetic appear. " +
            "\nIt has become a symbol of rebellion, technology, and the merging of the virtual and physical worlds.";
        private static readonly string secondIntro = "This program is my way of creating similar visual effects in the standard console view." +
            "\nI created this program when I was learning out with threads and needed a way to get started." +
            "\nThe first raindrops occur slowly. That's how I tried to make the rain more realistic.";

        /// <summary>
        /// метод вывода вступления на экран
        /// </summary>
        static public void WriteIntro(int pause)
        {
            foreach (var item in firstIntro)
            {
                Console.Write(item);
                Thread.Sleep(pause);
            }
            Thread.Sleep(1000);
            Console.WriteLine("\n");
            foreach (var item in secondIntro)
            {
                Console.Write(item);
                Thread.Sleep(pause);
            }
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}
