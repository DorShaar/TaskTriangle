using Newtonsoft.Json;
using System;

namespace Triangle.Time
{
    /// <summary>
    /// Date and 2 time window possibilities: start of the day (09:00) or middle of the day (14:00).
    /// </summary>
    public class TaskerDateTime
    {
        public DateTime DateTime { get; }
        public DayPeriod DayPeriod { get; }

        public TaskerDateTime(DateTime dateTime, DayPeriod dayPeriod)
        {
            DateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            DayPeriod = dayPeriod;
        }
    }
}