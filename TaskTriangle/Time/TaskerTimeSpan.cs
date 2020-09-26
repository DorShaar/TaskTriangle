using System;

namespace Triangle.Time
{
    public class TaskerTimeSpan
    {
        private readonly TimeSpan mTimeSpan;

        public int Days => mTimeSpan.Days;
        public int Hours => mTimeSpan.Hours;

        public static TaskerTimeSpan CreateQuick()
        {
            return new TaskerTimeSpan(1);
        }

        public static TaskerTimeSpan CreateZero()
        {
            return new TaskerTimeSpan(0);
        }

        private TaskerTimeSpan(int hours)
        {
            mTimeSpan = new TimeSpan(hours, 0, 0);
        }

        public TaskerTimeSpan(int days, bool halfDay, TimeMode timeMode)
        {
            if (days < 0)
                throw new ArgumentException($"Non of the arguments {nameof(days)}, {nameof(halfDay)} cannot be negative");

            if (days == 0 && !halfDay)
            {
                throw new ArgumentException($"One of the arguments {nameof(days)}, {nameof(halfDay)} must be positive. " +
                    $"If you would like to create zero time span, use {nameof(CreateZero)}");
            }

            int hours = 0;
            if (!halfDay)
            {
                mTimeSpan = new TimeSpan(days, hours, 0, 0);
                return;
            }

            hours = TimeConsts.HalfDay;
            if (timeMode == TimeMode.Work)
                hours = TimeConsts.HalfWorkDay;

            mTimeSpan = new TimeSpan(days, hours, 0, 0);
        }

        public TaskerTimeSpan(int days, bool halfDay) : this(days, halfDay, TimeMode.Regular)
        {
        }

        public int CalculateTotalHours(TimeMode timeMode)
        {
            if (timeMode == TimeMode.Regular)
                return 24 * Days + Hours;
            else
                return TimeConsts.HoursPerWorkDay * Days + Hours;
        }
    }
}