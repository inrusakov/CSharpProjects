using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>
            {
                new User {Name="Том", Age=23, Languages = new List<string> {"английский", "немецкий" }},
                new User {Name="Боб", Age=27, Languages = new List<string> {"английский", "французский" }},
                new User {Name="Джон", Age=29, Languages = new List<string> {"английский", "испанский" }},
                new User {Name="Элис", Age=24, Languages = new List<string> {"испанский", "немецкий" }}
            };

            var selectedUsers = from user in users //1 способ
                                where user.Age > 25
                                select user; 

            var selectedUsers2 = users.Where(u => u.Age > 25);//2 способ

            var selectedUsers3 = from user in users //3 способ с усиленным фильтром
                                from lang in user.Languages
                                where user.Age < 28
                                where lang == "английский" || lang == "испанский"
                                select user;

            var selectedUsers4 = users.SelectMany(u => u.Languages,//4 способ
                            (u, l) => new { User = u, Lang = l })
                          .Where(u => u.Lang == "английский" && u.User.Age < 28)
                          .Select(u => u.User);

            foreach (User user in selectedUsers)
                Console.WriteLine($"{user.Name} - {user.Age} {user.Languages[0]} {user.Languages[1]}");
            Console.WriteLine();
            foreach (User user in selectedUsers2)
                Console.WriteLine($"{user.Name} - {user.Age} {user.Languages[0]} {user.Languages[1]}");
            Console.WriteLine();
            foreach (User user in selectedUsers3)
                Console.WriteLine($"{user.Name} - {user.Age} {user.Languages[0]} {user.Languages[1]}");
            foreach (User user in selectedUsers4)
                Console.WriteLine($"{user.Name} - {user.Age} {user.Languages[0]} {user.Languages[1]}");

            Console.ReadKey();
        }
    }
}
