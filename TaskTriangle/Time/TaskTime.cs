using System;

namespace Triangle.Time
{
    public class TaskTime
    {
        public TaskerDateTime StartTime { get; }
        public TaskerTimeSpan Duration { get; }

        public TaskTime(TaskerDateTime startTime, TaskerTimeSpan duration)
        {
            StartTime = startTime;
            Duration = duration;
        }

        public DateTime GetExpectedDueDate()
        {
            DateTime dateTime = StartTime.DateTime.AddDays(Duration.TimeSpan.Days);
            return dateTime.AddHours(Duration.TimeSpan.Hours);
        }
    }
}