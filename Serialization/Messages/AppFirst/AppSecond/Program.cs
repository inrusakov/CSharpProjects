using System;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace MessageLib
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                MessageBox newBox = new MessageBox();
                try
                {
                    // Считывание из файла и десериализация.
                    using (StreamReader sr = new StreamReader(@"..\..\..\AppFirst\bin\Debug\messageBox.json"))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if (line.StartsWith("{\"Content\""))
                            {
                                newBox.ReceiveMail(JsonConvert.DeserializeObject<Message>(line));
                            }
                            else
                            {
                                newBox.ReceiveMail(JsonConvert.DeserializeObject<Dmessage>(line));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                foreach (var item in newBox)
                {
                    Console.WriteLine(item);
                }

                // LINQ 1.
                Console.WriteLine("LINQ 1");
                Message before = newBox.OrderBy(x => x.ReceiveDate).Where(x => x.GetType() == typeof(Message)).First();
                foreach (var item in newBox.Where(x => x.Content.Length > 20).Where(x => x.ReceiveDate < before.ReceiveDate))
                {
                    Console.WriteLine(item);
                }


                Console.WriteLine("LINQ 2");
                var secLinq = newBox
                    .Where(x => x.GetType() == typeof(Dmessage))
                    .GroupBy(x => (x as Dmessage).Hours / 24)
                    .OrderBy(x => x.Key)
                    .Select(g => g.OrderBy(x => x.ReceiveDate).ToList())
                    .ToList();
                foreach (var item in secLinq)
                {
                    foreach (var bite in item)
                    {
                        Console.WriteLine(bite);
                    }
                }

                Console.WriteLine("To exit press ESC...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
