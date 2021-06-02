using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Считать_массив_из_файла
{
    class Program
    {
        static int[] GetArrayFromFile(string path, int size)
        {
            int[] array = new int[size];
            string buff;

            using (StreamReader sr = new StreamReader(path))
            {
                buff = sr.ReadLine();
                string[] buffer = buff.Split(' ');
                try
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (int.TryParse(buffer[i], out array[i]))
                        {
                            int.TryParse(buffer[i], out array[i]);
                        }
                        else
                        {
                            int a = 1, b = 0;
                            Console.WriteLine(a / b);
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Элемент массива не является числом");
                }
            }
            return array;
        }
        static void Main(string[] args)
        {
            int size = 5;
            int[] array = new int[size];
            try
            {
                array = GetArrayFromFile("input.txt", 5);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); ;
            }
            for (int i = 0; i < size; i++)
            {
                Console.Write(array[i]+" ");
            }
            Console.ReadKey();
        }
    }
}
