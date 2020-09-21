using System;

namespace Triangle.Time
{
    /// <summary>
    /// Date and 2 time window possibilities: start of the day (09:00) or middle of the day (14:00).
    /// </summary>
    public class TaskerDateTime
    {
        public DateTime DateTime { get; }

        public TaskerDateTime(DateTime date, DayPeriod dayPeriod)
        {
            int hour = dayPeriod switch
            {
                DayPeriod.Morning => 9,
                DayPeriod.Noon => 14,
                _ => throw new ArgumentException($"Invalid argument {nameof(dayPeriod)}: {dayPeriod}"),
            };

            DateTime = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
        }
    }
}