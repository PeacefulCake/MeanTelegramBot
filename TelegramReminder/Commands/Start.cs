using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramReminder.Commands
{
    internal class Start : CommandBase
    {
        public Start()
        {
            Name = "start";
            Description = "Дефолтная команда для старта";
            NeedRegister = false;
        }

        override public async Task<Message> Execute(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken)
        {
            handler.AwaitForSimpleTextReply(message.Chat.Id, this);

            return await botClient.SendTextMessageAsync(message.Chat.Id, "Как к тебе можно обращаться?");

            //var replyMessage = await handler.GetReplyMessage(message.Chat.Id, cancellationToken);

            

            // по крутому так конечно var nameMessage = await handler.GetReplyMessage(message.Chat.Id);

        }

        public override async Task<Message> ExecuteStep(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken)
        {
            handler.StopWaitingReply(message.Chat.Id, this);
            return await botClient.SendTextMessageAsync(message.Chat.Id, $"Привет, {message.Text}!");
        }
    }
}
