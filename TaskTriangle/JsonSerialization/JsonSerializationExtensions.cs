using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Triangle.Time;

namespace Triangle.JsonSerialization
{
    public static class JsonSerializationExtensions
    {
        public static void AddTaskTriangleDateTimeConveter(this JsonSerializerSettings settings)
        {
            settings.Converters.Add(
                new IsoDateTimeConverter { DateTimeFormat = TimeConsts.TimeFormat });
        }
    }
}