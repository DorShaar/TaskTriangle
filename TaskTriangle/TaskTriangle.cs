using System;
using System.Linq;
using System.Text;
using Triangle.Content;
using Triangle.Resources;
using Triangle.Time;

namespace Triangle
{
    public class TaskTriangle
    {
        private readonly StringBuilder mStringBuilder = new StringBuilder();
        public TriangleConfiguration Configuration { get; } = new TriangleConfiguration();

        public TaskTime Time { get; }
        public TaskContent Content { get; }
        public TaskResources Resources { get; }

        internal TaskTriangle(TaskTime time, TaskContent content, TaskResources resources,
            TriangleConfiguration configuration)
        {
            Time = time;
            Resources = resources;
            Content = content;
            Configuration = configuration;
        }

        public string GetStatus()
        {
            TaskerDateTime expectedDueDate = Time.GetExpectedDueDate();

            mStringBuilder
                .Append($"Report time: {DateTime.Now}")
                .AppendLine(Environment.NewLine)
                .Append("Start Time: ").Append(Time.StartTime.DateTime.ToString(TimeConsts.TimeFormat)).Append(" ")
                .Append(Time.StartTime.DayPeriod).Append(".")
                .AppendLine()
                .Append("Expected duration given: ").Append(Time.ExpectedDuration.Days).Append(" days and ")
                .Append(Time.ExpectedDuration.Hours).Append(" hours. (Total hours: ")
                .Append(Time.ExpectedDuration.CalculateTotalHours(Time.TimeMode)).Append(").")
                .AppendLine()
                .Append("Due date: ").Append(expectedDueDate.DateTime.ToString(TimeConsts.TimeFormat)).Append(" ")
                .Append(expectedDueDate.DayPeriod).Append(".")
                .AppendLine(Environment.NewLine)
                .AppendLine("Contents:");

            foreach (var content in Content.GetContents())
            {
                mStringBuilder.Append(content.Key).Append(" - ").Append(content.Value ? "Done." : "Open.")
                .AppendLine();
            }

            mStringBuilder.AppendLine()
                .AppendLine("Resources:");

            foreach (string resource in Resources.GetResources())
            {
                mStringBuilder.Append(resource).Append(", ");
            }

            mStringBuilder.AppendLine();

            string status = mStringBuilder.ToString();
            mStringBuilder.Clear();

            return status;
        }

        public bool ShouldNotify()
        {
            int totalHours = Time.ExpectedDuration.CalculateTotalHours(Time.TimeMode);
            TaskerTimeSpan remainingTime = Time.GetRemainingTime();
            float fraction = (float)remainingTime.CalculateTotalHours(Time.TimeMode) / totalHours;
            int currentTimeProgressPercentage = 100 - (int)(fraction * 100);

            return Configuration.PercentagesProgressToNotify.Any(percentage =>
                percentage <= currentTimeProgressPercentage);
        }
    }
}