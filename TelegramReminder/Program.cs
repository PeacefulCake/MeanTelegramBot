using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramReminder;

var tbc = new TelegramBotClient("");
//var me = await tbc.GetMeAsync();
/*await tbc.SendTextMessageAsync(
    new Telegram.Bot.Types.ChatId(195422219),
    "������ ����, ����� ��� ����?"
    );*/
//https://telegrambots.github.io/book/3/updates/index.html


var handler = new MessageHandler();

var comms = await tbc.GetMyCommandsAsync();
if (!handler.ValidateCommandsList(comms))
    await tbc.SetMyCommandsAsync(handler.GetBotCommands());

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = new[] { UpdateType.Message, UpdateType.CallbackQuery },
    ThrowPendingUpdates = true
};

tbc.StartReceiving(
    handler.HandleUpdateAsync,
    handler.HandleErrorAsync,
    receiverOptions
    );


// ���� ��� ����������� � �����������.
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();



await host.RunAsync();


// 1. /remind - ������� "�� ���� ����� ����� ��� <...>"
// 2. OnStart - ValidateCommandsList(), ���������� ����� �������, ������ ��� ���������, ������ �����, � ������� ��� ������������� �� ������������.
// 3 /subscribe - ������ ���������� �����������
// 4. ����� �������� ������������
// 5. ������� ���� ��� ��������
// 6. ���� ������� ��� �� � API GPT'���, ����� ������ ����, ���� � ������� ���� � ����� ��� ������� �����
// 7. ��� ������������ ��������� ��������� ����, ������� ����� ��������. ������� ������, ����� ����� � ������ ����������
// 8. �� � ��� ���������� ������ �����, �� ����� �� ��������� �������