using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseTask2
{
    class BuyerFactory :IEntityFactory<Buyer>
    {
        private static long _id = 0;

        private string _name;

        private string _surname;

        private string _adress;

        private string _city;

        private string _district;

        private string _country;

        private long _zipcode;

        public BuyerFactory(string name, string surname,
                     string adress, string city, string district,
                     string country, long zipcode)
        {
            _name = name;
            _surname = surname;
            _adress = adress;
            _city = city;
            _district = district;
            _country = country;
            _zipcode = zipcode;
        }

        public BuyerFactory(bool renew)
        {
            _id = 0;
        }

        public Buyer Instance => new Buyer(_id++,_name,_surname,_adress,_city,_district,_country,_zipcode);
    }
}
