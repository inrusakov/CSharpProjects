using Dapper;
using HSEApiTraining.Models.Customer;
using HSEApiTraining.Providers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace HSEApiTraining
{
    public interface ICustomerRepository
    {
        (IEnumerable<Customer> Customers, string Error) GetCustomers(int count);
        (IEnumerable<Customer> Customers, string Error) GetAll();
        (IEnumerable<Customer> Customers, string Error) GetBanned();
        (IEnumerable<Customer> Customers, string Error) GetNotBanned();
        (IEnumerable<Customer> Customers, string Error) SearchByName(string name);
        (IEnumerable<Customer> Customers, string Error) SearchBySurname(string surname);
        string AddCustomer(AddCustomerRequest request);
        string GenerateRandomCustomers(int count);
        string DeleteCustomer(int id);
        string DeleteAll();
        string UpdateCustomer(int id, UpdateCustomerRequest request);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ISQLiteConnectionProvider _connectionProvider;
        public CustomerRepository(ISQLiteConnectionProvider sqliteConnectionProvider)
        {
            _connectionProvider = sqliteConnectionProvider;
        }

        public static Random rnd = new Random();

        public static List<string> names = new List<string> {
            "Andrey",   "Boris",  "Carl",
            "Daniel",   "Emma",   "Frank",
            "Gleb",     "Hannah", "Irina",
            "Jack",
            "Smith",  "Ivanov",   "Stepanov",
            "Lamar",  "Hanks",    "Kuznetsov",
            "Dudarev","Grinkrug", "Silakov",
            "Putin"};

        public static AddCustomerRequest GenerateAddCustomerRequest()
        {
            AddCustomerRequest request = new AddCustomerRequest()
            {
                Name = string.Empty,
                PhoneNumber = string.Empty,
                Surname = string.Empty
            };
            switch (rnd.Next(1, 5))
            {
                case 1:
                    request.PhoneNumber = "+7" + rnd.Next(10000000, 100000000);
                    break;
                case 2:
                    request.PhoneNumber = "+0" + rnd.Next(10000000, 100000000);
                    break;
                case 3:
                    request.PhoneNumber = "+380" + rnd.Next(10000000, 100000000);
                    break;
                case 4:
                    request.PhoneNumber = "+1" + rnd.Next(10000000, 100000000);
                    break;
            }
            request.Name = names[rnd.Next(0, 10)];
            request.Surname = names[rnd.Next(9, 20)];
            return request;
        }

        public (IEnumerable<Customer> Customers, string Error) GetCustomers(int count)
        {
            try
            {
                if (count >= 0)
                {
                    using (var connection = _connectionProvider.GetDbConnection())
                    {
                        connection.Open();
                        return (
                            connection.Query<Customer>(@"
                        SELECT 
                        id as Id,
                        name as Name, 
                        surname as Surname, 
                        phone_number as PhoneNumber 
                        FROM Customer 
                        LIMIT @count",
                            new { count = count }),
                            null);
                    }
                }
                else
                {
                    throw new Exception("Count cannot be less than 0");
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public (IEnumerable<Customer> Customers, string Error) GetAll()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (
                        connection.Query<Customer>(@"
                        SELECT 
                        id as Id,
                        name as Name, 
                        surname as Surname, 
                        phone_number as PhoneNumber 
                        FROM Customer",
                        new { }),
                        null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public (IEnumerable<Customer> Customers, string Error) GetBanned()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (
                        connection.Query<Customer>(@"
                        SELECT 
                        id as Id,
                        name as Name, 
                        surname as Surname, 
                        phone_number as PhoneNumber 
                        FROM Customer 
                        WHERE phone_number 
                        IN (SELECT phone 
                        FROM banned_phone)",
                        new { }),
                        null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public (IEnumerable<Customer> Customers, string Error) GetNotBanned()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (
                        connection.Query<Customer>(@"
                        SELECT 
                        id as Id,
                        name as Name, 
                        surname as Surname, 
                        phone_number as PhoneNumber 
                        FROM Customer 
                        WHERE phone_number 
                        NOT IN (SELECT phone 
                        FROM banned_phone)",
                        new { }),
                        null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public (IEnumerable<Customer> Customers, string Error) SearchByName(string name)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (
                        connection.Query<Customer>(@"
                        SELECT *
                        FROM Customer
                        WHERE name 
                        LIKE @text",
                        new { text = "%" + name + "%" }),
                        null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public (IEnumerable<Customer> Customers, string Error) SearchBySurname(string surname)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (
                        connection.Query<Customer>(@"
                        SELECT *
                        FROM Customer
                        WHERE surname 
                        LIKE @text",
                        new { text = "%" + surname + "%" }),
                        null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public string AddCustomer(AddCustomerRequest request)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    connection.Execute(
                        @"INSERT INTO Customer 
                        ( name, surname, phone_number ) VALUES 
                        ( @Name, @Surname, @PhoneNumber );",
                        new { Name = request.Name, Surname = request.Surname, PhoneNumber = request.PhoneNumber });
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string GenerateRandomCustomers(int count)
        {
            try
            {
                for (int i = 0; i < count; i++)
                {
                    AddCustomerRequest request = GenerateAddCustomerRequest();
                    using (var connection = _connectionProvider.GetDbConnection())
                    {
                        connection.Open();
                        connection.Execute(
                            @"INSERT INTO Customer 
                        ( name, surname, phone_number ) VALUES 
                        ( @Name, @Surname, @PhoneNumber );",
                            new { Name = request.Name, Surname = request.Surname, PhoneNumber = request.PhoneNumber });
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteCustomer(int id)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    if (connection.Execute(
                        @"DELETE FROM Customer WHERE id = @Id;",
                        new { Id = id }) == 0)
                    {
                        throw new Exception("Customer does not exist ");
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteAll()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    connection.Execute(
                    @"DELETE FROM Customer ",
                    new { });
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateCustomer(int id, UpdateCustomerRequest request)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    if ((
                        connection.Query<Customer>(
                        @"SELECT 
                        id as Id,
                        name as Name, 
                        surname as Surname, 
                        phone_number as PhoneNumber 
                        FROM Customer 
                        WHERE id = @Id;",
                        new { Id = id }).Count() != 0))
                    {
                        connection.Execute(
                        @"UPDATE Customer 
                        SET name = @Name, surname = @Surname, phone_number = @PhoneNumber
                        WHERE id = @Id;",
                        new { Name = request.Name, Surname = request.Surname, PhoneNumber = request.PhoneNumber, Id = id });
                    }
                    else
                    {
                        throw new Exception("Customer does not exist ");
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
