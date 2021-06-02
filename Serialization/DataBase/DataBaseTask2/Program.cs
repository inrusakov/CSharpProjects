using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBaseTask2
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
        /// Генератор строки.
        /// </summary>
        /// <param name="length"> Длина строки</param>
        /// <returns> Случайно генерированную строку </returns>
        static string StringGenerator(int length)
        {
            string str = string.Empty;
            if (length >= 1)
            {
                str += (char)rnd.Next('A', 'Z');
                for (int i = 0; i < length - 1; i++)
                {
                    str += (char)rnd.Next('a', 'z');
                }
            }
            return str;
        }

        /// <summary>
        /// Добавление случайных сущностей.
        /// </summary>
        /// <param name="db"> База для добавления </param>
        static void InsertRandomEntity(ref DataBase db)
        {
            Console.WriteLine("Which entity do you want to add?" +
                "\r\n 1. Buyers \r\n 2.Shops \r\n 3.Sales \r\n 4. Goods");
            int choice = GetIntInput(1, 4);
            Console.WriteLine("How many? (from 1 to 100)");
            int amount = GetIntInput(1, 100);
            switch (choice)
            {
                case 1:
                    for (int i = 0; i < amount; i++)
                    {
                        db.InsertInto(new BuyerFactory(
                            StringGenerator(rnd.Next(1, 13)),
                            StringGenerator(rnd.Next(1, 13)),
                            StringGenerator(rnd.Next(1, 11)) + rnd.Next(1, 100),
                            StringGenerator(rnd.Next(1, 13)),
                            StringGenerator(rnd.Next(1, 13)),
                            StringGenerator(rnd.Next(1, 13)),
                            rnd.Next(100000, 999999)));
                    }
                    break;
                case 2:
                    for (int i = 0; i < amount; i++)
                    {
                        db.InsertInto(new ShopFactory(
                            StringGenerator(rnd.Next(1, 13)),
                            StringGenerator(rnd.Next(1, 13)),
                            StringGenerator(rnd.Next(1, 11)),
                            StringGenerator(rnd.Next(1, 13)),
                            "+7" + rnd.Next(10000000, 99999999)));
                    }
                    break;
                case 3:
                    if (db.Table<Buyer>().Count() > 0
                        && db.Table<Good>().Count() > 0
                        && db.Table<Shop>().Count() > 0)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            db.InsertInto(new SaleFactory(
                                rnd.Next(0, db.Table<Buyer>().Count()),
                                rnd.Next(0, db.Table<Shop>().Count()),
                                rnd.Next(0, db.Table<Good>().Count()),
                                rnd.Next(1, 10),
                                rnd.Next(1, 100)));
                        }
                    }
                    else
                    {
                        throw new Exception("Your DB do not have some tables!");
                    }
                    break;
                case 4:
                    for (int i = 0; i < amount; i++)
                    {
                        db.InsertInto(new GoodFactory(
                            StringGenerator(rnd.Next(1, 13)),
                            StringGenerator(rnd.Next(1, 16)),
                            StringGenerator(rnd.Next(1, 11))));
                    }
                    break;
            }
        }

        /// <summary>
        /// Ввести сущность самостоятельно.
        /// </summary>
        /// <param name="db">База данных</param>
        static void InsertEnitity(ref DataBase db)
        {
            Console.WriteLine("Which entity do you want to add?" +
                "\r\n 1. Buyers \r\n 2. Shops \r\n 3. Sales \r\n 4. Goods");
            int choice = GetIntInput(1, 4);
            switch (choice)
            {
                case 1:
                    {
                        Console.WriteLine("Enter name: ");
                        string name = null;
                        while (name == null)
                        {
                            name = Console.ReadLine();
                        }
                        Console.WriteLine("Enter surname: ");
                        string surname = null;
                        while (surname == null)
                        {
                            surname = Console.ReadLine();
                        }
                        Console.WriteLine("Enter adress: ");
                        string adress = null;
                        while (adress == null)
                        {
                            adress = Console.ReadLine();
                        }
                        Console.WriteLine("Enter city: ");
                        string city = null;
                        while (city == null)
                        {
                            city = Console.ReadLine();
                        }
                        Console.WriteLine("Enter district: ");
                        string district = null;
                        while (district == null)
                        {
                            district = Console.ReadLine();
                        }
                        Console.WriteLine("Enter country: ");
                        string country = null;
                        while (country == null)
                        {
                            country = Console.ReadLine();
                        }
                        Console.WriteLine("Enter Zip which has 6 digits: ");
                        int zip = GetIntInput(100000, 999999);
                        db.InsertInto(new BuyerFactory(name, surname, adress, city, district, country, zip));
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter name: ");
                        string name = null;
                        while (name == null)
                        {
                            name = Console.ReadLine();
                        }
                        Console.WriteLine("Enter city: ");
                        string city = null;
                        while (city == null)
                        {
                            city = Console.ReadLine();
                        }
                        Console.WriteLine("Enter district: ");
                        string district = null;
                        while (district == null)
                        {
                            district = Console.ReadLine();
                        }
                        Console.WriteLine("Enter country: ");
                        string country = null;
                        while (country == null)
                        {
                            country = Console.ReadLine();
                        }
                        Console.WriteLine("Enter telephone: ");
                        string telephone = null;
                        while (telephone == null)
                        {
                            telephone = Console.ReadLine();
                        }
                        db.InsertInto(new ShopFactory(name,city,district,country,telephone));
                        break;
                    }
                case 3:
                    {
                        if (db.Table<Buyer>().Count() > 0
                            && db.Table<Good>().Count() > 0
                            && db.Table<Shop>().Count() > 0)
                        {
                            Console.WriteLine("Enter buyerID: ");
                            long buyerID = GetIntInput(0, db.Table<Buyer>().Count());
                            Console.WriteLine("Enter shopID: ");
                            long shopID = GetIntInput(0, db.Table<Shop>().Count());
                            Console.WriteLine("Enter goodID: ");
                            long goodID = GetIntInput(0, db.Table<Good>().Count());
                            Console.WriteLine("Enter amount (from 1 to 100): ");
                            long amount = GetIntInput(1, 100);
                            Console.WriteLine("Enter price (from 0 to 1000)");
                            long price = GetIntInput(0, 1000);
                            db.InsertInto(new SaleFactory(buyerID,shopID,goodID,amount,price));
                        }
                        else
                        {
                            throw new Exception("Your DB do not have some tables!");
                        }
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Enter name: ");
                        string name = null;
                        while (name == null)
                        {
                            name = Console.ReadLine();
                        }
                        Console.WriteLine("Enter description: ");
                        string description = null;
                        while (description == null)
                        {
                            description = Console.ReadLine();
                        }
                        Console.WriteLine("Enter category: ");
                        string category = null;
                        while (category == null)
                        {
                            category = Console.ReadLine();
                        }
                        db.InsertInto(new GoodFactory(name, description, category));
                        break;
                    }
            }
        }

        /// <summary>
        /// Метод с меню.
        /// </summary>
        /// <param name="db"> База данных</param>
        /// <returns> 1 если все успешно, 0 если программа должна закрыться</returns>
        static int Menu(ref DataBase db)
        {
            Console.Clear();
            Console.WriteLine("What do you want to do?" +
                "\r\n 1. Serialize \r\n 2. Create Empty \r\n 3. Show tables \r\n 4. Show LINQ \r\n 5. Deserialize \r\n 6. Add Entity \r\n 7. Exit");
            switch (GetIntInput(1, 7))
            {
                case 1:
                    Console.WriteLine("Which one?" + "\r\n 1. Sales \r\n 2. Shops \r\n 3. Goods \r\n 4. Buyers \r\n 5. All");
                    int ser = GetIntInput(1, 5);
                    switch (ser)
                    {
                        case 1:
                            db.Serialize(typeof(Sale));
                            break;
                        case 2:
                            db.Serialize(typeof(Shop));
                            break;
                        case 3:
                            db.Serialize(typeof(Good));
                            break;
                        case 4:
                            db.Serialize(typeof(Buyer));
                            break;
                        case 5:
                            db.Serialize();
                            break;
                    }
                    Console.WriteLine("Data Base serialized");
                    break;
                case 2:
                    db = null;
                    db = new DataBase("ShopDataBase");
                    db.CreateTable<Good>();
                    db.CreateTable<Shop>();
                    db.CreateTable<Buyer>();
                    db.CreateTable<Sale>();
                    break;
                case 3:
                    Console.WriteLine("Which table do you want to see?" +
                "\r\n 1. Sales \r\n 2. Shops \r\n 3. Goods \r\n 4. Buyers");
                    switch (GetIntInput(1, 4))
                    {
                        case 1:
                            foreach (var item in db.Table<Sale>().ToList())
                            {
                                Console.WriteLine(item);
                            }
                            break;
                        case 2:
                            foreach (var item in db.Table<Shop>().ToList())
                            {
                                Console.WriteLine(item);
                            }
                            break;
                        case 3:
                            foreach (var item in db.Table<Good>().ToList())
                            {
                                Console.WriteLine(item);
                            }
                            break;
                        case 4:
                            foreach (var item in db.Table<Buyer>().ToList())
                            {
                                Console.WriteLine(item);
                            }
                            break;
                    }
                    break;
                case 4:
                    ShowLinq(db);
                    break;
                case 5:
                    GoodFactory goodFactory = new GoodFactory(true);
                    BuyerFactory buyerFactory = new BuyerFactory(true);
                    ShopFactory shopFactory = new ShopFactory(true);
                    SaleFactory saleFactory = new SaleFactory(true);
                    db = null;
                    db = new DataBase("ShopDataBase");
                    db.Deserialize();
                    Console.WriteLine("Data Base deserialized");
                    break;
                case 6:
                    Console.WriteLine("Do you want to insert Entity yourself or make it randomly? \r\n 1. Myself \r\n 2. Randomly");
                    int ent = GetIntInput(1,2);
                    switch (ent)
                    {
                        case 1:
                            InsertEnitity(ref db);
                            break;
                        case 2:
                            InsertRandomEntity(ref db);
                            break;
                    }
                    Console.WriteLine("Enitity added");
                    break;
                case 7:
                    return 0;
            }
            return 1;
        }

        /// <summary>
        /// Показать все Linq запросы
        /// </summary>
        /// <param name="db">База данных</param>
        static void ShowLinq(DataBase db)
        {
            // LINQ 1. DONE
            {
                Console.WriteLine("Linq 1");
                var GoodsBought = db.Table<Sale>()
                    .ToList()
                    .Select(x => new { id = x.GoodId, buyer = x.BuyerId });
                var maxNameLengthId = db.Table<Buyer>()
                    .Where(x => x.Name.Length ==
                        db.Table<Buyer>()
                        .OrderBy(y => y.Name.Length)
                        .Reverse()
                        .Select(y => y.Name.Length)
                        .FirstOrDefault())
                    .Select(x => x.Id);
                List<long> goodsByLongestName = new List<long>();
                for (int i = 0; i < maxNameLengthId.Count(); i++)
                {
                    foreach (var item in GoodsBought
                    .Where(x => x.buyer == maxNameLengthId.ToList()[i])
                    .Select(x => x.id))
                    {
                        goodsByLongestName.Add(item);
                    }
                }
                Console.WriteLine("Goods bought by buyer with longest name: ");
                for (int i = 0; i < goodsByLongestName.Count(); i++)
                {
                    Console.WriteLine(db.Table<Good>()
                        .Where(x => x.Id == goodsByLongestName[i])
                        .Select(x => x.Name)
                        .FirstOrDefault());
                }
            }

            // LINQ 2. DONE
            {
                var mostExpensive = db.Table<Sale>()
                    .OrderBy(x => x.Price)
                    .Reverse()
                    .Select(x => x.Price)
                    .First();
                List<long> idWithHighestPrice = new List<long>();
                for (int i = 0; i < db.Table<Sale>().Count(); i++)
                {
                    if (db.Table<Sale>().ToList()[i].Price == mostExpensive)
                    {
                        idWithHighestPrice.Add(db.Table<Sale>().ToList()[i].GoodId);
                    }
                }
                Console.WriteLine("\r\nLinq 2 \r\nMost expensive category: ");
                for (int i = 0; i < idWithHighestPrice.Count(); i++)
                {
                    Console.WriteLine(
                    db.Table<Good>()
                    .Where(x => x.Id == idWithHighestPrice.ToList()[i])
                    .Select(x => x.Category)
                    .FirstOrDefault());
                }
            }

            // LINQ 3. DONE
            {
                Console.WriteLine("\r\nLinq 3");
                var shopIdWithSold = db.Table<Sale>()
                    .Select(x => new { id = x.ShopId, sold = (int)(x.Price * x.Amount) })
                    .OrderBy(x => x.sold)
                    .GroupBy(x => x.id)
                    .Select(x => x.ToList())
                    .ToList();
                var shopIdSold = shopIdWithSold
                    .Select(x => new { id = x.First().id, all = x.Select(y => y.sold).Sum() })
                    .ToList();
                string soldMinCity = string.Empty;
                long soldMinAll = long.MaxValue;
                for (int i = 0; i < shopIdSold.Count(); i++)
                {
                    long soldByOne = 0;
                    string soldCity = string.Empty;
                    for (int j = 0; j < db.Table<Shop>().Count(); j++)
                    {
                        if (db.Table<Shop>().ToList()[j].City == db.Table<Shop>()
                            .Where(x => x.Id == shopIdSold[i].id)
                            .FirstOrDefault().City)
                        {
                            soldByOne += shopIdSold[i].all;
                            soldCity = db.Table<Shop>().ToList()[j].City;
                        }
                    }
                    if (soldByOne < soldMinAll)
                    {
                        soldMinCity = soldCity;
                        soldMinAll = soldByOne;
                    }
                }
                Console.WriteLine($"City with lowest income: {soldMinCity}, with income: {soldMinAll}");
            }

            // LINQ 4. DONE
            {
                Console.WriteLine("\r\nLinq 4");
                var mostBuyerGoods = db.Table<Sale>()
                    .OrderBy(x => x.Amount)
                    .GroupBy(x => x.GoodId)
                    .ToList();
                long mostBuyedId = 0, maxAmount = long.MinValue;
                for (int i = 0; i < mostBuyerGoods.ToList().Count(); i++)
                {
                    long buyAmount = 0;
                    for (int j = 0; j < mostBuyerGoods[i].ToList().Count(); j++)
                    {
                        buyAmount += mostBuyerGoods[i].ToList()[j].Amount;
                    }
                    if (maxAmount < buyAmount)
                    {
                        maxAmount = buyAmount;
                        mostBuyedId = mostBuyerGoods[i].ToList().First().GoodId;
                    }
                }
                var buyers = db.Table<Sale>()
                    .Where(x => x.GoodId == mostBuyedId)
                    .Select(x => x.BuyerId)
                    .ToList();
                Console.WriteLine("People surnames who bought the most popular good");
                for (int i = 0; i < db.Table<Buyer>().ToArray().Count(); i++)
                {
                    if (buyers.Contains(db.Table<Buyer>().ToList()[i].Id))
                    {
                        Console.WriteLine(db.Table<Buyer>().ToList()[i]);
                    }
                }
            }

            // LINQ 5. DONE
            {
                Console.WriteLine("\r\nLinq 5");
                var diffCountries = db.Table<Shop>()
                    .GroupBy(x => x.Country)
                    .OrderBy(x => x.Count())
                    .Select(x => new { count = x });
                Console.Write("Country with lowes amount of shops: " + diffCountries.ToList()[0].count.Key + " ");
                Console.WriteLine(diffCountries.ToList()[0].count.Count());
            }

            // LINQ 6. DONE
            {
                Console.WriteLine("\r\nLinq 6");
                Console.WriteLine("Enter buyer id: ");
                long buyerId = GetIntInput(0, db.Table<Buyer>().Count());
                var buyerCity = db.Table<Buyer>()
                    .Where(x => x.Id == buyerId)
                    .Select(x => x.City)
                    .FirstOrDefault();
                var buyerSales = db.Table<Sale>()
                    .Where(x => x.BuyerId == buyerId)
                    .ToList();
                List<Sale> shopList = new List<Sale>();
                for (int i = 0; i < db.Table<Shop>().ToList().Count(); i++)
                {
                    for (int j = 0; j < buyerSales.Count(); j++)
                    {
                        if (db.Table<Shop>().ToList()[i].Id == buyerSales[j].ShopId
                            && db.Table<Shop>().ToList()[i].City != buyerCity)
                        {
                            shopList.Add(buyerSales[j]);
                            Console.WriteLine(buyerSales[j]);
                        }
                    }
                }
            }

            // LINQ 7. DONE
            {
                Console.WriteLine("\r\nLinq 7");
                long allSold = db.Table<Sale>().Sum(x => (x.Price * x.Amount));
                Console.WriteLine($"All income: {allSold}");
            }
        }

        static void Main(string[] args)
        {
            DataBase db = new DataBase("ShopDataBase");
            int exitCode = 1;
            while (exitCode == 1)
            {
                try
                {
                    exitCode = Menu(ref db);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadLine();
                }
            }
        }
    }
}