// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using Office365APIEditor.ViewerHelper.Data.MailAPI;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data.CalendarAPI
{
    [DataContract]
    class NewEvent
    {
        public NewEvent()
        {
            Attendees = new List<Attendee>();
            Location = new Location();
            Body = new ItemBody();
        }

        [DataMember(Name = "subject")]
        public string Subject;

        [DataMember(Name = "attendees")]
        public List<Attendee> Attendees;

        [DataMember(Name = "location")]
        public Location Location;

        [DataMember(Name = "isAllDay")]
        public bool IsAllDay;

        [DataMember(Name = "start")]
        public DateTimeAndTimeZone Start;

        [DataMember(Name = "end")]
        public DateTimeAndTimeZone End;

        [DataMember(Name = "body")]
        public ItemBody Body;

        [DataMember(Name = "recurrence")]
        public PatternedRecurrence Recurrence;

        [DataMember(Name = "isOnlineMeeting", EmitDefaultValue = false)]
        public bool IsOnlineMeeting;

        [DataMember(Name = "onlineMeetingProvider", EmitDefaultValue = false)]
        public string OnlineMeetingProvider;

        public Sensitivity Sensitivity;

        [DataMember(Name = "sensitivity", EmitDefaultValue = false)]
        private string SensitivityString
        {
            get { return Sensitivity.ToString().ToLower(); }
            set { Sensitivity = (Sensitivity)Enum.Parse(typeof(Sensitivity), value, true); }
        }
    }
}
