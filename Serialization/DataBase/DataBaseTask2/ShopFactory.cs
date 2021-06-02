using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseTask2
{
    class ShopFactory : IEntityFactory<Shop>
    {
        private static long _id = 0;

        private string _name;

        private string _city;

        private string _district;

        private string _country;

        private string _telephone;

        public ShopFactory(string name, string city,
            string district, string country, string telephone)
        {
            _name = name;
            _city = city;
            _district = district;
            _country = country;
            _telephone = telephone;
        }

        public ShopFactory(bool renew)
        {
            _id = 0;
        }

        public Shop Instance => new Shop(_id++, _name, _city, _district, _country, _telephone);
    }
}
