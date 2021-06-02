using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseTask2
{
    class Buyer :IEntity
    {
		public long Id { get; } 

        public string Name { get; }

        public string Surname { get; }

        public string Adress { get; }

        public string City { get; }

        public string District { get; }

        public string Country { get; }

        public long Zipcode { get; }

        public Buyer(long id, string name, string surname, 
                     string adress, string city, string district, 
                     string country, long zipcode)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Adress = adress;
            City = city;
            District = district;
            Country = country;
            Zipcode = zipcode;
        }

        public override string ToString()
        {
            return $"id: {Id}," +
                $" Name: {Name}," +
                $" Surname: {Surname}," +
                $" City: {City}," +
                $" District: {District}," +
                $" Country: {Country}"+
                $" ZIP: {Zipcode}";
        }
    }
}
