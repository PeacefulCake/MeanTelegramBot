using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramReminder.LexicEngines
{
    internal interface ILexicEngine
    {
        string Greetings();

        string RandomTextReply(string inputText);

        string GenerateSubscriptionMessage();
    }
}
