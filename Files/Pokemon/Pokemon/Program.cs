using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Threading;

namespace Pokemon
{
    class Program
    {
        /// <summary>
        /// Метод для чтения листа покемонов из файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        static List<Pokemon> GetPokemons(string path)
        {
            List<Pokemon> PokeList = new List<Pokemon>();
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                do
                {
                    string temp = sr.ReadLine();
                    int char2 = temp.LastIndexOf("]");
                    int char1 = temp.IndexOf("[");
                    string[] abilities = (((((temp.Substring(char1, char2)).Replace(" ", "")).Replace("[", "")).Replace("]", "")).Replace("'", "")).Split(',');
                    string all = temp.Substring(char2, temp.Length - char2);
                    if (temp[0] == '\"')
                    {
                        all = all.Remove(0, 3);
                    }
                    else
                    {
                        all = all.Remove(0, 2);
                    }
                    string[] full = all.Split(',');
                    for (int i = 0; i < full.Length; i++)
                    {
                        if (full[i] == "")
                        {
                            full[i] = "0";
                        }
                    }
                    Pokemon Poke = new Pokemon
                    {
                        abilities = abilities,
                        against_bug = decimal.Parse(full[0], CultureInfo.InvariantCulture),
                        against_dark = decimal.Parse(full[1], CultureInfo.InvariantCulture),
                        against_dragon = decimal.Parse(full[2], CultureInfo.InvariantCulture),
                        against_electric = decimal.Parse(full[3], CultureInfo.InvariantCulture),
                        against_fairy = decimal.Parse(full[4], CultureInfo.InvariantCulture),
                        against_fight = decimal.Parse(full[5], CultureInfo.InvariantCulture),
                        against_fire = decimal.Parse(full[6], CultureInfo.InvariantCulture),
                        against_flying = decimal.Parse(full[7], CultureInfo.InvariantCulture),
                        against_ghost = decimal.Parse(full[8], CultureInfo.InvariantCulture),
                        against_grass = decimal.Parse(full[9], CultureInfo.InvariantCulture),
                        against_ground = decimal.Parse(full[10], CultureInfo.InvariantCulture),
                        against_ice = decimal.Parse(full[11], CultureInfo.InvariantCulture),
                        against_normal = decimal.Parse(full[12], CultureInfo.InvariantCulture),
                        against_poison = decimal.Parse(full[13], CultureInfo.InvariantCulture),
                        against_psychic = decimal.Parse(full[14], CultureInfo.InvariantCulture),
                        against_rock = decimal.Parse(full[15], CultureInfo.InvariantCulture),
                        against_steel = decimal.Parse(full[16], CultureInfo.InvariantCulture),
                        against_water = decimal.Parse(full[17], CultureInfo.InvariantCulture),
                        attack = decimal.Parse(full[18], CultureInfo.InvariantCulture),
                        base_egg_steps = decimal.Parse(full[19], CultureInfo.InvariantCulture),
                        base_happiness = decimal.Parse(full[20], CultureInfo.InvariantCulture),
                        base_total = decimal.Parse(full[21], CultureInfo.InvariantCulture),
                        capture_rate = full[22],
                        classfication = full[23],
                        defense = decimal.Parse(full[24], CultureInfo.InvariantCulture),
                        experience_growth = decimal.Parse(full[25], CultureInfo.InvariantCulture),
                        height_m = decimal.Parse(full[26], CultureInfo.InvariantCulture),
                        hp = decimal.Parse(full[27], CultureInfo.InvariantCulture),
                        japanese_name = full[28],
                        name = full[29],
                        percentage_male = decimal.Parse(full[30], CultureInfo.InvariantCulture),
                        pokedex_number = decimal.Parse(full[31], CultureInfo.InvariantCulture),
                        sp_attack = decimal.Parse(full[32], CultureInfo.InvariantCulture),
                        sp_defense = decimal.Parse(full[33], CultureInfo.InvariantCulture),
                        speed = decimal.Parse(full[34], CultureInfo.InvariantCulture),
                        type1 = full[35],
                        type2 = full[36],
                        weight_kg = decimal.Parse(full[37], CultureInfo.InvariantCulture),
                        generation = decimal.Parse(full[38], CultureInfo.InvariantCulture),
                        is_legendary = decimal.Parse(full[39], CultureInfo.InvariantCulture)
                    };
                    PokeList.Add(Poke);
                } while (!sr.EndOfStream);
                return PokeList;
            }
        }

