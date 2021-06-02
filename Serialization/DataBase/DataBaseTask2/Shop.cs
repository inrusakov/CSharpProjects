using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseTask2
{
    class Shop : IEntity
    {
        public long Id { get; }
        
        public string Name { get; }

        public string City { get; }

        public string District { get; }

        public string Country { get; }

        public string Telephone { get; }

        public Shop(long id, string name, string city, 
            string district, string country, string telephone)
        {
            Id = id;
            Name = name;
            City = city;
            District = district;
            Country = country;
            Telephone = telephone;
        }

        public override string ToString()
        {
            return $"id: {Id}," +
                $" Name: {Name}," +
                $" City: {City}," +
                $" District: {District}," +
                $" Country: {Country}," +
                $" Telephone: {Telephone}";
        }
    }
}
