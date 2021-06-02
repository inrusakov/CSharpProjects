using Dapper;
using HSEApiTraining.Providers;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HSEApiTraining.Bot.Commands
{
    public class StartCommand : Command
    {
        public override string Name => @"/start";

        public override bool Contains(Message message)
        {
            return message.Text.Contains(this.Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient, ISQLiteConnectionProvider sqliteConnectionProvider)
        {
            var chatId = message.Chat.Id;

            using (var connection = sqliteConnectionProvider.GetDbConnection())
            {
                connection.Open();
                if ((
                        connection.Execute(
                        @"SELECT 
                        id as Id
                        FROM players 
                        WHERE id = @Id;",
                        new { Id = message.From.Id }))!=0)
                {
                    connection.Execute(
                        @"DELETE FROM players WHERE id = @Id;",
                        new { Id = message.From.Id });
                    connection.Execute(
                    @"INSERT INTO players 
                        ( id, name, nick, chatId ) VALUES 
                        ( @Id, @Name, @Nickname, @ChatId );",
                    new
                    {
                        Id = message.From.Id,
                        Name = message.From.FirstName,
                        Nickname = message.From.Username,
                        ChatId = message.Chat.Id
                    });
                }
            }

            await botClient.SendTextMessageAsync(chatId, 
                "Welcome to Naval Combat! " +
                "\n You can send me /rules command to see how to play this game"
                , parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }
    }
}
