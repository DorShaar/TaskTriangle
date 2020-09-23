using System;
using Triangle.Time;
using Xunit;

namespace TaskTriangleTests
{
    public class TaskTimeTests
    {
        [Theory]
        [InlineData("25/01/2020", DayPeriod.Noon, 7, true, TimeMode.Work, 2020, 2, 2, DayPeriod.Morning)]
        [InlineData("25/01/2020", DayPeriod.Noon, 7, false, TimeMode.Work, 2020, 2, 1, DayPeriod.Noon)]
        [InlineData("25/01/2020", DayPeriod.Morning, 7, true, TimeMode.Work, 2020, 2, 1, DayPeriod.Noon)]
        [InlineData("25/01/2020", DayPeriod.Morning, 7, false, TimeMode.Work, 2020, 2, 1, DayPeriod.Morning)]
        public void GetExpectedDueDate_AsExpected(
            string dateString, DayPeriod dayPeriod, int days, bool halfDay, TimeMode timeMode,
            int expectedYear, int expectedMonth, int expectedDay, DayPeriod expectedDayPeriod)
        {
            TaskerDateTime dateTime = new TaskerDateTime(dateString.ToDateTime(), dayPeriod);
            TaskerTimeSpan timeSpan = new TaskerTimeSpan(days, halfDay, timeMode);
            TaskTime taskTime = new TaskTime(dateTime, timeSpan);

            TaskerDateTime dueDate = taskTime.GetExpectedDueDate();

            DateTime expectedTime = new DateTime(expectedYear, expectedMonth, expectedDay);
            Assert.Equal(expectedTime, dueDate.DateTime);
            Assert.Equal(expectedDayPeriod, dueDate.DayPeriod);
        }

        [Fact]
        public void GetRemainingTime_AsExpected()
        {
            TaskerDateTime dateTime = new TaskerDateTime(DateTime.Now, DayPeriod.Noon);
            TaskerTimeSpan timeSpan = new TaskerTimeSpan(7, halfDay: true, TimeMode.Work);
            TaskTime taskTime = new TaskTime(dateTime, timeSpan);

            TaskerTimeSpan remainingTime = taskTime.GetRemainingTime();

            Assert.Equal(TimeMode.Work, remainingTime.TimeMode);
            Assert.Equal(7, remainingTime.Days);
            Assert.Equal(3, remainingTime.Hours);
        }
    }
}