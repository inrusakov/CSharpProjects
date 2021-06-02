using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LabWork
{
    class Program
    {
        static Random rnd = new Random();
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
        /// Считывает из файла первую строку
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Считанная строка</returns>
        static string ReadFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        string text = "";
                        text = sr.ReadLine();
                        return text;
                    }
                }
                else
                {
                    Console.WriteLine("File does not exists ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return "";
        }
        /// <summary>
        /// Записывает в файл строку 
        /// </summary>
        /// <param name="text">Строка для записи</param>
        /// <param name="path">Путь записи</param>
        static void WriteFile(string text, string path)
        {
            if (File.Exists(path))
            {
                File.AppendAllText(path, text);
                Console.WriteLine("Saved");
            }
            else
            {
                Console.WriteLine("File does not exists");
            }
        }
        /// <summary>
        /// Кодирует строку методом пар и выводит ключ
        /// </summary>
        /// <param name="text">Тест для кодирования</param>
        /// <returns>Закодированный текст</returns>
        static string PairCode(string text)
        {
            char[] alf = new char[26];
            char charCount = 'a';
            for (int i = 0; i < 26; i++)
            {
                alf[i] = (char)(charCount + i);
            }
            char[] side1 = new char[13];
            char[] side2 = new char[13];
            int randomer = 26;
            int rand, temp;
            StringBuilder sb = new StringBuilder();
            while (randomer > 0)
            {
                for (int i = 0; i < 13; i++)
                {
                    rand = rnd.Next(0, randomer);
                    side1[i] = alf[rand];
                    temp = alf[rand];
                    alf[rand] = alf[alf.Length - 1];
                    alf[alf.Length - 1] = (char)temp;
                    Array.Resize(ref alf, alf.Length - 1);
                    randomer--;
                }
                for (int i = 0; i < 13; i++)
                {
                    rand = rnd.Next(0, randomer);
                    side2[i] = alf[rand];
                    temp = alf[rand];
                    alf[rand] = alf[alf.Length - 1];
                    alf[alf.Length - 1] = (char)temp;
                    Array.Resize(ref alf, alf.Length - 1);
                    randomer--;
                }
            }
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (text[i] == side1[j])
                    {
                        sb.Append(side2[j]);
                    }
                }
                for (int j = 0; j < 13; j++)
                {
                    if (text[i] == side2[j])
                    {
                        sb.Append(side1[j]);
                    }
                }
            }
            StringBuilder seed = new StringBuilder();
            for (int i = 0; i < 13; i++)
            {
                seed.Append(side1[i]);
            }
            for (int i = 0; i < 13; i++)
            {
                seed.Append(side2[i]);
            }
            Console.WriteLine("Your key= " + seed.ToString());
            return sb.ToString();
        }
        /// <summary>
        /// Декодирует код используя ключ
        /// </summary>
        /// <param name="text">Текст для декодирования</param>
        /// <param name="key">Ключ для расшифровки</param>
        /// <returns>Расшифрованный текст</returns>
        static string PairDeCode(string text, string key)
        {
            char[] side1 = new char[13];
            char[] side2 = new char[13];
            StringBuilder sb = new StringBuilder();
            int counter = 13;
            for (int i = 0; i < 13; i++)
            {
                side1[i] = key[i];
            }
            for (int i = 0; i < 13; i++)
            {
                side2[i] = key[counter];
                counter++;
            }
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (text[i] == side1[j])
                    {
                        sb.Append(side2[j]);
                    }
                }
                for (int j = 0; j < 13; j++)
                {
                    if (text[i] == side2[j])
                    {
                        sb.Append(side1[j]);
                    }
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// Кодирует текст методом цезаря
        /// </summary>
        /// <param name="word">Текст для шифрования</param>
        /// <returns>Зашифрованный текст</returns>
        static string RomeCode(string word)
        {
            int shift = rnd.Next(1, 10);
            Console.WriteLine("Your key -" + shift);
            //Оптимизация для времени 
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < word.Length; i++)
            {
                int pos = word[i] + (shift % 26);
                if (pos > 'z')
                {
                    pos = 'a' + (pos - 'z') - 1;
                }
                if (pos > ' ' && pos < 'a')
                {
                    pos = 'z' - ('a' - pos) + 1;
                }
                sb.Append((char)pos);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Декодирует текст 
        /// </summary>
        /// <param name="word">зашифрованный текст</param>
        /// <param name="key">Ключ дешифрования</param>
        /// <returns>Расшифрованный текст</returns>
        static string RomeDeCode(string word, int key)
        {
            int shift = key;
            //Оптимизация для времени 
            StringBuilder sb = new StringBuilder();
            if (word != "")
            {
                for (int i = 0; i < word.Length; i++)
                {
                    int pos = word[i] - (shift % 26);
                    if (pos > 'z')
                    {
                        pos = 'a' + (pos - 'z') - 1;
                    }
                    if (pos < 'a')
                    {
                        pos = 'z' - ('a' - pos) + 1;
                    }
                    sb.Append((char)pos);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// Проверка ключа дешифрования для метода пар
        /// </summary>
        /// <param name="key">ключ </param>
        /// <returns>правильный или нет</returns>
        static bool CheckPareKey(string key)
        {
            if (key.Length == 26)
            {
                for (int i = 0; i < 26; i++)
                {
                    if (key[i] < 'a' || key[i] > 'z')
                    {
                        Console.WriteLine("Incorrect key");
                        return false;
                    }
                }
                Console.WriteLine("Correct key");
                return true;
            }
            Console.WriteLine("Incorrect key");
            return false;
        }
        /// <summary>
        /// Проверяет, можно ли использовать строку для шифрования 
        /// </summary>
        /// <param name="text">строка</param>
        /// <returns>Можно -true, нет-false</returns>
        static bool CheckString(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] < 'a' || text[i] > 'z')
                {
                    return false;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            string text = "null";
            Console.WriteLine("Welcome" +
                "\nПрограмма принимает на вход строку состоящую только из латинские буквы нижнего регистра" +
                " не содержащую пробелов" +
                "\nИз файла программа считывает только первую строку");
            while (true)
            {
                PrintMenu();
                int menu = GetIntInput(1, 9);
                int key;
                switch (menu)
                {
                    case 1:
                        Console.WriteLine("Input path - ");
                        string pathInput = Console.ReadLine();
                        string check = ReadFile(pathInput);
                        if (check == "" || !CheckString(check))
                        {
                            Console.WriteLine("Incorrect text");
                        }
                        else
                        {
                            Console.WriteLine("Correct");
                            text = check;
                        }
                        break;
                    case 2:
                        Console.WriteLine(text);
                        break;
                    case 4:
                        Console.WriteLine("Enter key(from 1 to 10) - ");
                        key = GetIntInput(1, 10);
                        text = RomeDeCode(text, key);
                        Console.WriteLine(text);
                        break;
                    case 3:
                        text = RomeCode(text);
                        Console.WriteLine("Your crypted text - " + text);
                        break;
                    case 6:
                        Console.WriteLine("Enter your key(26 latin symbols) - ");
                        string keyPa = Console.ReadLine();
                        if (CheckPareKey(keyPa))
                        {
                            Console.WriteLine("Decoded text - ");
                            text = PairDeCode(text, keyPa);
                            Console.WriteLine(text);
                        }
                        else
                        {
                            Console.WriteLine("There are some troubles");
                        }
                        break;
                    case 5:
                        text = PairCode(text);
                        Console.WriteLine("Your code - " + text);
                        break;
                    case 7:
                        Console.WriteLine("Enter path - ");
                        string pathOutput = Console.ReadLine();
                        WriteFile(text, pathOutput);
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    case 9:
                        Console.WriteLine("U sure?");
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Метод рисует меню
        /// </summary>
        static void PrintMenu()
        {
            Console.WriteLine("1. Read all text from");
            Console.WriteLine("2. Print");
            Console.WriteLine("3. Rom encrypt");
            Console.WriteLine("4. Rom decrypt");
            Console.WriteLine("5. Pair encrypt");
            Console.WriteLine("6. Pair decrypt");
            Console.WriteLine("7. Save in");
            Console.WriteLine("8. Exit");
            Console.WriteLine("9. Clear console");
        }
    }
}
