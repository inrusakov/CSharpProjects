using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Palindrome
{
    class MainClass
    {
        public static string palindromeSearch(string input)
        {
            // Substrings and amount of palindromes.
            Dictionary<string, int> values = new Dictionary<string, int>();
            int leng = input.Length + 1;

            // Results table.
            int[,] table = new int[2, leng + 1];

            // Find all substring using "guards".
            input = "*" + input + "+" + "*";

            // Using Manacher's algorithm.
            for (int j = 0; j <= 1; j++)
            {
                int palRad = 0;
                table[j, 0] = 0;
                int i = 1;
                while (i <= leng)
                {
                    while (input[i - palRad - 1] == input[i + j + palRad])
                    {
                        palRad++;
                    }

                    table[j, i] = palRad;
                    int k = 1;

                    while ((table[j, i - k] != palRad - k) && k < palRad)
                    {
                        table[j, i + k] = Math.Min(table[j, i - k], palRad - k);
                        k++;
                    }
                    palRad = Math.Max(palRad - k, 0);
                    i += k;
                }
            }
            // Remove guards.
            input = input.Substring(1);

            // Add palindromes in dictionary.
            for (int i = 0; i < leng; i++)
            {
                if (!values.ContainsKey(input.Substring(i, 1)))
                {
                    values.Add(input.Substring(i, 1), 1);
                }
                else
                {
                    values[input.Substring(i, 1)]++;
                }

                for (int j = 0; j <= 1; j++)
                    for (int rp = table[j, i]; rp > 0; rp--)
                    {
                        if (!values.ContainsKey(input.Substring(i - rp - 1, 2 * rp + j)))
                            values.Add(input.Substring(i - rp - 1, 2 * rp + j), 1);

                        else
                            values[input.Substring(i - rp - 1, 2 * rp + j)]++;
                    }
            }

            string result = string.Empty;
            int summ = -1;
            foreach (var item in values.Values)
            {
                summ += item;
            }
            result += summ + " ";

            int even = 0;
            foreach (var item in values.Keys.Where(x => x.Length % 2 == 0))
            {
                even += values[item];
            }
            result += even + " ";

            int odd = -1;
            foreach (var item in values.Keys.Where(x => x.Length % 2 == 1))
            {
                odd += values[item];
            }
            result += odd;

            return result;
        }

        static int Main(string[] args)
        {
            Console.WriteLine(palindromeSearch("asasasasa"));
            Console.Read();
            return 1;

            //if (args.Length != 2)
            //{
            //    Console.WriteLine("Please enter a string argument.");
            //    Console.WriteLine(@"Usage: .\Program test1.txt answer1.txt");
            //    return 1;
            //}
            //string path1 = @"input\" + args[0];
            //string path2 = @"output\" + args[1];
            //string input = string.Empty, output = string.Empty;
            //try
            //{
            //    using (StreamReader sr = new StreamReader(path1))
            //    {
            //        input = sr.ReadToEnd();
            //    }
            //    using (StreamReader sr = new StreamReader(path2))
            //    {
            //        output = sr.ReadToEnd();
            //    }
            //}
            //catch (IOException e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //if (input.Length != 0 || output.Length != 0)
            //{
            //    input = input.ToLower().Trim();
            //}
            //else
            //{
            //    Console.WriteLine("Wrong input/output params");
            //    return 1;
            //}

            //if (palindromeSearch(input) == output)
            //{
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine("Correct");
            //    Console.ResetColor();
            //    return 1;
            //}
            //else
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("ERROR");
            //    Console.ResetColor();

            //    Console.WriteLine($"Input: {input}");
            //    Console.WriteLine($"Output expected: {output}  Your output: {palindromeSearch(input)}");
            //    return 0;
            //}
        }
    }
}
