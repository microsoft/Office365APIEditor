# Sample Request Built-in Functions

## DateTime

|Function|C# Code|
|:-------|-------|
|${DateTime1HourLater}|DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss")|
|${DateTime2HourLater}|DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-ddTHH:mm:ss")|
|${DateTime1DayLater12}|DateTime.UtcNow.AddDays(1)ToString("yyyy-MM-dd") + "T12:00:00"|
|${DateTime1DayLater14}|DateTime.UtcNow.AddDays(1)ToString("yyyy-MM-dd") + "T14:00:00"|
|${DateTime1DayAnd1HourLater}|DateTime.UtcNow.AddDays(1).AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss")|
|${DateTime1DayAnd2HourLater}|DateTime.UtcNow.AddDays(1).AddHours(2).ToString("yyyy-MM-ddTHH:mm:ss")|
|${DateTime1MonthLater12}|DateTime.UtcNow.AddMonths(1)ToString("yyyy-MM-dd") + "T12:00:00"|

## Date

|Function|C# Code|
|:-------|-------|
|${DateToday}|DateTime.UtcNow.ToString("yyyy-MM-dd")|
|${Date3MonthLater}|DateTime.UtcNow.AddMonths(3).ToString("yyyy-MM-dd")|