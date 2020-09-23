using System;

namespace Triangle.Time
{
    public class TaskerTimeSpan
    {
        private readonly TimeSpan mTimeSpan;

        public TimeMode TimeMode { get; }
        public int Days => mTimeSpan.Days;
        public int Hours => mTimeSpan.Hours;
        public int TotalHours { get => CalculateTotalHours(); }

        public static TaskerTimeSpan CreateQuick()
        {
            return new TaskerTimeSpan();
        }

        private TaskerTimeSpan()
        {
            mTimeSpan = new TimeSpan(1, 0, 0);
        }

        public TaskerTimeSpan(int days, bool halfDay, TimeMode timeMode)
        {
            if (days < 0)
                throw new ArgumentException($"Non of the arguments {nameof(days)}, {nameof(halfDay)} cannot be negative");

            if (days == 0 && !halfDay)
                throw new ArgumentException($"One of the arguments {nameof(days)}, {nameof(halfDay)} must be positive");

            TimeMode = timeMode;

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

        private int CalculateTotalHours()
        {
            if (TimeMode == TimeMode.Regular)
                return 24 * Days + Hours;
            else
                return TimeConsts.HoursPerWorkDay * Days + Hours;
        }
    }
}