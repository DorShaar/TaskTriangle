﻿using System;
using Triangle;
using Triangle.Time;
using Xunit;

namespace TaskTriangleTests
{
    public class TaskTriangleTests
    {
        [Theory]
        [InlineData(-1, true)]
        [InlineData(1, false)]
        public void ShouldNotify_AsExpected(int daysFromNow, bool shouldNotify)
        {
            string dayesFromNow = DateTime.Now.Date.AddDays(daysFromNow).ToString(TimeConsts.TimeFormat);

            TaskTriangle taskTriangle = CreateTaskTriangle(dayesFromNow);

            Assert.Equal(shouldNotify, taskTriangle.ShouldNotify());
        }

        private TaskTriangle CreateTaskTriangle(string startDate)
        {
            TaskTriangleBuilder builder = new TaskTriangleBuilder();

            return builder
                .AddContent("Clean teeth with dental floss")
                .AddResource("Me")
                .SetTime(DateTime.Parse(startDate), TimeSpan.FromHours(5))
                .AddContent("Sleep at 10 PM")
                .Build();
        }

        [Fact]
        public void GetStatus_AsExpected()
        {
            TaskTriangle taskTriangle = CreateTaskTriangle("25/09/2020");

            const string expectedStatus =
@"Start Time: 25/09/2020.
Expected duration given: 1 days and 0 hours. (Total hours: 24).
Due date: 26/09/2020.

Contents:
Clean teeth with dental floss - Done.
Sleep at 10 PM - Open.

Resources:
Me, 
";

            Assert.True(taskTriangle.Content.MarkContentDone("Clean teeth with dental floss"));

            string actualStatus = taskTriangle.GetStatus();
            string statusWithoutReportTime = actualStatus.Remove(0, 36);

            Assert.Equal(expectedStatus, statusWithoutReportTime);
        }
    }
}