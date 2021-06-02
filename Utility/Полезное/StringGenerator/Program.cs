using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringGenerator
{
    class Program
    {
        static Random rnd = new Random();
        /// <summary>
        /// Генератор строк.
        /// </summary>
        /// <param name="length">Длина</param>
        /// <returns>строка</returns>
        static string StringGenerator(int length)
        {
            string newStr = string.Empty;
            newStr += (char)rnd.Next('A', 'Z' + 1);
            for (int i = 0; i < length; i++)
            {
                switch (rnd.Next(1, 4))
                {
                    case 1:
                        if (rnd.Next(1, 3) > 1)
                        {
                            newStr += (char)rnd.Next('A', 'Z' + 1);
                        }
                        else
                        {
                            newStr += (char)rnd.Next('a', 'z' + 1);
                        }
                        break;
                    case 2:
                        newStr += " ";
                        break;
                    case 3:
                        newStr += rnd.Next(1, 10);
                        break;
                }
            }
            return newStr;
        }

        static void Main(string[] args)
        {
        }
    }
}
