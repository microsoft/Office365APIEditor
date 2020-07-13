// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data
{
    [DataContract]
    public class DateTimeAndTimeZone
    {
        [DataMember(Name = "dateTime")]
        public string DateTime;

        [DataMember(Name = "timeZone")]
        public string TimeZone;

        public override string ToString()
        {
            return $"{DateTime} ({TimeZone})";
        }

        public DateTime ToUniversalTime()
        {
            try
            {
                TimeZoneInfo originalTimeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
                DateTime parsedDateTime = System.DateTime.Parse(DateTime);
                return TimeZoneInfo.ConvertTimeToUtc(parsedDateTime, originalTimeZone);
            }
            catch
            {
                return System.DateTime.MinValue;
            }
        }
    }
}