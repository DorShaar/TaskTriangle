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

        public TaskTriangleBuilder SetTime(DateTime dateTime, TimeSpan duration)
        {
            mTime = new TaskTime(dateTime, duration);
            return this;
        }

        private TaskTime CreateDefaultTime()
        {
            return new TaskTime(DateTime.Now, TimeSpan.FromHours(TimeConsts.WorkHoursPerDay));
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

            return new TaskTriangle(mTime, mContent, mResources);
        }
    }
}