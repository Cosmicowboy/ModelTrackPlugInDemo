using System;

namespace ModelTrackPlugIn.Helpers.Extensions
{
    public static class TimeFormatting
    {
        public static DateTime FormatTime(this string unixTime)
        {
            bool success = Int32.TryParse(unixTime, out int dateOffset);

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

            dateTime = dateTime.AddSeconds(dateOffset);

            return dateTime;

        }
    }
}
