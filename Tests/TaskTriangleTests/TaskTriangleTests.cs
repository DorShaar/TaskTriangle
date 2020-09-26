using System;
using Triangle;
using Triangle.Time;
using Xunit;

namespace TaskTriangleTests
{
    public class TaskTriangleTests
    {
        [Theory]
        [InlineData(60, true)]
        [InlineData(null, false)]
        public void ShouldNotify_AsExpected(int? percentage, bool shouldNotify)
        {
            string yesterdayDate = DateTime.Now.Date.AddDays(-1).ToString(TimeConsts.TimeFormat);

            TaskTriangle taskTriangle = CreateTaskTriangle(yesterdayDate);

            if (percentage.HasValue)
                taskTriangle.Configuration.PercentagesProgressToNotify.Add(percentage.Value);

            Assert.Equal(shouldNotify, taskTriangle.ShouldNotify());
        }

        [Fact]
        public void GetStatus_AsExpected()
        {
            TaskTriangle taskTriangle = CreateTaskTriangle("25/09/2020");

            string expectedStatus = 
@"Start Time: 25/09/2020 Morning.
Expected duration given: 1 days and 0 hours. (Total hours: 24).
Due date: 26/09/2020 Morning.

Contents:
Clean teeth with dental floss - Done.
Sleep at 10 PM - Open.

Resources:
Me, 
";

            Assert.True(taskTriangle.Content.MarkContentDone("Clean teeth with dental floss"));

            string actualStatus = taskTriangle.GetStatus();
            string statusWithoutReportTime = actualStatus.Remove(0, 38);

            Assert.Equal(expectedStatus, statusWithoutReportTime);
        }

        private TaskTriangle CreateTaskTriangle(string startDate)
        {
            TaskTriangleBuilder builder = new TaskTriangleBuilder();

            TaskTriangle taskTriangle = builder
                .AddContent("Clean teeth with dental floss")
                .AddResource("Me")
                .SetTime(startDate, DayPeriod.Morning, workDays: 1, halfWorkDay: false)
                .AddContent("Sleep at 10 PM")
                .Build();

            return taskTriangle;
        }
    }
}