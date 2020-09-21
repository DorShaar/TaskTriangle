using System;
using Triangle.Time;
using Xunit;

namespace Triangle.Tests
{
    public class TaskerTimeSpanTests
    {
        [Theory]
        [InlineData(3, true, TimeMode.Work, 3)]
        [InlineData(2, false, TimeMode.Work, 0)]
        [InlineData(0, true, TimeMode.Work, 3)]
        [InlineData(5, true, TimeMode.Regular, 12)]
        [InlineData(4, false, TimeMode.Regular, 0)]
        public void Ctor_ValidArguments_AsExpected(int days, bool halfDay, TimeMode timeMode, int expectedHours)
        {
            TaskerTimeSpan taskerTimeSpan = new TaskerTimeSpan(days, halfDay, timeMode);

            Assert.Equal(days, taskerTimeSpan.TimeSpan.Days);
            Assert.Equal(expectedHours, taskerTimeSpan.TimeSpan.Hours);
            Assert.Equal(0, taskerTimeSpan.TimeSpan.Minutes);
        }

        [Fact]
        public void Ctor_ValidArgumentsWithoudTimeMode_AsExpected()
        {
            TaskerTimeSpan taskerTimeSpan = new TaskerTimeSpan(3, true);
            Assert.Equal(3, taskerTimeSpan.TimeSpan.Days);
            Assert.Equal(12, taskerTimeSpan.TimeSpan.Hours);
            Assert.Equal(0, taskerTimeSpan.TimeSpan.Minutes);
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
            Assert.Equal(0, taskerTimeSpan.TimeSpan.Days);
            Assert.Equal(1, taskerTimeSpan.TimeSpan.Hours);
            Assert.Equal(0, taskerTimeSpan.TimeSpan.Minutes);
        }
    }
}