using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Triangle;
using Triangle.JsonSerialization;
using Xunit;

namespace TaskTriangleTests
{
    public class JsonSerializationTests
    {
        private const string TestFilesDirectory = "TestFiles";

        private readonly JsonSerializerSettings mSerializerSettings = new JsonSerializerSettings();
        private readonly string mSerializeTriangleFile = Path.Combine(TestFilesDirectory, "serialized_triangle.txt");

        public JsonSerializationTests()
        {
            mSerializerSettings.AddTaskTriangleDateTimeConveter();
        }

        [Fact]
        public async Task Serialize_AsExpected()
        {
            TaskTriangleBuilder taskTriangleBuilder = new TaskTriangleBuilder();
            taskTriangleBuilder.AddContent("content 1")
                               .AddContent("content 2")
                               .AddResource("resource 1")
                               .SetTime(DateTime.Parse("29/09/2020"), TimeSpan.FromDays(0.5));

            TaskTriangle taskTriangle = taskTriangleBuilder.Build();

            string jsonText = JsonConvert.SerializeObject(
                taskTriangle, Formatting.Indented, mSerializerSettings);

            string expectedText = await File.ReadAllTextAsync(mSerializeTriangleFile)
                .ConfigureAwait(false);

            Assert.Equal(expectedText, jsonText);
        }

        [Fact]
        public async Task Deserialize_AsExpected()
        {
            string serializedTriangleJson = await File.ReadAllTextAsync(mSerializeTriangleFile)
                .ConfigureAwait(false);

            TaskTriangle taskTriangle = JsonConvert.DeserializeObject<TaskTriangle>(
                serializedTriangleJson, mSerializerSettings);

            Assert.True(taskTriangle.Content.GetContents().TryGetValue("content 1", out bool _));
            Assert.True(taskTriangle.Content.GetContents().TryGetValue("content 2", out bool _));
            Assert.False(taskTriangle.Content.GetContents().TryGetValue("content 3", out bool _));

            Assert.Equal("resource 1", taskTriangle.Resources.GetResources().First());

            Assert.Equal(2020, taskTriangle.Time.StartTime.Year);
            Assert.Equal(9, taskTriangle.Time.StartTime.Month);
            Assert.Equal(29, taskTriangle.Time.StartTime.Day);
        }
    }
}