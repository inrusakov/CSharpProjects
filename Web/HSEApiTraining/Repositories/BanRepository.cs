using Dapper;
using HSEApiTraining.Models.Customer;
using HSEApiTraining.Providers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace HSEApiTraining
{
    public interface IBanRepository
    {
        string Ban(AddBanRequest request);
        (IEnumerable<BannedPhone> BannedPhones, string Error) GetAll();
        string DeleteBanned(int id);
        string DeleteAll();
    }

    public class BanRepository : IBanRepository
    {
        private readonly ISQLiteConnectionProvider _connectionProvider;
        public BanRepository(ISQLiteConnectionProvider sqliteConnectionProvider)
        {
            _connectionProvider = sqliteConnectionProvider;
        }

        public string Ban(AddBanRequest request)
        {
            try
            {
                if (request.Phone[0] == '+' && request.Phone.Length <= 13)
                {
                    using (var connection = _connectionProvider.GetDbConnection())
                    {
                        connection.Open();
                        connection.Execute(
                        @"INSERT INTO banned_phone 
                        ( phone ) VALUES 
                        ( @phone);",
                            new { phone = request.Phone });
                    }
                    return null;
                }
                else
                {
                    throw new Exception("Wrong phone format");
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public (IEnumerable<BannedPhone> BannedPhones, string Error) GetAll()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (
                        connection.Query<BannedPhone>(@"
                        SELECT 
                        id as Id,
                        phone as Phone 
                        FROM banned_phone",
                        new { }),
                        null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public string DeleteBanned(int id)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    if ((
                        connection.Query<BannedPhone>(
                        @"SELECT 
                        id as Id,
                        phone as Phone
                        FROM banned_phone 
                        WHERE id = @Id;",
                        new { Id = id }).Count() != 0))
                    {
                        connection.Execute(
                        @"DELETE FROM banned_phone WHERE id = @Id;",
                        new { Id = id });
                    }
                    else
                    {
                        throw new Exception("Banned phone does not exist ");
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
                    @"DELETE FROM banned_phone ",
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
