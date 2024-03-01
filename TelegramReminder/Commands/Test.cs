using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramReminder.Commands
{
    internal class Test : CommandBase
    {
        public Test()
        {
            Name = "test";
            Description = "Для тестов";
            RecievingDataPrefix = "TESTCALLBACK.";
        }

        override public async Task<Message> Execute(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken)
        {
            /*var keyboard = new[]
                {
                        new KeyboardButton[] { "1.1", "1.2" },
                        new KeyboardButton[] { "2.1", "2.2" },
                };

            ReplyKeyboardMarkup replyKeyboardMarkup = new(keyboard)
            {
                ResizeKeyboard = true
            };
            

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Choose",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);*/
            await botClient.SendChatActionAsync(
                chatId: message.Chat.Id,
                chatAction: ChatAction.Typing,
                cancellationToken: cancellationToken);

            // Simulate longer running task
            await Task.Delay(500, cancellationToken);

            InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1.1", RecievingDataPrefix + "11"),
                        InlineKeyboardButton.WithCallbackData("1.2", RecievingDataPrefix + "12"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("2.1", RecievingDataPrefix + "21"),
                        InlineKeyboardButton.WithCallbackData("2.2", RecievingDataPrefix + "22"),
                    },
                });

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Choose",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }

        public override async Task ExecuteCallback(ITelegramBotClient botClient, CallbackQuery callbackQuery, MessageHandler handler, CancellationToken cancellationToken)
        {
            var justData = callbackQuery.Data.Remove(0, RecievingDataPrefix.Length);

            await botClient.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: $"Received {callbackQuery.Data}",
                cancellationToken: cancellationToken);

            await botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: $"Received {justData}",
                cancellationToken: cancellationToken);
        }
    }

    internal class RemoveTest : CommandBase
    {
        public RemoveTest()
        {
            Name = "remove";
            Description = "Убрать что было для тестов";
        }

        override public async Task<Message> Execute(ITelegramBotClient botClient, Message message, MessageHandler handler, CancellationToken cancellationToken)
        {
            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Removing keyboard",
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }
    }
}
