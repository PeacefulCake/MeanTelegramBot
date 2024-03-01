using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;
using static System.Net.Mime.MediaTypeNames;
using Telegram.Bot.Types.ReplyMarkups;
using System.Windows.Input;
using TelegramReminder.Commands;
using System.ComponentModel.Design;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace TelegramReminder
{
    public class MessageHandler
    {
        IList<CommandBase> _commands;
        CommandBase _defaultCommand;
        CommandBase _nonCommandReply;

        //AsyncResultHandle<Message> _handle;
        Dictionary<long, CommandBase> _simpleTextResplyWaiters;

        public MessageHandler()
        {
            _simpleTextResplyWaiters = new Dictionary<long, CommandBase>();
            // Блок регистрации команд, которые должны быть доступны.
            _commands = new List<CommandBase>
            {
                new Start(),
                new Hello(),
                new Test(),
                new RemoveTest()
            };

            _nonCommandReply = new SimpleTextReply();
            _defaultCommand = new UnknownCommand();
        }

        public bool ValidateCommandsList(IEnumerable<BotCommand> currentCommands) => ValidateCommandsList(currentCommands.Select(c => c.Command).ToArray());

        public bool ValidateCommandsList(IEnumerable<string> currentCommands)
        {
            var actualCommands = _commands.Where(c => c.NeedRegister).Select(c => c.Name).ToArray();

            if (currentCommands.Count() != actualCommands.Count())
                return false;

            if (actualCommands.Except(currentCommands).Any())
                return false;
            
            return true;
        }

        public IEnumerable<BotCommand> GetBotCommands()
        {
            return _commands.Where(c => c.NeedRegister).Select(c => c.ToApiDto()).ToArray();
        }


        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.CallbackQuery is var callbackQuery && callbackQuery is not null)
            {

                if (string.IsNullOrWhiteSpace(callbackQuery.Data))
                    return;

                var command = _commands.Where(c => callbackQuery.Data.StartsWith(c.RecievingDataPrefix)).FirstOrDefault();
                if (command == null)
                    return;

                await command.ExecuteCallback(botClient, callbackQuery, this, cancellationToken);

            }

            if (update.Message is var message && message is not null)
            {
                if (message.Type != MessageType.Text ||
                    string.IsNullOrWhiteSpace(message.Text))
                    return;

                var command = _nonCommandReply;

                if (message.Text[0] == '/')
                {
                    var commandName = message.Text.Split(' ')[0].Replace("/", "");
                    command = _commands.Where(c => c.Name == commandName).FirstOrDefault(_defaultCommand);
                }
                else
                {
                    if (_simpleTextResplyWaiters.TryGetValue(message.Chat.Id, out var replyCommand))
                    {
                        await replyCommand.ExecuteStep(botClient, message, this, cancellationToken);
                        return;
                    }
                }

                Message sentMessage = await command.Execute(botClient, message, this, cancellationToken);
                
                // если нужно ждать ответное сообщение, то флаг нужно вешать для пользака, и тогда в результате это вообще не обязательно созавать.
                // А для начала вообще проверять знаем ли ользователя и отправлять знакомиться, иначе просто не запускать команды

            }
        }


        public void AwaitForSimpleTextReply(long chatId, CommandBase command)
        {
            _simpleTextResplyWaiters.Add(chatId, command);
        }

        public void StopWaitingReply(long chatId, CommandBase command)
        {
            _simpleTextResplyWaiters.Remove(chatId);
        }

        /// <summary>
        /// Не работает =(. Асинхронное получение ответа, чтобы можно было красиво писать обработчики команд
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /*public async Task<Message> GetReplyMessage(long chatId, CancellationToken cancellationToken)
        {
            _handle = new AsyncResultHandle<Message>();

            var result = await _handle.GetReultAsync(cancellationToken);

            _handle = null;

            return result;
            // Так можно и прикольно сделать. В т.ч. и для коллбэков, но для них лучше простой обработкой организовать. Иначе будет просто дикая паутина из тасок.
            //return await botClient.SendTextMessageAsync(message.Chat.Id, "Как к тебе можно обращаться?");

            // по крутому так конечно var nameMessage = await handler.GetReplyMessage(message.Chat.Id);

        }*/

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            //_logger.Error(exception);
        }


    }
}
