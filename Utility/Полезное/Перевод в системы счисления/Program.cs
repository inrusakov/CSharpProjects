using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Перевод_в_системы_счисления
{
    /// <summary>
    /// Ввод переменной для систем счисления
    /// </summary>
    /// <returns> Значение введенное с клавиатуры </returns>
    class Program
    {
        static int GetValueSystem()
        {
            string str;
            int temp;
            bool check;

            str = Console.ReadLine();
            check = int.TryParse(str, out temp);
            // Проверка на тип.
            if (check == true && temp > 1 && temp < 10)
            {
                return temp;
            }
            else
            {
                Console.WriteLine("wrong");
                Environment.Exit(0);
                return 0;
            }
        }
        /// <summary>
        /// Ввод переменной для перевода
        /// </summary>
        /// <param name="system"> Cистема счисления</param>
        /// <returns>Если каждая цифра в числе меньше системы счисления то возврат числа, else "wrong"</returns>
        static int GetValue(int system)
        {
            string str;
            int temp;
            bool check;

            str = Console.ReadLine();
            check = int.TryParse(str, out temp);
            // Проверка на тип и числа большие чем система счисления.
            if (check == true && temp > -1 && CheckEveryDigit(temp, system) == true)
            {
                return temp;
            }
            else
            {
                Console.WriteLine("wrong");
                Environment.Exit(0);
                return 0;
            }
        }
        /// <summary>
        /// Перевод в десятичную систему счисления
        /// </summary>
        /// <param name="system"> Значения системы из которой будет перевод </param>
        /// <param name="value"> Само значение переменной которую будут переводить </param>
        /// <returns> Значение в десятичной системе </returns>
        static int CountInDecimal(int system, int value)
        {
            int new_value = 0, counter = 0;
            while (value > 0)
            {
                new_value += ((value % 10) * (int)(Math.Pow(system, counter)));
                counter++;
                value = value / 10;
            }
            return new_value;
        }
        /// <summary>
        /// Проверка каждой цифры в числе для соотношения с системой счисления
        /// </summary>
        /// <param name="value"> Значение для проверки </param>
        /// <param name="system"> Система счисления </param>
        /// <returns> Если каждая цифра в числе меньше системы счисления то true , else false </returns>
        static bool CheckEveryDigit(int value, int system)
        {
            while (value > 0)
            {
                if (value % 10 < system)
                {
                    value = value / 10;
                }
                else if (value % 10 >= system)
                {
                    return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            //переменная для системы и переменная для самого значения 
            int system, value;
            //Ввод значения для системы
            system = GetValueSystem();
            //Ввод значения для переменной 
            value = GetValue(system);
            //Вывод переведенного значения  
            Console.WriteLine(CountInDecimal(system, value));
        }
    }
}
