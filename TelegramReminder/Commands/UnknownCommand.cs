using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramReminder.Commands
{
    internal class UnknownCommand : CommandBase
    {
        public UnknownCommand()
        {
            Name = "==unknowncommandreply==";
            Description = "Ответ на несуществующую команду";
        }

        override public async Task<Message> Execute(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken)
        {
            return await botClient.SendTextMessageAsync(message.Chat.Id, "Что за хуйню ты пишешь? Нет такоё команды, дебилушка!");
        }
    }
}
