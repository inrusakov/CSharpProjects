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
        string DeleteCustomer(int id);
        string DeleteAll();
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly ISQLiteConnectionProvider _connectionProvider;
        public CustomerRepository(ISQLiteConnectionProvider sqliteConnectionProvider)
        {
            _connectionProvider = sqliteConnectionProvider;
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
                        nick as Nickname, 
                        chatId as PhoneNumber 
                        FROM players 
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
                        nick as Nickname, 
                        chatId as PhoneNumber 
                        FROM players",
                        new { }),
                        null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
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
                        @"DELETE FROM players WHERE id = @Id;",
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
                    @"DELETE FROM players ",
                    new { });
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
