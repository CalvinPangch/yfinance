using YFinance.Interfaces.Utils;

namespace YFinance.Implementation.Utils;

/// <summary>
/// Basic timezone helper for DST fixes and conversions.
/// </summary>
public class TimezoneHelper : ITimezoneHelper
{
    public DateTime ConvertToExchangeTime(DateTime utcDateTime, string timeZoneId)
    {
        var tz = GetTimeZone(timeZoneId);
        if (utcDateTime.Kind != DateTimeKind.Utc)
            utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

        return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, tz);
    }

    public DateTime[] FixDstIssues(DateTime[] timestamps, string timeZoneId)
    {
        if (timestamps.Length == 0)
            return timestamps;

        var tz = GetTimeZone(timeZoneId);
        var fixedTimestamps = new DateTime[timestamps.Length];

        for (int i = 0; i < timestamps.Length; i++)
        {
            var utc = timestamps[i].Kind == DateTimeKind.Utc
                ? timestamps[i]
                : DateTime.SpecifyKind(timestamps[i], DateTimeKind.Utc);

            var local = TimeZoneInfo.ConvertTimeFromUtc(utc, tz);

            if (local.Hour != 0)
            {
                var normalizedLocal = local.Date;
                fixedTimestamps[i] = TimeZoneInfo.ConvertTimeToUtc(normalizedLocal, tz);
            }
            else
            {
                fixedTimestamps[i] = utc;
            }
        }

        return fixedTimestamps;
    }

    public TimeSpan GetTimezoneOffset(DateTime dateTime, string timeZoneId)
    {
        var tz = GetTimeZone(timeZoneId);
        return tz.GetUtcOffset(dateTime);
    }

    private static TimeZoneInfo GetTimeZone(string timeZoneId)
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        catch (TimeZoneNotFoundException)
        {
            return TimeZoneInfo.Utc;
        }
        catch (InvalidTimeZoneException)
        {
            return TimeZoneInfo.Utc;
        }
    }
}
