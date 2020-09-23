using System;
using Triangle.Content;
using Triangle.Resources;
using Triangle.Time;

namespace Triangle
{
    public class TaskTriangle
    {
        public TriangleConfiguration Configuration { get; } = new TriangleConfiguration();

        public TaskTime Time { get; }
        public TaskResources Resources { get; }
        public TaskContent Content { get; }

        internal TaskTriangle(TaskTime time, TaskResources resources, TaskContent content)
        {
            Time = time;
            Resources = resources;
            Content = content;

            Configuration.PercentageProgressToNotify.Add(80);
        }

        //public bool GetStatus()
        //{
        //    // TODO
        //}

        //public bool ShouldNotify()
        //{
        //    DateTime dueDate = Time.GetExpectedDueDate();

        //}
    }
}