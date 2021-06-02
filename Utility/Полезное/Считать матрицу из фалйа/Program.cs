using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Считать_матрицу_из_фалйа
{
    class Program
    {
        static int[,] GetArrayFromFile(string path, int m,int n)
        {
            int[,] array = new int[m,n];
            string buff;

            using (StreamReader sr = new StreamReader(path))
            {
                try
                {
                    for (int i = 0; i < m; i++)
                    {
                        buff = sr.ReadLine();
                        string[] buffer = buff.Split(' ');
                        for (int j = 0; j < n; j++)
                        {
                            if (int.TryParse(buffer[j], out array[i, j]))
                            {
                                int.TryParse(buffer[j], out array[i, j]);
                            }
                            else
                            {
                                int a = 1, b = 0;
                                Console.WriteLine(a / b);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Элемент массива не является числом");
                }
            }
            return array;
        }
        static void Main(string[] args)
        {
            int[,] array = new int[5,5];
            try
            {
                array = GetArrayFromFile("input.txt", 5, 5);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); ;
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine("\n");
            }
            Console.ReadKey();
        }
    }
}
