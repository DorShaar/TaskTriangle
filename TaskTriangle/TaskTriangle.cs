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
        private readonly StringBuilder mStringBuilder = new StringBuilder();
        private Stack<int> mAlertDuringPercentages { get; } = new Stack<int>(4);

        public TimeSpan AlertBefore { get; set; } = TimeSpan.FromMinutes(30);

        public TaskTime Time { get; }
        public TaskContent Content { get; }
        public TaskResources Resources { get; }

        [JsonConstructor]
        internal TaskTriangle(TaskTime time, TaskContent content, TaskResources resources)
        {
            Time = time;
            Resources = resources;
            Content = content;

            InitializeAlertDuringPercentages();
        }

        private void InitializeAlertDuringPercentages()
        {
            mAlertDuringPercentages.Push(90);
            mAlertDuringPercentages.Push(80);
            mAlertDuringPercentages.Push(70);
            mAlertDuringPercentages.Push(50);
        }

        public string GetStatus()
        {
            DateTime expectedDueDate = Time.GetExpectedDueDate();

            mStringBuilder
                .Append("Report time: ").Append(DateTime.Now)
                .AppendLine(Environment.NewLine)
                .Append("Start Time: ").Append(Time.StartTime.ToString(TimeConsts.TimeFormat)).AppendLine(".")
                .Append("Expected duration given: ").Append(Time.ExpectedDuration.Days).Append(" days and ")
                .Append(Time.ExpectedDuration.Hours).Append(" hours. (Total hours: ")
                .Append(Time.ExpectedDuration.TotalHours).AppendLine(").")
                .Append("Due date: ").Append(expectedDueDate.ToString(TimeConsts.TimeFormat)).Append(".")
                .AppendLine(Environment.NewLine)
                .AppendLine("Contents:");

            foreach (var content in Content.GetContents())
            {
                mStringBuilder.Append(content.Key).Append(" - ").AppendLine(content.Value ? "Done." : "Open.");
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
            int currentTimeProgressPercentage = GetCurrentTimeProgressPercentage();

            int alertPercentage = mAlertDuringPercentages.Peek();

            bool shouldNotify = false;

            while (mAlertDuringPercentages.Count > 0 && alertPercentage < currentTimeProgressPercentage)
            {
                shouldNotify = true;

                mAlertDuringPercentages.Pop();

                if (mAlertDuringPercentages.Count > 0)
                    alertPercentage = mAlertDuringPercentages.Peek();
            }

            return shouldNotify;
        }

        public int GetCurrentTimeProgressPercentage()
        {
            double totalHours = Time.ExpectedDuration.TotalHours;

            TimeSpan timeLeft = Time.GetExpectedDueDate() - DateTime.Now;

            double fraction = timeLeft.TotalHours / totalHours;

            int currentTimeProgressPercentage;

            if (fraction > 1)
                currentTimeProgressPercentage = 0;
            else
                currentTimeProgressPercentage = 100 - (int)(fraction * 100);

            return currentTimeProgressPercentage;
        }
    }
}