        /// <summary>
        /// Метод для записи команды покемонов в файл
        /// </summary>
        /// <param name="team">Команда для записи</param>
        /// <param name="path">Путь к файлу</param>
        static void WritePokemons(PokemonTeam team, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var pokemon in team.team)
                {
                    sw.WriteLine($"{AbilitiesString(pokemon.abilities)}," +
                        $"{pokemon.against_bug},{pokemon.against_dark}," +
                        $"{pokemon.against_dragon},{pokemon.against_electric}," +
                        $"{pokemon.against_fairy},{pokemon.against_fight}," +
                        $"{pokemon.against_fire},{pokemon.against_flying}," +
                        $"{pokemon.against_ghost},{pokemon.against_grass}," +
                        $"{pokemon.against_ground},{pokemon.against_ice}," +
                        $"{pokemon.against_normal},{pokemon.against_poison}," +
                        $"{pokemon.against_psychic},{pokemon.against_rock}," +
                        $"{pokemon.against_steel},{pokemon.against_water}," +
                        $"{pokemon.attack},{pokemon.base_egg_steps}," +
                        $"{pokemon.base_happiness},{pokemon.base_total}," +
                        $"{pokemon.capture_rate},{pokemon.classfication}," +
                        $"{pokemon.defense},{pokemon.experience_growth}," +
                        $"{pokemon.height_m},{pokemon.hp}," +
                        $"{pokemon.japanese_name},{pokemon.name}," +
                        $"{pokemon.percentage_male},{pokemon.pokedex_number}," +
                        $"{pokemon.sp_attack},{pokemon.sp_defense}," +
                        $"{pokemon.speed},{pokemon.type1}," +
                        $"{pokemon.type2},{pokemon.weight_kg}," +
                        $"{pokemon.generation},{pokemon.is_legendary}");
                }
            }
        }

        /// <summary>
        /// Вспомогательный метод для представлении массива строк, как одной строки для записи в файл 
        /// </summary>
        /// <param name="abs">Массив строк</param>
        /// <returns>Целую строку</returns>
        static string AbilitiesString(string[] abs)
        {
            string abils = "\"[";
            for (int i = 0; i < abs.Length; i++)
            {
                abils += (abs[0] + ", ");
            }
            abils += "]\"";
            return abils;
        }

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
        /// Вывод в консоль первого меню
        /// </summary>
        static void PrintMenu()
        {
            Console.WriteLine("1. Load Pokemons from csv file");
            Console.WriteLine("2. Show teams");
            Console.WriteLine("3. Create a team");
            Console.WriteLine("4. Move from one to to another");
            Console.WriteLine("5. Dismiss a team");
            Console.WriteLine("6. Write a team");
            Console.WriteLine("7. EXIT");
        }

        /// <summary>
        /// Вывод в консоль второго меню
        /// </summary>
        static void PrintMenu2()
        {
            Console.WriteLine("1. Amount");
            Console.WriteLine("2. Type1");
            Console.WriteLine("3. IsLegendary");
        }

        /// <summary>
        /// Выводит типы распределения в консоль
        /// </summary>
        static void PrintTypes()
        {
            Console.WriteLine("bug dark dragon " +
                "electric fairy fighting " +
                "fire flying ghost grass " +
                "ground ice normal poison " +
                "psychic rock steel water");
        }

        /// <summary>
        /// Вспомогательный метод для того чтобы пользователь выбрал команду
        /// </summary>
        /// <param name="team1">команда 1</param>
        /// <param name="team2">команда 2</param>
        /// <param name="team3">команда 3</param>
        /// <param name="team4">команда 4</param>
        /// <param name="team5">команда 5</param>
        /// <returns>Выбранная команда</returns>
        static PokemonTeam TeamGetter(PokemonTeam team1,
            PokemonTeam team2,
            PokemonTeam team3,
            PokemonTeam team4,
            PokemonTeam team5)
        {
            int choice = GetIntInput(1, 5);
            switch (choice)
            {
                case 1:
                    return team1;
                case 2:
                    return team2;
                case 3:
                    return team3;
                case 4:
                    return team4;
                case 5:
                    return team5;
            }
            return null;
        }

        /// <summary>
        /// Выводит информацию о командах
        /// </summary>
        /// <param name="free">Свободная команда</param>
        /// <param name="team1">команда 1</param>
        /// <param name="team2">команда 2</param>
        /// <param name="team3">команда 3</param>
        /// <param name="team4">команда 4</param>
        /// <param name="team5">команда 5</param>
        static void PrintTeams(PokemonTeam free, PokemonTeam team1,
            PokemonTeam team2, PokemonTeam team3,
            PokemonTeam team4, PokemonTeam team5)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Free Team has {free.team.Count}");
            Console.ResetColor();
            foreach (var item in free) { Console.WriteLine(item); }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Team 1 has {team1.team.Count}");
            Console.ResetColor();
            foreach (var item in team1) { Console.WriteLine(item); }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Team 2 has {team2.team.Count}");
            Console.ResetColor();
            foreach (var item in team2) { Console.WriteLine(item); }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Team 3 has {team3.team.Count}");
            Console.ResetColor();
            foreach (var item in team3) { Console.WriteLine(item); }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Team 4 has {team4.team.Count}");
            Console.ResetColor();
            foreach (var item in team4) { Console.WriteLine(item); }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Team 5 has {team5.team.Count}");
            Console.ResetColor();
            foreach (var item in team5) { Console.WriteLine(item); }
        }

        /// <summary>
        /// Метод для заполнения одной из команд покемонами из свободной команды
        /// </summary>
        /// <param name="team"> команда для заполнения</param>
        /// <param name="freeteam"> свободная команда</param>
        /// <param name="choice"> выбор метода распределения пользователя</param>
        /// <returns></returns>
        static PokemonTeam TeamFiller(ref PokemonTeam team, ref PokemonTeam freeteam, int choice)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Enter amount of pokemons from 1 to {freeteam.team.Count}");
                    int amnt = GetIntInput(1, freeteam.team.Count + 1);
                    freeteam.SendPokemons(team, freeteam.Take(amnt).ToList());
                    break;
                case 2:
                    Console.WriteLine("Enter type");
                    PrintTypes();
                    string type = Console.ReadLine();
                    try
                    {
                        freeteam.SendPokemons(team, freeteam.Where(x => x.type1.Contains($"{type}")).ToList());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 3:
                    Console.WriteLine("Do u want it to be legendary?  Y/N");
                    string answer = Console.ReadLine();
                    try
                    {
                        if (answer == "Y")
                        {
                            freeteam.SendPokemons(team, freeteam.Where(x => x.is_legendary == 1).ToList());
                        }
                        else if (answer == "N")
                        {

                        }
                        else
                        {
                            Console.WriteLine("Wrong answer");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
            }
            return team;
        }

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            List<Pokemon> freelist = new List<Pokemon>();
            List<Pokemon> list1 = new List<Pokemon>()
                , list2 = new List<Pokemon>()
                , list3 = new List<Pokemon>()
                , list4 = new List<Pokemon>()
                , list5 = new List<Pokemon>();
            PokemonTeam freeteam = new PokemonTeam(freelist)
                , team1 = new PokemonTeam(list1) { number = 1 }
                , team2 = new PokemonTeam(list2) { number = 2 }
                , team3 = new PokemonTeam(list3) { number = 3 }
                , team4 = new PokemonTeam(list4) { number = 4 }
                , team5 = new PokemonTeam(list5) { number = 5 };
            do
            {
                Console.Clear();
                //Меню
                Console.WriteLine("What do you want to do?");
                PrintMenu();
                int choice = GetIntInput(1, 7);
                switch (choice)
                {
                    //Чтение в команду листа покемонов
                    case 1:
                        try
                        {
                            Console.WriteLine("Enter file name (don't miss .csv)");
                            string path = Console.ReadLine();
                            Console.WriteLine("Which team do you want to fill? 0.free team 1-5.other teams");
                            int fillchoice = GetIntInput(0, 5);
                            switch (fillchoice)
                            {
                                case 0:
                                    freeteam = new PokemonTeam(GetPokemons(path));
                                    break;
                                case 1:
                                    team1 = new PokemonTeam(GetPokemons(path));
                                    break;
                                case 2:
                                    team2 = new PokemonTeam(GetPokemons(path));
                                    break;
                                case 3:
                                    team3 = new PokemonTeam(GetPokemons(path));
                                    break;
                                case 4:
                                    team4 = new PokemonTeam(GetPokemons(path));
                                    break;
                                case 5:
                                    team5 = new PokemonTeam(GetPokemons(path));
                                    break;
                            }
                        }
                        catch (IOException)
                        {
                            Console.WriteLine("There is some problem with file, probably it doesn't exists");
                        }
                        break;

                    //Просто вывод информации о всех командах
                    case 2:
                        PrintTeams(freeteam, team1, team2, team3, team4, team5);
                        break;

                    //Перевод из свободный команды в любую
                    case 3:
                        Console.WriteLine("Enter the factor of dividing");
                        PrintMenu2();
                        int choice2 = GetIntInput(1, 3);
                        Console.WriteLine("Enter num of team which you want to fill from freeteam");
                        int teamchoice = GetIntInput(1, 5);
                        switch (teamchoice)
                        {
                            case 1:
                                TeamFiller(ref team1, ref freeteam, choice2);
                                break;
                            case 2:
                                TeamFiller(ref team2, ref freeteam, choice2);
                                break;
                            case 3:
                                TeamFiller(ref team3, ref freeteam, choice2);
                                break;
                            case 4:
                                TeamFiller(ref team4, ref freeteam, choice2);
                                break;
                            case 5:
                                TeamFiller(ref team5, ref freeteam, choice2);
                                break;
                        }
                        break;
                    
                    //Перевод из одной команды в другую
                    case 4:
                        Console.WriteLine("Enter the factor of dividing");
                        PrintMenu2();
                        int choicemove = GetIntInput(1, 3);
                        Console.WriteLine("Enter num of team where you want to take pokemons");
                        PokemonTeam teamfrom = TeamGetter(team1, team2, team3, team4, team5);
                        Console.WriteLine("Enter num of team which you want to fill");
                        PokemonTeam teamto = TeamGetter(team1, team2, team3, team4, team5);
                        TeamFiller(ref teamto, ref teamfrom, choicemove);
                        break;

                    //Роспуск команды
                    case 5:
                        Console.WriteLine("Which team do you want to dismiss?");
                        PokemonTeam teamtodiss = TeamGetter(team1, team2, team3, team4, team5);
                        while (teamtodiss.Count() != 0)
                        {
                            teamtodiss.SendPokemons(freeteam, teamtodiss.team);
                        }
                        Console.WriteLine($"team{teamtodiss} dismissed");
                        break;

                    //Запись команды в файл
                    case 6:
                        Console.WriteLine("Which team do you want to write down? 0.freeteam 1-5.other teams");
                        PokemonTeam teamwrite = TeamGetter(team1, team2, team3, team4, team5);
                        Console.WriteLine("Where do you want it to write down (don't miss .csv)?");
                        string pathsw = Console.ReadLine();
                        WritePokemons(teamwrite, pathsw);
                        break;

                    //Выход
                    case 7:
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine("To continue press any key, to exit press ESC");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
