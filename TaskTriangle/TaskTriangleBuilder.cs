using System;
using Triangle.Configuration;
using Triangle.Content;
using Triangle.Resources;
using Triangle.Time;

namespace Triangle
{
    public class TaskTriangleBuilder
    {
        private TaskTime mTime;
        private readonly TaskContent mContent = new TaskContent();
        private readonly TaskResources mResources = new TaskResources();
        private readonly TriangleConfiguration mConfiguration = new TriangleConfiguration();

        public TaskTriangleBuilder SetTime(string startDate, DayPeriod dayPeriod, int workDays, bool halfWorkDay)
        {
            return SetTime(startDate, dayPeriod, workDays, halfWorkDay, TimeMode.Regular);
        }

        private TaskTriangleBuilder SetTime(
            string startDate, DayPeriod dayPeriod, int workDays, bool halfWorkDay, TimeMode timeMode)
        {
            TaskerDateTime dateTime = new TaskerDateTime(startDate.ToDateTime(), dayPeriod);
            TaskerTimeSpan timeSpan = new TaskerTimeSpan(workDays, halfWorkDay, timeMode);

            TaskTime taskTime = new TaskTime(dateTime, timeSpan, timeMode);
            mTime = taskTime;

            return this;
        }

        private TaskTime CreateDefaultTime()
        {
            TimeMode regularTimeMode = TimeMode.Regular;

            TaskerDateTime dateTime = new TaskerDateTime(DateTime.Now, DayPeriod.Morning);
            TaskerTimeSpan timeSpan = new TaskerTimeSpan(1, false, regularTimeMode);

            return new TaskTime(dateTime, timeSpan, regularTimeMode);
        }

        public TaskTriangleBuilder AddContent(string content)
        {
            mContent.AddContent(content);
            return this;
        }

        public TaskTriangleBuilder AddResource(string resource)
        {
            mResources.AddResource(resource);
            return this;
        }

        public TaskTriangleBuilder AddPercentageProgressToNotify(int percentage)
        {
            mConfiguration.PercentagesProgressToNotify.Set(percentage);
            return this;
        }

        public TaskTriangle Build()
        {
            if (mTime == null)
                mTime = CreateDefaultTime();

            return new TaskTriangle(mTime, mContent, mResources, mConfiguration);
        }
    }
}