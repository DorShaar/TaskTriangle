using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Triangle.Content;
using Triangle.Resources;
using Triangle.Time;

namespace Triangle
{
    public class TaskTriangle
    {
        /// <summary>
        /// Default is 30 minutes.
        /// </summary>
        public List<TimeSpan> AlertsBefore { get; set; } = new List<TimeSpan> { TimeSpan.FromMinutes(30) };

        public TaskTime Time { get; }
        public TaskContent Content { get; }
        public TaskResources Resources { get; }

        [JsonConstructor]
        internal TaskTriangle(TaskTime time, TaskContent content, TaskResources resources)
        {
            Time = time;
            Resources = resources;
            Content = content;
        }

        public string GetStringStatus()
        {
            DateTime expectedDueDate = Time.GetExpectedDueDate();

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder
                .Append("Report time: ").Append(DateTime.Now)
                .AppendLine(Environment.NewLine)
                .Append("Start Time: ").Append(Time.StartTime.ToString(TimeConsts.TimeFormat)).AppendLine(".")
                .Append("Expected duration given: ").Append(Time.ExpectedDuration.Days).Append(" days and ")
                .Append(Time.ExpectedDuration.Hours).Append(" hours. (Total hours: ")
                .Append(Time.ExpectedDuration.TotalHours).AppendLine(").")
                .Append("Due date: ").Append(expectedDueDate.ToString(TimeConsts.TimeFormat)).Append(".")
                .AppendLine(Environment.NewLine);

            AppendContentsToStringBuilder(stringBuilder);
            AppendResourcesToStringBuilder(stringBuilder);

            stringBuilder.AppendLine();

            string status = stringBuilder.ToString();
            stringBuilder.Clear();

            return status;
        }

        private void AppendContentsToStringBuilder(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("Contents:");

            foreach (var content in Content.GetContents())
            {
                stringBuilder.Append(content.Key).Append(" - ").AppendLine(content.Value ? "Done." : "Open.");
            }
        }

        private void AppendResourcesToStringBuilder(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine()
               .AppendLine("Resources:");

            foreach (string resource in Resources.GetResources())
            {
                stringBuilder.Append(resource).Append(", ");
            }
        }
    }
}