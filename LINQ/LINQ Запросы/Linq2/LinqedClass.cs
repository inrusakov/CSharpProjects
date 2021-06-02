using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Languages { get; set; }
        public User()
        {
            Languages = new List<string>();
        }
    }
}
