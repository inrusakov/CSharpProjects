using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Чтение_из_файла
{
    class Program
    {
        static string ReadFileLine(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    string line;


                    line = sr.ReadLine();
                    return line;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); ;
                }
                sr.Close();
                return "";
            }
        }
        static void Main(string[] args)
        {
            string test;
            test = ReadFileLine("test.txt");
            Console.WriteLine(test);
            Console.ReadKey();
        }
    }
}
