using System;
using Triangle.Time;
using Xunit;

namespace Triangle.Tests
{
    public class TaskerDateTimeTests
    {
        [Theory]
        [InlineData("01/02/1991", DayPeriod.Morning, 2, 9)]
        [InlineData("03/05/1992", DayPeriod.Noon, 5, 14)]
        public void Ctor_ValidArguments_AsExpected(
            string dateString, DayPeriod dayPeriod, int expectedMonth, int expectedHour)
        {
            TaskerDateTime taskerDateTime = new TaskerDateTime(DateTime.Parse(dateString), dayPeriod);
            Assert.Equal(expectedMonth, taskerDateTime.DateTime.Month);
            Assert.Equal(expectedHour, taskerDateTime.DateTime.Hour);
            Assert.Equal(0, taskerDateTime.DateTime.Minute);
        }

        [Fact]
        public void Ctor_InvalidArguments_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TaskerDateTime(DateTime.Now, (DayPeriod)50));
        }
    }
}