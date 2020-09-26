using System;

namespace Triangle.Time
{
    public class TaskTime
    {
        public TimeMode TimeMode { get; }
        public TaskerDateTime StartTime { get; }
        public TaskerTimeSpan ExpectedDuration { get; }

        public TaskTime(TaskerDateTime startTime, TaskerTimeSpan expectedDuration, TimeMode timeMode)
        {
            StartTime = startTime;
            ExpectedDuration = expectedDuration;
            TimeMode = timeMode;
        }

        public TaskerDateTime GetExpectedDueDate()
        {
            DateTime dateTime = StartTime.DateTime.AddDays(ExpectedDuration.Days);

            bool isHalfDayLeft = IsHalfDayLeft(ExpectedDuration.Hours);

            if (StartTime.DayPeriod == DayPeriod.Noon && isHalfDayLeft)
                dateTime = dateTime.AddDays(1);
            else if (StartTime.DayPeriod == DayPeriod.Noon || isHalfDayLeft)
                return new TaskerDateTime(dateTime, DayPeriod.Noon);

            return new TaskerDateTime(dateTime, DayPeriod.Morning);
        }

        public TaskerTimeSpan GetRemainingTime()
        {
            DateTime expectedDueDate = GetExpectedDueDate().DateTime;

            TimeSpan remainingTime = expectedDueDate - DateTime.Now;

            if (remainingTime.TotalMilliseconds < 0)
                return TaskerTimeSpan.CreateZero();

            bool halfDay = IsHalfDayLeft(remainingTime.Hours);

            return new TaskerTimeSpan(remainingTime.Days, halfDay, TimeMode);
        }

        private bool IsHalfDayLeft(int hours)
        {
            if (TimeMode == TimeMode.Regular)
                return hours >= (double)(TimeConsts.HalfDay / 2);

            return hours >= (double)(TimeConsts.HalfWorkDay / 2);
        }
    }
}