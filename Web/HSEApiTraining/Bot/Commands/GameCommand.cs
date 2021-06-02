using HSEApiTraining.Providers;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HSEApiTraining.Bot.Commands
{
    public class GameCommand : Command
    {
        public override string Name => @"/game";

        public override bool Contains(Message message)
        {
            return message.Text.Contains(this.Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient, ISQLiteConnectionProvider sqliteConnectionProvider)
        {
            var chatId = message.Chat.Id;

            InlineKeyboardMarkup InlineKeyboard_React = new InlineKeyboardMarkup(new[]
            {
                new InlineKeyboardButton[]
                {
                InlineKeyboardButton.WithUrl("Play", "https://navalcombatgame.herokuapp.com"),
                }
            });

            //await botClient.SendGameAsync(chatId: chatId, "navalcombat", replyMarkup: InlineKeyboard_React);
            await botClient.SendTextMessageAsync(chatId: chatId, text: "⚓ WELCOME! ⚓", replyMarkup: InlineKeyboard_React);
        }
    }
}
