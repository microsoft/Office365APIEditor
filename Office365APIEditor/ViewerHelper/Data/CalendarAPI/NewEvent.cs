using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Office365APIEditor.ViewerHelper.Data.MailAPI;

namespace Office365APIEditor.ViewerHelper.Data.CalendarAPI
{
    class NewEvent
    {
        public string Subject;
        public MailAddressCollection RequiredAttendees;
        public MailAddressCollection OptionalAttendees;
        public MailAddressCollection ResourceAttendees;
        public string Location;
        public bool IsAllDay;
        public DateTimeAndTimeZone Start;
        public DateTimeAndTimeZone End;
        public BodyType BodyType;
        public string Body;
        public PatternedRecurrence Recurrence;
    }
}
