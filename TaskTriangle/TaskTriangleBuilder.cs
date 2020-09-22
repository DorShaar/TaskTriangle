using System;
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

        private readonly TaskTriangleBuilder mTaskTriangleBuilder = new TaskTriangleBuilder();

        public TaskTriangleBuilder SetTime(string startDate, DayPeriod dayPeriod, int workDays, bool halfWorkDay)
        {
            return SetTime(startDate, dayPeriod, workDays, halfWorkDay, TimeMode.Regular);
        }

        private TaskTriangleBuilder SetTime(
            string startDate, DayPeriod dayPeriod, int workDays, bool halfWorkDay, TimeMode timeMode)
        {
            TaskerDateTime dateTime = new TaskerDateTime(DateTime.Parse(startDate), dayPeriod);
            TaskerTimeSpan timeSpan = new TaskerTimeSpan(workDays, halfWorkDay, timeMode);

            TaskTime taskTime = new TaskTime(dateTime, timeSpan);
            mTaskTriangleBuilder.mTime = taskTime;

            return mTaskTriangleBuilder;
        }

        private TaskTime CreateDefaultTime()
        {
            TaskerDateTime dateTime = new TaskerDateTime(DateTime.Now, DayPeriod.Morning);
            TaskerTimeSpan timeSpan = new TaskerTimeSpan(1, false, TimeMode.Regular);

            return new TaskTime(dateTime, timeSpan);
        }

        public TaskTriangleBuilder AddContent(string content)
        {
            mTaskTriangleBuilder.mContent.AddContent(content);
            return mTaskTriangleBuilder;
        }

        public TaskTriangleBuilder AddResource(string resource)
        {
            mTaskTriangleBuilder.mResources.AddResource(resource);
            return mTaskTriangleBuilder;
        }

        public TaskTriangle Build()
        {
            if (mTaskTriangleBuilder.mTime == null)
                mTaskTriangleBuilder.mTime = CreateDefaultTime();

            return new TaskTriangle(
                mTaskTriangleBuilder.mTime,
                mTaskTriangleBuilder.mResources,
                mTaskTriangleBuilder.mContent);
        }
    }
}