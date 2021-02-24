using System;

namespace Triangle.Time
{
    public class TaskTime
    {
        public DateTime StartTime { get; }
        public TimeSpan ExpectedDuration { get; }

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