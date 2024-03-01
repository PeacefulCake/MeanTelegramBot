using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramReminder
{
    internal class UserSettings
    {
        // TODO : reminders
        public long ChatId { get; set; }
        public string Name { get; set; }

        public MessageStyle Behavior { get; set; }

        public Periodicity SubsribtionPeriod { get; set; }

        public DateTime? LastSubsribedMessage { get; set; }

        public UserSettings(long chatId, string name)
        {
            ChatId = chatId;
            Name = name;

            Behavior = MessageStyle.Negative;
            SubsribtionPeriod = Periodicity.None;
            LastSubsribedMessage = null;
        }
    }

    enum MessageStyle
    {
        Negative = 0,
        Positive = 1
    }

    enum Periodicity
    {
        None = 0,
        Month = 1,
        Day = 2,
        Hour = 3
    }

}
