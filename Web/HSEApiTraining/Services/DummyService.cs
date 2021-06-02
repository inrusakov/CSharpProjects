using System;

namespace HSEApiTraining
{
    /// <summary>
    /// Интерфейс для IDummyService
    /// </summary>
    public interface IDummyService
    {
        /// <summary>
        /// Метод получения строки со случайным числом
        /// </summary>
        /// <param name="number">Ограничение на рандом</param>
        /// <returns>Сгенерированная строка</returns>
        string DummyGenerator(int number);
    }

    public class DummyService : IDummyService
    {
        static Random rand = new Random();

        /// <summary>
        /// Метод генерации случайного числа
        /// </summary>
        /// <param name="number">Целое число</param>
        /// <returns>Сгенерированная строка</returns>
        public string DummyGenerator(int number)
        {
            if (number <0)
            {
                return $"Random ({number}, 0): {rand.Next(number,0)}";
            }
            else
            {
                return $"Random (0, {number}): {rand.Next(number)}";
            }
        }
    }
}
