using System;
using Triangle.Time;
using Xunit;

namespace Triangle.Tests
{
    public class TaskerDateTimeTests
    {
        [Theory]
        [InlineData("01/02/1991", DayPeriod.Morning, 2)]
        [InlineData("03/05/1992", DayPeriod.Noon, 5)]
        public void Ctor_ValidArguments_AsExpected(string dateString, DayPeriod dayPeriod, int expectedMonth)
        {
            TaskerDateTime taskerDateTime = new TaskerDateTime(dateString.ToDateTime(), dayPeriod);
            Assert.Equal(expectedMonth, taskerDateTime.DateTime.Month);
            Assert.Equal(dayPeriod, taskerDateTime.DayPeriod);
            Assert.Equal(0, taskerDateTime.DateTime.Hour);
            Assert.Equal(0, taskerDateTime.DateTime.Minute);
        }
    }
}