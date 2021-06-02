using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace laba
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
        static void Rules()
        {
            Console.WriteLine("В начале игры вы сможете выбрать имена для всех героев");
            Console.WriteLine("Условием завершения боя является, смерть одного из участников боя. \n" +
                "В случае смерти пожирателя  душ уровень  всех  героев  повышается  и  повторяется  бой,\n" +
                "с  уже  новым пожирателем. Если умирает воин или хилер, то игрок считается проигравшим и\n" +
                "игра возвращается в главное меню. Пожиратель душ атакует случайно или способностью, если\n" +
                "она доступна ему по уровню, или обычной атакой.");
            Console.WriteLine("Чтобы продолжить нажмите любую кнопку");
            Console.ReadKey();
            Console.Clear();
        }
        static void FightChoice()
        {
            Console.WriteLine("\n1. Attack by warrior\n2. Attack by healer\n3. Use special warrior ability\n4. Use special healer ability" +
                "\n5. Heal warrior\n6. Heal healer\n");
        }
        static bool Fight(Hero[]heroes)
        {
            int choice;
            int counter = 0;
            while (true)
            {
                if (!(heroes[0].IsAlive()))
                {
                    Console.WriteLine(heroes[0].Name + " was slayed");
                    return false;
                }
                if (!(heroes[1].IsAlive()))
                {
                    Console.WriteLine(heroes[1].Name + " was slayed");
                    return false;
                }
                if (!(heroes[2].IsAlive()))
                {
                    Console.WriteLine("Congratulations, You Won!");
                    Console.WriteLine(heroes[2].Name+" was slayed");
                    return true;
                }
                
                Console.WriteLine(heroes[2].Name + " of " + heroes[2].Level + " level attacked");
                FightChoice();
                Console.WriteLine(heroes[0].Name+" hp = " + heroes[0].Health + "  level = " + heroes[0].Level + "  stamina = "+ heroes[0].StaminaOrMana);
                Console.WriteLine(heroes[1].Name+" hp = " + heroes[1].Health + "  level = " + heroes[1].Level + "  mana = "   + heroes[1].StaminaOrMana);
                Console.WriteLine(heroes[2].Name+" hp = " + heroes[2].Health + "  level = " + heroes[2].Level);
                choice = GetIntInput(1, 6);
                switch (choice)
                {
                    case 1:
                        heroes[0].Action(heroes[2]);
                        break;
                    case 2:
                        heroes[1].Action(heroes[2]);
                        break;
                    case 3:
                        heroes[0].SpecialAbility(heroes[2]);
                        break;
                    case 4:
                        heroes[1].SpecialAbility(heroes[2]);
                        break;
                    case 5:
                        heroes[1].Action(heroes[0]);
                        break;
                    case 6:
                        heroes[1].Action(heroes[1]);
                        break;
                }
                if (Assistant.GetRand(80)|| counter < 1)
                {
                    if (Assistant.GetRand(50))
                    {
                        heroes[2].Action(heroes[1]);
                    }
                    else
                    {
                        heroes[2].Action(heroes[0]);
                    }
                }
                else 
                {
                    if (Assistant.GetRand(50))
                    {
                        heroes[2].SpecialAbility(heroes[1]);
                    }
                    else
                    {
                        heroes[2].SpecialAbility(heroes[0]);
                    }
                }
                Console.WriteLine("WAIT 2 SECONDS");
                Thread.Sleep(2000);
                Console.Clear();
            }
        }
        static void Game()
        {
            string war;
            do
            {
                Console.WriteLine("Enter warrior's name");
                war = Console.ReadLine();
            } while (war == null);
            
            string heal;
            do
            {
                Console.WriteLine("Enter healer's name");
                heal = Console.ReadLine();
            } while (heal == null);

            string enem;
            do
            {
                Console.WriteLine("Enter soul eater's name");
                enem = Console.ReadLine();
            } while (enem == null);
            Console.Clear();
            Hero[] heroes = new Hero[3];
            heroes[0] = new Warrior(war);
            heroes[1] = new Healer(heal);
            heroes[2] = new SoulEater(enem);

            for (int i = 0; i < 10; i++)
            {
                if (Fight(heroes))
                {
                    heroes[0].LevelUp();
                    heroes[1].LevelUp();
                    heroes[2].LevelUp();
                }
                else
                {
                    return;
                }
            }
            return;
        }
        static void Main(string[] args)
        {
            int breaker = 0;
            while (breaker != 1)
            {
                Console.Clear();
                Console.WriteLine("1. Start journey\n2. Rules\n3. Exit");
                int choice1 = GetIntInput(1, 3);
                Console.Clear();
                switch (choice1)
                {
                    case 1:
                        Game();
                        break;
                    case 2:
                        Rules();
                        break;
                    case 3:
                        breaker = 1;
                        break;
                }
            }
        }
    }
}
