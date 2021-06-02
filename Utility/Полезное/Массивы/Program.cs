using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Массивы
{
    class Program
    {
        static void Main(string[] args)
        {
            int [] arrayName = new int[10] {5,3,2,5,6,2,3,4,5,6};
            Array.Sort(arrayName);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(arrayName[i]+"");
            }
            Console.ReadLine(  );

            string[] input = Console.ReadLine().Split();
            // "0 0 1"
            // ["0", "0", "1"]
            string test;
            test = string.Join(" ",arrayName);
        }
    }
}
