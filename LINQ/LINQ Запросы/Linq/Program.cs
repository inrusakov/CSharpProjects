using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] teams = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            var selectedTeams = from t in teams // определяем каждый объект из teams как t
                                where t.ToUpper().StartsWith("Б") //фильтрация по критерию
                                orderby t  // упорядочиваем по возрастанию
                                select t; // выбираем объект

            string[] teams2 = { "Бавария", "Боруссия", "Реал Мадрид", "Манчестер Сити", "ПСЖ", "Барселона" };
            var selectedTeams2 = teams2.Where(t => t.ToUpper().StartsWith("Р")).OrderBy(t => t);
            int counter = (from t in teams2 where t.Contains("ал") select t).Count( );// считать количество строк с ал

            int[] elems = { 1, 3, 5, 234, 32, 325, 235, 263, 43, 234, 6, 2346 };
            var selectedInts = from t in elems where t > 10 orderby t select t; // Числа больше 10
            elems.OrderByDescending(s => s); //Отсортировать по возрастанию
            var selectedInts2 = elems.Where(t => t<50).OrderBy(t => t);

            foreach (var item in selectedTeams)
            {
                Console.WriteLine(item);
            }
            foreach (var item in selectedTeams2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Amount of P words = " + counter);
            foreach (var item in selectedInts)
            {
                Console.WriteLine(item);
            }
            foreach (var item in selectedInts2)
            {
                Console.WriteLine(item);
            }







            Console.ReadKey();
        }
    }
}
