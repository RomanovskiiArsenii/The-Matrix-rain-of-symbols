namespace The_Matrix_rain_of_symbols
{
    //TODO: усовершенствовать потоки 
    /// <summary>
    /// Класс создания дождя из капель
    /// </summary>
    internal class GetRainy
    {
        readonly Rain[] raindrops;  // количество капель
        readonly int pause;         // задержка между каплями

        /// <summary>
        /// Конструктор создает массив из объектов класса Rain
        /// </summary>
        /// <param name="quantity">количество элементов массива</param>
        /// <param name="ticks">интервал времени между символами</param>
        /// <param name="pause">интервал времени между потоками</param>
        /// <param name="deleteTime">интервал времени до удаления капли</param>
        public GetRainy(int quantity, int ticks, int deleteTime, int pause)
        {
            raindrops = new Rain[quantity];
            for (int i = 0; i < quantity; i++)
            {
                raindrops[i] = new Rain(ticks, deleteTime);
            }
            this.pause = pause;
        }
        /// <summary>
        /// Старт потоков, в количестве, соответствующем количеству капель + спецэффект начала
        /// </summary>
        public void LetsGetWet()
        {
            for (int i = 0; i < raindrops.Length; i++)                              // перебор массив аобъектов класса Rain
            {
                int startEffect = 7;                                                // спецэффект начала дождя (количество капель с задержкой и время)
                new Thread(raindrops[i].RainDrop).Start();                          // объявление потока по слабой ссылке
                if (i < startEffect) Thread.Sleep(pause * (3 * startEffect - i));   // задержка между потоками (спецэффект)
                else Thread.Sleep(pause);                                           // задержка между потоками
            }
        }
    }
}
