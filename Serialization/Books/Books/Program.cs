using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace Books
{
    class Program
    {
        public static Dictionary<char, string> translator = new Dictionary<char, string>();
        static void Replace(string text, string path)
        {
            using (StreamWriter sw = new StreamWriter("new" + path))
            {
                Console.WriteLine("\r\nКнига: " + path);
                Console.WriteLine("Было  " + text.Length + " символов");
                long counter = 0;
                for (int i = 0; i < text.Length; i++)
                {
                    string output = string.Empty;
                    if (!char.IsUpper(text[i]) && char.IsLetter(text[i]) && translator.Keys.Contains(char.ToUpper(text[i])))
                    {
                        string newchar;
                        translator.TryGetValue(char.ToUpper(text[i]), out newchar);
                        newchar = char.ToLower(newchar.ToCharArray().First()).ToString();
                        sw.Write(newchar);
                        counter++;
                    }
                    if (char.IsUpper(text[i]) && char.IsLetter(text[i]) && translator.Keys.Contains(char.ToUpper(text[i])))
                    {
                        string newchar;
                        translator.TryGetValue(char.ToUpper(text[i]), out newchar);
                        sw.Write(newchar);
                        counter++;
                    }
                    if (!char.IsLetter(text[i]))
                    {
                        sw.Write(text[i]);
                        counter++;
                    }
                }
                Console.WriteLine("Стало " + counter + " символов");
            }
        }
        static void Replace(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                using (StreamWriter sw = new StreamWriter("new" + path))
                {
                    string text = sr.ReadToEnd();
                    Console.WriteLine("\r\nКнига: " + path);
                    Console.WriteLine("Было  " + text.Length + " символов");
                    long counter = 0;
                    for (int i = 0; i < text.Length; i++)
                    {
                        string output = string.Empty;
                        if (!char.IsUpper(text[i]) && char.IsLetter(text[i]) && translator.Keys.Contains(char.ToUpper(text[i])))
                        {
                            string newchar;
                            translator.TryGetValue(char.ToUpper(text[i]), out newchar);
                            newchar = char.ToLower(newchar.ToCharArray().First()).ToString();
                            sw.Write(newchar);
                            counter++;
                        }
                        if (char.IsUpper(text[i]) && char.IsLetter(text[i]) && translator.Keys.Contains(char.ToUpper(text[i])))
                        {
                            string newchar;
                            translator.TryGetValue(char.ToUpper(text[i]), out newchar);
                            sw.Write(newchar);
                            counter++;
                        }
                        if (!char.IsLetter(text[i]))
                        {
                            sw.Write(text[i]);
                            counter++;
                        }
                    }
                    Console.WriteLine("Стало " + counter + " символов");
                }
            }
        }
        static void MakeDictionary()
        {
            translator.Add('A', "А");
            translator.Add('B', "Б");
            translator.Add('V', "В");
            translator.Add('G', "Г");
            translator.Add('D', "Д");
            translator.Add('E', "Е");
            translator.Add('J', "Ж");
            translator.Add('Z', "З");
            translator.Add('I', "И");
            translator.Add('K', "К");
            translator.Add('L', "Л");
            translator.Add('M', "М");
            translator.Add('N', "Н");
            translator.Add('O', "О");
            translator.Add('P', "П");
            translator.Add('R', "Р");
            translator.Add('S', "С");
            translator.Add('T', "Т");
            translator.Add('U', "У");
            translator.Add('F', "Ф");
            translator.Add('H', "Х");
            translator.Add('C', "Ц");
            translator.Add('Q', "КУ");
            translator.Add('W', "У");
            translator.Add('X', "КС");
            translator.Add('Y', "Й");
        }
        static async void StartReplace()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            Task t1 = Task.Run(() => Replace("121-0.txt"));
            Task t2 = Task.Run(() => Replace("1727-0.txt"));
            Task t3 = Task.Run(() => Replace("4200-0.txt"));
            Task t4 = Task.Run(() => Replace("58975-0.txt"));
            Task t5 = Task.Run(() => Replace("pg972.txt"));
            Task t6 = Task.Run(() => Replace("pg3207.txt"));
            Task t7 = Task.Run(() => Replace("pg19942.txt"));
            Task t8 = Task.Run(() => Replace("pg27827.txt"));
            Task t9 = Task.Run(() => Replace("pg43936.txt"));
            await Task.WhenAll(new[] { t1, t2, t3, t4, t5, t6, t7, t8, t9 });
            timer.Stop();
            Console.WriteLine($"Всего потрачено: {timer.Elapsed} времени");
        }
        static void Main(string[] args)
        {

            MakeDictionary();
            //StartReplace();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string url = "https://www.gutenberg.org/files/1661/1661-0.txt";
            string response;
            using (var webClient = new WebClient())
            {
                response = webClient.DownloadString(url);
            }
            Replace(response, "_book_from_web.txt");
            timer.Stop();
            Console.WriteLine($"Всего потрачено: {timer.Elapsed} времени");
            Console.ReadLine();
        }
    }
}
