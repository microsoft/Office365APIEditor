// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data.CalendarAPI
{
    [DataContract]
    public class PatternedRecurrence
    {
        public PatternedRecurrence()
        {
            Pattern = new RecurrencePattern();
            Range = new RecurrenceRange();
        }

        [DataMember(Name = "pattern")]
        public RecurrencePattern Pattern;

        [DataMember(Name = "range")]
        public RecurrenceRange Range;
    }

    [DataContract]
    public class RecurrencePattern
    {
        public RecurrencePattern()
        {
            DaysOfWeek = null;
            FirstDayOfWeek = null;
            Index = null;
        }

        [DataMember(Name = "dayOfMonth", EmitDefaultValue = false)]
        public int DayOfMonth;

        public DayOfWeek[] DaysOfWeek;

        public DayOfWeek? FirstDayOfWeek;

        public WeekIndex? Index;

        [DataMember(Name = "interval")]
        public int Interval;
        
        [DataMember(Name = "month", EmitDefaultValue = false)]
        public int Month;

        public RecurrencePatternType Type;

        [DataMember(Name = "daysOfWeek", EmitDefaultValue = false)]
        private string[] DaysOfWeekString
        {
            get
            {
                if (DaysOfWeek == null)
                {
                    return null;
                }

                List<string> temp = new List<string>();

                foreach (var item in DaysOfWeek)
                {
                    temp.Add(item.ToString().ToLower());
                }

                return temp.ToArray();
            }
            set
            {
                if (value == null)
                {
                    DaysOfWeek = null;
                }
                else
                {
                    List<DayOfWeek> temp = new List<DayOfWeek>();

                    foreach (var item in value)
                    {
                        temp.Add((DayOfWeek)Enum.Parse(typeof(DayOfWeek), item));
                    }

                    DaysOfWeek = temp.ToArray(); ;
                }
            }
        }

        [DataMember(Name = "firstDayOfWeek", EmitDefaultValue = false)]
        private string FirstDayOfWeebString
        {
            get
            {
                return FirstDayOfWeek.HasValue ? FirstDayOfWeek.Value.ToString().ToLower() : null;
            }
            set
            {
                FirstDayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), value);
            }
        }

        [DataMember(Name = "index", EmitDefaultValue = false)]
        private string IndexString
        {
            get
            {
                return Index.HasValue ? Index.Value.ToString().ToLower() : null;
            }
            set
            {
                Index = (WeekIndex)Enum.Parse(typeof(WeekIndex), value);
            }
        }

        [DataMember(Name = "type")]
        private string RecurrencePatternString
        {
            get
            {
                string original = Type.ToString();
                return char.ToLower(original[0]) + original.Substring(1);
            }
            set
            {
                Type = (RecurrencePatternType)Enum.Parse(typeof(RecurrencePatternType), value);
            }
        }


    }

    [DataContract]
    public class RecurrenceRange
    {
        public RecurrenceRange()
        {
            EndDate = null;
        }

        public DateTime? EndDate;

        [DataMember(Name = "numberOfOccurrences", EmitDefaultValue = false)]
        public int NumberOfOccurrences;

        [DataMember(Name = "recurrenceTimeZone")]
        public string RecurrenceTimeZone;

        public DateTime StartDate;

        public RecurrenceRangeType Type;

        [DataMember(Name = "endDate", EmitDefaultValue = false)]
        private string EndDateString
        {
            get
            {
                return EndDate.HasValue ? EndDate.Value.ToString("yyyy-MM-dd") : null;
            }
            set
            {
                EndDate = DateTime.Parse(value);
            }
        }

        [DataMember(Name = "startDate")]
        private string StartDateString
        {
            get
            {
                return StartDate.ToString("yyyy-MM-dd");
            }
            set
            {
                StartDate = DateTime.Parse(value);
            }
        }

        [DataMember(Name = "type")]
        private string RecurrenceRangeTypeString
        {
            get
            {
                string original = Type.ToString();
                return char.ToLower(original[0]) + original.Substring(1);
            }
            set
            {
                Type = (RecurrenceRangeType)Enum.Parse(typeof(RecurrenceRangeType), value);
            }
        }
    }

    [DataContract]
    public enum WeekIndex
    {
        First,
        Second,
        Third,
        Fourth,
        Last
    }

    [DataContract]
    public enum RecurrencePatternType
    {
        Daily,
        Weekly,
        AbsoluteMonthly,
        RelativeMonthly,
        AbsoluteYearly,
        RelativeYearly
    }

    public enum RecurrenceRangeType
    {
        noEnd, numbered, endDate
    }
}
