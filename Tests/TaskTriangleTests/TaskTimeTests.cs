using System;
using Triangle.Time;
using Xunit;

namespace TaskTriangleTests
{
    public class TaskTimeTests
    {
        [Theory]
        [InlineData("25/01/2020", 7, 0, 2020, 2, 1)]
        [InlineData("25/01/2020", 7, 25, 2020, 2, 2)]
        public void GetExpectedDueDate_AsExpected(string dateString, int days, int hours,
            int expectedYear, int expectedMonth, int expectedDay)
        {
            TimeSpan duration = TimeSpan.FromDays(days).Add(TimeSpan.FromHours(hours));
            TaskTime taskTime = new TaskTime(DateTime.Parse(dateString), duration);

            DateTime dueDate = taskTime.GetExpectedDueDate();

            Assert.Equal(expectedYear, dueDate.Year);
            Assert.Equal(expectedMonth, dueDate.Month);
            Assert.Equal(expectedDay, dueDate.Day);
        }
    }
}