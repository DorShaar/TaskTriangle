using System;
using Triangle.Time;
using Xunit;

namespace Triangle.Tests
{
    public class TaskerTimeSpanTests
    {
        [Theory]
        [InlineData(3, true, TimeMode.Work, 3, 21)]
        [InlineData(2, false, TimeMode.Work, 0, 12)]
        [InlineData(0, true, TimeMode.Work, 3, 3)]
        [InlineData(5, true, TimeMode.Regular, 12, 132)]
        [InlineData(4, false, TimeMode.Regular, 0, 96)]
        public void Ctor_ValidArguments_AsExpected(int days, bool halfDay, TimeMode timeMode,
            int expectedHours, int expectedTotalHours)
        {
            TaskerTimeSpan taskerTimeSpan = new TaskerTimeSpan(days, halfDay, timeMode);

            Assert.Equal(days, taskerTimeSpan.Days);
            Assert.Equal(expectedHours, taskerTimeSpan.Hours);
            Assert.Equal(expectedTotalHours, taskerTimeSpan.TotalHours);
        }

        [Fact]
        public void Ctor_ValidArgumentsWithoutTimeMode_AsExpected()
        {
            TaskerTimeSpan taskerTimeSpan = new TaskerTimeSpan(3, true);
            Assert.Equal(3, taskerTimeSpan.Days);
            Assert.Equal(12, taskerTimeSpan.Hours);
            Assert.Equal(84, taskerTimeSpan.TotalHours);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void Ctor_InvalidArguments_ThrowsArgumentException(int days, bool halfDay)
        {
            Assert.Throws<ArgumentException>(() => new TaskerTimeSpan(days, halfDay));
        }

        [Fact]
        public void CreateQuick_TimeSpanIsOneHour()
        {
            TaskerTimeSpan taskerTimeSpan = TaskerTimeSpan.CreateQuick();
            Assert.Equal(0, taskerTimeSpan.Days);
            Assert.Equal(1, taskerTimeSpan.Hours);
            Assert.Equal(1, taskerTimeSpan.TotalHours);
        }
    }
}