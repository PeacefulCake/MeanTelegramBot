using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramReminder.Commands
{
    internal class SimpleTextReply : CommandBase
    {
        public SimpleTextReply()
        {
            Name = "==simpletextreply==";
            Description = "Ответ на текстовое сообщение";
        }

        override public async Task<Message> Execute(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken)
        {
            return await botClient.SendTextMessageAsync(message.Chat.Id, "Сам ты " + message.Text);
        }
    }
}
