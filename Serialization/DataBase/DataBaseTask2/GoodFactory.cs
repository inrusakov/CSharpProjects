using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseTask2
{
    class GoodFactory : IEntityFactory<Good>
    {
        private static long _id = 0;

        private string _name;

        private string _description;

        private string _category;

        public GoodFactory(string name, string description, string category)
        {
            _name = name;
            _description = description;
            _category = category;
        }

        public GoodFactory(bool renew)
        {
            _id = 0;
        }

        public Good Instance => new Good(_id++, _name, _description, _category);
    }
}
