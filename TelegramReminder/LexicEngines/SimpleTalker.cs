using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramReminder.LexicEngines
{
    internal abstract class SimpleTalker : ILexicEngine
    {
        protected IEnumerable<string> gretingVariants = new List<string>();
        protected IEnumerable<string> subscriptionMessages = new List<string>();
        protected IEnumerable<string> replyPhrases = new List<string>();

        public string GenerateSubscriptionMessage()
        {
            throw new NotImplementedException();
        }

        public string Greetings()
        {
            throw new NotImplementedException();
        }

        public string RandomTextReply(string inputText)
        {
            throw new NotImplementedException();
        }
    }
}
