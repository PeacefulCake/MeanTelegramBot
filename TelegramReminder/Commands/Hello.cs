using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramReminder.Commands
{
    internal class Hello : CommandBase
    {
        public Hello()
        {
            Name = "hello";
            Description = "Дружеское приветствие =)";
        }

        override public async Task<Message> Execute(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken)
        {
            return await botClient.SendTextMessageAsync(message.Chat.Id, "Привет, пидор!");
        }

    }
}
