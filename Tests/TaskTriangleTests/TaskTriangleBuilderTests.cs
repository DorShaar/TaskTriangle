using System;
using System.Collections.Generic;
using System.Linq;
using Triangle;
using Xunit;

namespace TaskTriangleTests
{
    public class TaskTriangleBuilderTests
    {
        [Fact]
        public void Build_AsExpected()
        {
            TaskTriangleBuilder builder = new TaskTriangleBuilder();

            const string content1 = "Clean teeth with dental floss";
            const string content2 = "Sleep at 10 PM";
            const string resource = "Me";

            TaskTriangle taskTriangle = builder
                .AddContent(content1)
                .AddResource(resource)
                .SetTime(DateTime.Parse("23/09/2020"), TimeSpan.FromHours(12))
                .AddContent(content2)
                .Build();

            IReadOnlyDictionary<string, bool> contents = taskTriangle.Content.GetContents();
            Assert.Equal(2, contents.Count);
            Assert.False(contents[content1]);
            Assert.False(contents[content2]);

            Assert.Equal(resource, taskTriangle.Resources.GetResources().First());

            Assert.Equal(23, taskTriangle.Time.StartTime.Day);
            Assert.Equal(09, taskTriangle.Time.StartTime.Month);
            Assert.Equal(2020, taskTriangle.Time.StartTime.Year);
        }
    }
}