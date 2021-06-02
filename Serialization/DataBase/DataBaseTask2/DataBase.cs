using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace DataBaseTask2
{
    class DataBase
    {
        private readonly IDictionary<Type, object> _tables = new Dictionary<Type, object>();

        public string Name { get; }

        public DataBase(string name)
        {
            Name = name;
        }

        public void CreateTable<T>() where T : IEntity
        {
            Type tableType = typeof(T);

            if (_tables.ContainsKey(tableType))
                throw new DataBaseException($"Table already exists {tableType.Name}!");

            _tables[tableType] = new List<T>();
        }

        public void InsertInto<T>(IEntityFactory<T> values) where T : IEntity
        {
            Type tableType = typeof(T);

            if (!_tables.ContainsKey(tableType))
                throw new DataBaseException($"Unknown table {tableType.Name}!");

            ((List<T>)_tables[tableType]).Add(values.Instance);
        }

        public IEnumerable<T> Table<T>() where T : IEntity
        {
            Type tableType = typeof(T);

            if (!_tables.ContainsKey(tableType))
                throw new DataBaseException($"Unknown table {tableType.Name}!");

            return (IEnumerable<T>)_tables[tableType];
        }

        public void Serialize()
        {
            for (int i = 0; i < _tables.Count; i++)
            {
                using (StreamWriter sw = new StreamWriter("DB" + _tables.Keys.ToList()[i].Name.ToString() + ".json"))
                {
                    string output = JsonConvert.SerializeObject(_tables.Values.ToList()[i]);
                    sw.WriteLine(output);
                }
            }
        }
        public void Serialize(Type table)
        {
            using (StreamWriter sw = new StreamWriter("DB" + table.Name + ".json"))
            {
                string output = JsonConvert.SerializeObject(_tables.Where(x => x.Key == table).Select(x => x.Value).First());
                sw.WriteLine(output);
            }
        }

        public IEnumerable<T> DeserializeOne<T>() where T : IEntity
        {
            Type tableType = typeof(T);
            using (StreamReader sr = new StreamReader("DB" + tableType.Name + ".json"))
            {
                IEnumerable<T> collection = (IEnumerable<T>)(JsonConvert.DeserializeObject(sr.ReadLine(), typeof(IEnumerable<T>)));
                return collection;
            }
        }

        public void Deserialize()
        {
            if (File.Exists("DBBuyer.json"))
            {
                CreateTable<Buyer>();
                List<Buyer> buyers = DeserializeOne<Buyer>().ToList();
                for (int i = 0; i < buyers.Count; i++)
                {
                    InsertInto(new BuyerFactory(buyers[i].Name, buyers[i].Surname, buyers[i].Adress,
                                                buyers[i].City, buyers[i].District, buyers[i].Country, buyers[i].Zipcode));
                }
            }

            if (File.Exists("DBGood.json"))
            {
                CreateTable<Good>();
                List<Good> goods = DeserializeOne<Good>().ToList();
                for (int i = 0; i < goods.Count; i++)
                {
                    InsertInto(new GoodFactory(goods[i].Name, goods[i].Description, goods[i].Category));
                }
            }

            if (File.Exists("DBShop.json"))
            {
                CreateTable<Shop>();
                List<Shop> shops = DeserializeOne<Shop>().ToList();
                for (int i = 0; i < shops.Count; i++)
                {
                    InsertInto(new ShopFactory(shops[i].Name, shops[i].City, shops[i].District, shops[i].Country, shops[i].Telephone));
                }
            }

            if (File.Exists("DBSale.json"))
            {
                CreateTable<Sale>();
                List<Sale> sales = DeserializeOne<Sale>().ToList();
                for (int i = 0; i < sales.Count; i++)
                {
                    InsertInto(new SaleFactory(sales[i].BuyerId, sales[i].ShopId, sales[i].GoodId, sales[i].Amount, sales[i].Price));
                }
            }
        }
    }
}
