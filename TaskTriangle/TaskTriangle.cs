using System.Linq;
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

            Configuration.PercentagesProgressToNotify.Add(80);
        }

        //public bool GetStatus()
        //{
        //    // TODO
        //}

        public bool ShouldNotify()
        {
            int totalHours = Time.ExpectedDuration.TotalHours;
            TaskerTimeSpan remainingTime = Time.GetRemainingTime();
            int currentProgressPercentage = (int)((double)(remainingTime.TotalHours / totalHours) * 100);

            return Configuration.PercentagesProgressToNotify.Any(percentage => 
                percentage <= currentProgressPercentage);
        }
    }
}