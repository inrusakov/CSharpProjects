using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq4_Сложение
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] soft = { "Microsoft", "Google", "Apple" };
            string[] hard = { "Apple", "IBM", "Samsung" };

            // пересечение последовательностей
            var result = soft.Intersect(hard);
            foreach (string s in result)
                Console.WriteLine(s);
            Console.WriteLine();

            // объединение последовательностей
            var result2 = soft.Union(hard);
            foreach (string s in result2)
                Console.WriteLine(s);
            Console.WriteLine();

            //Еще одно пересечение которое равно Union при Distinct 
            var result3 = soft.Concat(hard); //.Distinct();
            foreach (string s in result3)
                Console.WriteLine(s);

            Console.ReadKey();
        }
    }
}
