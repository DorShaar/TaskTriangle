using Triangle.Content;
using Triangle.Resources;
using Triangle.Time;

namespace Triangle
{
    public class TaskTriangle
    {
        public TaskTime Time { get; }
        public TaskResources Resources { get; }
        public TaskContent Content { get; }

        internal TaskTriangle(TaskTime time, TaskResources resources, TaskContent content)
        {
            Time = time;
            Resources = resources;
            Content = content;
        }

        //public bool ShouldRaiseFlag()
        //{
        //    // TODO
        //}
    }
}