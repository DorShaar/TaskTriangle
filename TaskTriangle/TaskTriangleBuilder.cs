using System;
using System.Globalization;
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

        public TaskTriangleBuilder SetTime(string startDate, DayPeriod dayPeriod, int workDays, bool halfWorkDay)
        {
            return SetTime(startDate, dayPeriod, workDays, halfWorkDay, TimeMode.Regular);
        }

        private TaskTriangleBuilder SetTime(
            string startDate, DayPeriod dayPeriod, int workDays, bool halfWorkDay, TimeMode timeMode)
        {
            TaskerDateTime dateTime = new TaskerDateTime(startDate.ToDateTime(), dayPeriod);
            TaskerTimeSpan timeSpan = new TaskerTimeSpan(workDays, halfWorkDay, timeMode);

            TaskTime taskTime = new TaskTime(dateTime, timeSpan);
            mTime = taskTime;

            return this;
        }

        private TaskTime CreateDefaultTime()
        {
            TaskerDateTime dateTime = new TaskerDateTime(DateTime.Now, DayPeriod.Morning);
            TaskerTimeSpan timeSpan = new TaskerTimeSpan(1, false, TimeMode.Regular);

            return new TaskTime(dateTime, timeSpan);
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

        public TaskTriangle Build()
        {
            if (mTime == null)
                mTime = CreateDefaultTime();

            return new TaskTriangle(mTime, mResources, mContent);
        }
    }
}