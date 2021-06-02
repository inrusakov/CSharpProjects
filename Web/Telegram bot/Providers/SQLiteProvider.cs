using Dapper;
using HSEApiTraining.Models.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data.SQLite;

namespace HSEApiTraining.Providers
{
    public interface ISQLiteConnectionProvider
    {
        SQLiteConnection GetDbConnection();
    }
    public class SQLiteConnectionProvider : ISQLiteConnectionProvider
    {
  
        private readonly string _connectionString;

        public SQLiteConnectionProvider(IOptions<DbConnectionOptions> dbOptions)
        {
            _connectionString = dbOptions.Value.ConnectionString; 
            using (var connection = GetDbConnection())
            {
                var createCustomerTableCommand =
                   @"CREATE TABLE IF NOT EXISTS players
                   (
                     id                             integer primary key AUTOINCREMENT,
                     name                           varchar(100) not null,
                     nick                           varchar(100) not null,
                     chatId                         varchar(100) not null
                   )";
                connection.Execute(createCustomerTableCommand);
                createCustomerTableCommand =
                   @"CREATE TABLE IF NOT EXISTS sessions
                   (
                     id                             integer primary key AUTOINCREMENT,
                     result                         varchar(100) not null
                   )";
                connection.Execute(createCustomerTableCommand);
            }
        }

        public SQLiteConnection GetDbConnection() 
            => new SQLiteConnection(_connectionString);
    }
}
