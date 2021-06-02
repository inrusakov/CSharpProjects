using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Повторение_решения
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {

                Console.WriteLine("To exit press ESC...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
