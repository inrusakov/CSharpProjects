using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Работа_на_семинарах
{
    class Program
    {
        static long GetIntInput(int lowerBound, int upperBound)
        {
            string s = Console.ReadLine();

            int n;

            while (!int.TryParse(s, out n) || n < lowerBound || n > upperBound)
            {
                Console.WriteLine("Repeat input");
                s = Console.ReadLine();
            }

            return n;
        }

        public delegate long MathOperation(long a);

        public static Dictionary<string, MathOperation> operations = new Dictionary<string, MathOperation>(4);

        static void Main(string[] args)
        {
            long val = GetIntInput(int.MinValue, int.MaxValue);
            MathOperation Pow = (x) =>
            {
                return x * x;
            };



            MathOperation Perc0 = (x) =>
            {
                Console.WriteLine($"{x} % 3 = 0 => {x} * 3 = {3 * x}");
                return 3 * x;
            };
            MathOperation Perc1 = (x) =>
            {
                Console.WriteLine($"{x} % 3 = 1 => {x} * 2 ={2 * x}");
                return 2 * x;
            };
            MathOperation Perc2 = (x) =>
            {
                Console.WriteLine($"{x} % 3 = 2 => {x} = {x}");
                return x;
            };



            MathOperation PowMinusval = (a) =>
            {
                Console.WriteLine($"{a * a} - {a} = {Pow(a) - a}");
                return (Pow(a) - a);
            };



            MathOperation PercHigher = (a) =>
            {
                Console.WriteLine($"{a}% 10 = {a % 10} => {a} >> 1 = {a >> 1}");
                return a >> 1;
            };
            MathOperation PercLowerOrEq = (a) =>
            {
                Console.WriteLine($"{a}% 10 = {a % 10} => {a} << 1 = {a << 1}");
                return a << 1;
            };


            operations.Add("1", Pow);
            operations.Add("2.1", Perc0);
            operations.Add("2.2", Perc1);
            operations.Add("2.3", Perc2);
            operations.Add("3", PowMinusval);
            operations.Add("4.1", PercHigher);
            operations.Add("4.2", PercLowerOrEq);


            Console.WriteLine($"{val}*{val} = "+operations["1"](val));
            val = operations["1"](val);
            if (val % 3 == 0)
            {
                val = operations["2.1"](val);
            }
            else if (val % 3 == 1)
            {
                val = operations["2.2"](val);
            }
            else if (val % 3 == 2)
            {
                val = operations["2.3"](val);
            }
            val = operations["3"](val);
            if (val % 10 > 5)
            {
                val = operations["4.1"](val);
            }
            else if (val % 10 <= 5)
            {
                val = operations["4.2"](val);
            }
            Console.ReadLine();

        }
    }
}
