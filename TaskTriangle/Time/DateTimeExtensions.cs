using System;
using System.Globalization;

namespace Triangle.Time
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this string dateString, string format = null)
        {
            if (format == null)
                format = TimeConsts.TimeFormat;

            return DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
        }
    }
}