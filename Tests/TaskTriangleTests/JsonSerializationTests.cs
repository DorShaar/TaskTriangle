using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Triangle;
using Triangle.JsonSerialization;
using Triangle.Time;
using Xunit;

namespace TaskTriangleTests
{
    public class JsonSerializationTests
    {
        private const string TestFilesDirectory = "TestFiles";
        private readonly JsonSerializerSettings mSerializerSettings = new JsonSerializerSettings();

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
                               .AddPercentageProgressToNotify(30)
                               .AddPercentageProgressToNotify(95)
                               .SetTime("29/09/2020", DayPeriod.Noon, 3, true);

            TaskTriangle taskTriangle = taskTriangleBuilder.Build();

            string jsonText = JsonConvert.SerializeObject(
                taskTriangle, Formatting.Indented, mSerializerSettings);
            string expectedTextPath = Path.Combine(TestFilesDirectory, "serialized_triangle.txt");
            string expectedText = await File.ReadAllTextAsync(expectedTextPath)
                .ConfigureAwait(false);

            Assert.Equal(expectedText, jsonText);
        }

        [Fact]
        public async Task Deserialize_AsExpected()
        {
            string serializedTrianglePath = Path.Combine(TestFilesDirectory, "serialized_triangle.txt");
            string serializedTriangleJson = await File.ReadAllTextAsync(serializedTrianglePath)
                .ConfigureAwait(false);

            TaskTriangle taskTriangle = JsonConvert.DeserializeObject<TaskTriangle>(
                serializedTriangleJson, mSerializerSettings);

            Assert.True(taskTriangle.Content.GetContents().TryGetValue("content 1", out bool _));
            Assert.True(taskTriangle.Content.GetContents().TryGetValue("content 2", out bool _));
            Assert.False(taskTriangle.Content.GetContents().TryGetValue("content 3", out bool _));

            Assert.Equal("resource 1", taskTriangle.Resources.GetResources().First());

            Assert.True(taskTriangle.Configuration.PercentagesProgressToNotify.HasLowerPercentage(30));
            taskTriangle.Configuration.PercentagesProgressToNotify.Reset(30);

            Assert.True(taskTriangle.Configuration.PercentagesProgressToNotify.HasLowerPercentage(95));
            Assert.False(taskTriangle.Configuration.PercentagesProgressToNotify.HasLowerPercentage(89));

            Assert.Equal(2020, taskTriangle.Time.StartTime.DateTime.Year);
            Assert.Equal(9, taskTriangle.Time.StartTime.DateTime.Month);
            Assert.Equal(29, taskTriangle.Time.StartTime.DateTime.Day);
            Assert.Equal(DayPeriod.Noon, taskTriangle.Time.StartTime.DayPeriod);
        }
    }
}