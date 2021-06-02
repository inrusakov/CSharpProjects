﻿using HSEApiTraining.Providers;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace HSEApiTraining.Bot.Commands
{
    public class RulesCommand : Command
    {
        public override string Name => @"/rules";

        public override bool Contains(Message message)
        {
            return message.Text.Contains(this.Name);
        }

        public override async Task Execute(Message message, TelegramBotClient botClient, ISQLiteConnectionProvider sqliteConnectionProvider)
        {
            var chatId = message.Chat.Id;
            await botClient.SendTextMessageAsync(chatId,
                "Battleship (also Battleships or Sea Battle[1]) is a strategy type guessing game for two players." +
                " It is played on ruled grids (paper or board) on which each player's fleet of ships (including battleships) are marked." +
                " The locations of the fleets are concealed from the other player." +
                " Players alternate turns calling \"shots\" at the other player's ships, and the objective of the game is to destroy the opposing player's fleet."+
                "Battleship is known worldwide as a pencil and paper game which dates from World War I.It was published by various companies as a pad - and - pencil game in the 1930s," +
                " and was released as a plastic board game by Milton Bradley in 1967.The game has spawned electronic versions, video games, smart device apps and a film. "
                , 
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
        }
    }
}
