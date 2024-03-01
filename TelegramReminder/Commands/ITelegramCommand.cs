using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramReminder.Commands
{
    internal interface ITelegramCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool NeedRegister { get; set; }

        public BotCommand ToApiDto();

        public Task<Message> Execute(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken);

        public Task<Message> ExecuteStep(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken);
    }
}
