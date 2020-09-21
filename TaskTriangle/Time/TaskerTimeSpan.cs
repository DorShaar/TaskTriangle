﻿using System;

namespace Triangle.Time
{
    public class TaskerTimeSpan
    {
        public TimeSpan TimeSpan { get; }

        public static TaskerTimeSpan CreateQuick()
        {
            return new TaskerTimeSpan();
        }

        private TaskerTimeSpan()
        {
            TimeSpan = new TimeSpan(1, 0, 0);
        }

        public TaskerTimeSpan(int days, bool halfDay, TimeMode timeMode)
        {
            if (days < 0)
                throw new ArgumentException($"Non of the arguments {nameof(days)}, {nameof(halfDay)} cannot be negative");

            if (days == 0 && !halfDay)
                throw new ArgumentException($"One of the arguments {nameof(days)}, {nameof(halfDay)} must be positive");

            int hours = 0;
            if (!halfDay)
            {
                TimeSpan = new TimeSpan(days, hours, 0, 0);
                return;
            }

            hours = 12;
            if (timeMode == TimeMode.Work)
                hours = 3;

            TimeSpan = new TimeSpan(days, hours, 0, 0);
        }

        public TaskerTimeSpan(int days, bool halfDay) : this(days, halfDay, TimeMode.Regular)
        {
        }
    }
}