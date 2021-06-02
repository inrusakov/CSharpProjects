using HSEApiTraining.Providers;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HSEApiTraining.Bot.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => @"/hello";

        public override bool Contains(Message message)
        {
            return message.Text.Contains(this.Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient, ISQLiteConnectionProvider sqliteConnectionProvider)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(
            chatId: chatId
            ,text: "Hi!");
        }
    }
}
