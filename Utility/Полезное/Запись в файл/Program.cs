using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Запись_в_файл
{
    class Program
    {
        //From here

        /// <summary>
        /// Запись text в файл с адрессом path
        /// </summary>
        /// <param name="path"> Адрес файла </param>
        /// <param name="text"> Текст для записи </param>
        static void FileWriter(string path, string text)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        //To here
        static void Main(string[] args)
        {
            string test = "TestString";
            FileWriter("test.txt", test);
            Console.ReadKey();
        }
    }
}
