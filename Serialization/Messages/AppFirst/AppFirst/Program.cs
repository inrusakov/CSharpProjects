using System;
using Newtonsoft.Json;
using System.IO;

// Русаков Иван БПИ194
namespace MessageLib
{
    class Program
    {
        /// <summary>
        /// Ввод значение 
        /// </summary>
        /// <param name="lowerBound"> Наименьше  значение </param>
        /// <param name="upperBound"> Наибольшее значение </param>
        /// <returns> Введенное значение </returns>
        static int GetIntInput(int lowerBound, int upperBound)
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
        /// <summary>
        /// Статический рандом.
        /// </summary>
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
            do
            {
                Console.Clear();
                Console.WriteLine("Enter number of Messages");
                int number = GetIntInput(10, 1000);
                MessageBox newBox = new MessageBox();

                // Запись и Сериализация обычных Message.
                for (int i = 0; i < number; i++)
                {
                    try
                    {
                        newBox.ReceiveMail(new Message(StringGenerator(rnd.Next(5, 100)), DateTime.Now));
                    }
                    catch (Exception)
                    {
                        i--;
                    }
                }

                // Запись и Сериализация обычных Message.
                for (int i = 0; i < number / 2; i++)
                {
                    try
                    {
                        newBox.ReceiveMail(new Dmessage(StringGenerator(rnd.Next(5, 36)), DateTime.Now, rnd.Next(1, 1000)));
                    }
                    catch (Exception)
                    {
                        i--;
                    }
                }
                
                // Вывод в консоль.
                foreach (var item in newBox)
                {
                    Console.WriteLine(item);
                }
                try
                {
                    using (StreamWriter sw = new StreamWriter("messageBox.json"))
                    {
                        for (int i = 0; i < newBox.Messages.Count; i++)
                        {
                            sw.WriteLine(JsonConvert.SerializeObject(newBox.Messages[i]));
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("To exit press ESC...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
