using System.Collections.Generic;
using System.Linq;
using Triangle;
using Triangle.Time;
using Xunit;

namespace TaskTriangleTests
{
    public class TaskTriangleBuilderTests
    {
        [Fact]
        public void Build_AsExpected()
        {
            TaskTriangleBuilder builder = new TaskTriangleBuilder();

            string content1 = "Clean teeth with dental floss";
            string content2 = "Sleep at 10 PM";
            string resource = "Me";

            TaskTriangle taskTriangle = builder
                .AddContent(content1)
                .AddResource(resource)
                .SetTime("23/09/2020", DayPeriod.Morning, 1, false)
                .AddContent(content2)
                .AddPercentageProgressToNotify(30)
                .AddPercentageProgressToNotify(85)
                .Build();

            IReadOnlyDictionary<string, bool> contents = taskTriangle.Content.GetContents();
            Assert.Equal(2, contents.Count);
            Assert.False(contents[content1]);
            Assert.False(contents[content2]);

            Assert.Equal(resource, taskTriangle.Resources.GetResources().First());

            Assert.Equal(23, taskTriangle.Time.StartTime.DateTime.Day);
            Assert.Equal(09, taskTriangle.Time.StartTime.DateTime.Month);
            Assert.Equal(2020, taskTriangle.Time.StartTime.DateTime.Year);


            Assert.True(taskTriangle.Configuration.PercentagesProgressToNotify.HasLowerPercentage(30));
            taskTriangle.Configuration.PercentagesProgressToNotify.Reset(30);

            Assert.True(taskTriangle.Configuration.PercentagesProgressToNotify.HasLowerPercentage(85));
        }
    }
}