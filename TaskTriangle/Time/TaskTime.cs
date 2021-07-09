using System;

namespace Triangle.Time
{
    public class TaskTime
    {
        public DateTime StartTime { get; set; }
        public TimeSpan ExpectedDuration { get; set; }

        public TaskTime(DateTime startTime, TimeSpan expectedDuration)
        {
            StartTime = startTime;
            ExpectedDuration = expectedDuration;
        }

        public DateTime GetExpectedDueDate()
        {
            return StartTime.Add(ExpectedDuration);
        }
    }
}