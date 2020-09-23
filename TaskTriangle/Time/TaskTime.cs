using System;

namespace Triangle.Time
{
    public class TaskTime
    {
        public TaskerDateTime StartTime { get; }
        public TaskerTimeSpan ExpectedDuration { get; }

        public TaskTime(TaskerDateTime startTime, TaskerTimeSpan expectedDuration)
        {
            StartTime = startTime;
            ExpectedDuration = expectedDuration;
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

            bool halfDay = IsHalfDayLeft(remainingTime.Hours);

            return new TaskerTimeSpan(remainingTime.Days, halfDay, ExpectedDuration.TimeMode);
        }

        private bool IsHalfDayLeft(int hours)
        {
            if (ExpectedDuration.TimeMode == TimeMode.Regular)
                return hours >= (double)(TimeConsts.HalfDay / 2);

            return hours >= (double)(TimeConsts.HalfWorkDay / 2);
        }
    }
}