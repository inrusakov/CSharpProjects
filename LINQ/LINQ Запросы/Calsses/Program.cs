using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person("Bob", 1000);
            Person p2 = new Person("Acc", 3000);
            Person p3 = new Person("Bsb", 2000);
            Person p4 = new Person("Bfb", 4000);
            Person p5 = new Person("Beb", 5000);
            Person p6 = new Person("Bib", 6000);

            Person[] personArray = new Person[] { p1, p2, p3, p4, p5, p6 };
            foreach (var p in personArray)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine();

           // Array.Sort(personArray,
             //   (x,y) => x.NetWorth >= y.NetWorth ? 1:-1);
            Array.Sort(personArray);

            foreach (var p in personArray)
            {
                Console.WriteLine(p);
            }

            Company c1 = new Company("HSE",1991,100, personArray[0]);
            Company c2 = new Company("Micr", 1912, 299, personArray[1]);
            Company c3 = new Company("MIFI", 1972, 2412, personArray[2]);
            Company c4 = new Company("MGU", 1942, 431, personArray[3]);
            Company c5 = new Company("Yandex", 1953, 123, personArray[4]);
            Company c6 = new Company("IBM", 1962, 532, personArray[5]);

            Company[] companies = new Company[] { c1, c2, c3, c4, c5, c6 };

            foreach (var comp in companies)
            {
                Console.WriteLine(comp);
            }
            Company newC = c1 + c2;
            if (c1>c2)
            {
                Console.WriteLine(c1);
            }
            else
            {
                Console.WriteLine(c2);
            }
            Console.WriteLine(newC);

            Console.ReadLine();


        }
    }
}
