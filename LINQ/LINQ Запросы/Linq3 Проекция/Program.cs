using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq3_Проекция
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>()
            {
                new User { Name = "Sam", Age = 43 },
                new User { Name = "Tom", Age = 33 }
            };
            List<Phone> phones = new List<Phone>()
            {
                new Phone {Name="Lumia 630", Company="Microsoft",YearOfbuy = 2018},
                new Phone {Name="iPhone 6", Company="Apple", YearOfbuy = 2019},
            };

            var people = from user in users //Проекция одного в другой 
                         from phone in phones
                         select new { Name = user.Name, Phone = phone.Name, 
                             Age = user.Age, YearOfBuy = phone.YearOfbuy};

            foreach (var p in people)
                Console.WriteLine($"{p.Name} - {p.Phone} - age = {p.Age} yearof = {p.YearOfBuy}");

            Console.ReadKey();
        }
    }
}
