using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Office365APIEditor.ViewerHelper.Data.CalendarAPI
{
    public class PatternedRecurrence
    {
        public RecurrencePattern Pattern;
        public RecurrenceRange Range;
    }

    public class RecurrencePattern
    {
        public int DayOfMonth;
        public DayOfWeek[] DaysOfWeek;
        public DayOfWeek FirstDayOfWeek;
        public WeekIndex Index;
        public int Interval;
        public int Month;
        public RecurrencePatternType Type;
    }

    public class RecurrenceRange
    {
        public DateTime EndDate;
        public int NumberOfOccurrences;
        public string RecurrenceTimeZone;
        public DateTime StartDate;
        public RecurrenceRangeType Type;
    }

    public enum WeekIndex
    {
        First,
        Second,
        Third,
        Fourth,
        Last
    }

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
