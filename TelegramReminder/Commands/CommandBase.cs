using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramReminder.Commands
{
    public abstract class CommandBase// : ITelegramCommand
    {
        public string Name = "_name";
        public string RecievingDataPrefix = "#nocallbbackdata#";
        public string Description = "_Описание";
        public bool NeedRegister = true;

        public BotCommand ToApiDto()
        {
            return new BotCommand() { Command = Name, Description = Description };
        }

        abstract public Task<Message> Execute(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken);

        virtual public Task<Message> ExecuteStep(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        virtual public Task ExecuteCallback(ITelegramBotClient botClient, CallbackQuery callbackQuery, MessageHandler handler, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
