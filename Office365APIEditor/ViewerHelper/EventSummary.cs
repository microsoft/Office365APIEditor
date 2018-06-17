// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Microsoft.Office365.OutlookServices;
using System;
using System.Collections.Generic;

namespace Office365APIEditor.ViewerHelper
{
    class EventSummary
    {
        //e.Id, e.Subject, e.Organizer, e.Attendees, e.Start, e.End, e.IsAllDay, e.CreatedDateTime
        public string Id;
        public string Subject;
        public Recipient Organizer;
        public IList<Attendee> Attendees;
        public DateTimeTimeZone Start;
        public DateTimeTimeZone End;
        public bool? IsAllDay;
        public DateTimeOffset? CreatedDateTime;
    }
}
