using Dapper;
using HSEApiTraining.Providers;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HSEApiTraining
{
    public interface IMessageRepository
    {
        Task<int> Execute(Update update);
    }
    public class MessageRepository : IMessageRepository
    {
        private readonly ISQLiteConnectionProvider _connectionProvider;

        public MessageRepository(ISQLiteConnectionProvider sqliteConnectionProvider)
        {
            _connectionProvider = sqliteConnectionProvider;
        }
        public async Task<int> Execute(Update update)
        {
            var commands = Bot.Bot.Commands;
            var message = update.Message;
            var botClient = await Bot.Bot.GetBotClientAsync();

            foreach (var command in commands)
            {
                if (command.Contains(message))
                {
                    await command.Execute(message, botClient, _connectionProvider);
                    break;
                }
            }
            return 1;
        }
    }
}
