using NodaTime;
using NodaTime.TimeZones;
using YFinance.NET.Interfaces.Utils;

namespace YFinance.NET.Implementation.Utils;

/// <summary>
/// Advanced timezone helper for DST fixes and conversions using NodaTime.
/// Handles DST transitions, ambiguous times, and IANA timezone IDs properly.
/// Based on Python yfinance timezone handling.
/// </summary>
public class TimezoneHelper : ITimezoneHelper
{
    private static readonly IDateTimeZoneProvider TimeZoneProvider = DateTimeZoneProviders.Tzdb;

    /// <summary>
    /// Converts a UTC datetime to the specified timezone.
    /// </summary>
    /// <param name="utcDateTime">The UTC datetime to convert.</param>
    /// <param name="timeZoneId">The IANA timezone ID (e.g., "America/New_York").</param>
    /// <returns>The datetime converted to the specified timezone.</returns>
    public DateTime ConvertToExchangeTime(DateTime utcDateTime, string timeZoneId)
    {
        var dateTimeZone = GetDateTimeZone(timeZoneId);

        // Ensure UTC kind
        if (utcDateTime.Kind != DateTimeKind.Utc)
            utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

        // Convert to NodaTime Instant
        var instant = Instant.FromDateTimeUtc(utcDateTime);

        // Convert to zone time
        var zonedDateTime = instant.InZone(dateTimeZone);

        // Return as DateTime with Unspecified kind (local to exchange)
        return zonedDateTime.ToDateTimeUnspecified();
    }

    /// <summary>
    /// Fixes daylight saving time issues in a timestamp array.
    /// </summary>
    /// <param name="timestamps">Array of timestamps to fix.</param>
    /// <param name="timeZoneId">The IANA timezone ID.</param>
    /// <returns>Timestamps with DST issues corrected.</returns>
    public DateTime[] FixDstIssues(DateTime[] timestamps, string timeZoneId)
    {
        if (timestamps.Length == 0)
            return timestamps;

        var dateTimeZone = GetDateTimeZone(timeZoneId);
        var fixedTimestamps = new DateTime[timestamps.Length];

        for (int i = 0; i < timestamps.Length; i++)
        {
            fixedTimestamps[i] = FixSingleDstIssue(timestamps[i], dateTimeZone);
        }

        return fixedTimestamps;
    }

    /// <summary>
    /// Fixes DST issues for a single timestamp.
    /// Handles ambiguous times (fall back) and skipped times (spring forward).
    /// </summary>
    private static DateTime FixSingleDstIssue(DateTime timestamp, DateTimeZone dateTimeZone)
    {
        // Ensure UTC kind
        var utc = timestamp.Kind == DateTimeKind.Utc
            ? timestamp
            : DateTime.SpecifyKind(timestamp, DateTimeKind.Utc);

        var instant = Instant.FromDateTimeUtc(utc);
        var zonedDateTime = instant.InZone(dateTimeZone);
        var localTime = zonedDateTime.LocalDateTime;

        // Check if the time falls on a DST transition boundary
        // This handles cases where Yahoo Finance reports times inconsistently around DST changes

        // If the hour is not at midnight (for daily data), normalize to midnight
        if (localTime.Hour != 0 && localTime.Minute == 0)
        {
            // Create a LocalDate at midnight
            var normalizedDate = localTime.Date.AtMidnight();

            // Try to map back to this timezone
            var mapping = dateTimeZone.MapLocal(normalizedDate);

            // Handle ambiguous times (when clocks fall back)
            if (mapping.Count == 2)
            {
                // Use the earlier occurrence (before DST ends)
                return mapping.First().ToDateTimeUtc();
            }
            // Handle skipped times (when clocks spring forward)
            else if (mapping.Count == 0)
            {
                // The time doesn't exist, use the time after the gap
                var intervalAfter = dateTimeZone.GetZoneInterval(instant);
                return intervalAfter.Start.ToDateTimeUtc();
            }
            else
            {
                // Normal case - single unambiguous mapping
                return mapping.Single().ToDateTimeUtc();
            }
        }

        // For intraday data with specific times, handle DST transitions carefully
        if (localTime.Hour != 0 || localTime.Minute != 0)
        {
            var mapping = dateTimeZone.MapLocal(localTime);

            if (mapping.Count == 2)
            {
                // Ambiguous time (fall back) - prefer the earlier occurrence
                return mapping.First().ToDateTimeUtc();
            }
            else if (mapping.Count == 0)
            {
                // Skipped time (spring forward) - use time after gap
                var intervalBefore = dateTimeZone.GetZoneInterval(instant.Minus(Duration.FromHours(1)));
                var intervalAfter = dateTimeZone.GetZoneInterval(instant.Plus(Duration.FromHours(1)));

                // Use the start of the interval after the gap
                return intervalAfter.Start.ToDateTimeUtc();
            }
        }

        // No DST issue detected, return original
        return utc;
    }

    /// <summary>
    /// Gets the timezone offset (UTC difference) for a specific datetime in the specified timezone.
    /// </summary>
    /// <param name="dateTime">The datetime to get the offset for.</param>
    /// <param name="timeZoneId">The IANA timezone ID.</param>
    /// <returns>The UTC offset as a TimeSpan.</returns>
    public TimeSpan GetTimezoneOffset(DateTime dateTime, string timeZoneId)
    {
        var dateTimeZone = GetDateTimeZone(timeZoneId);

        // Convert to Instant
        var instant = dateTime.Kind == DateTimeKind.Utc
            ? Instant.FromDateTimeUtc(dateTime)
            : Instant.FromDateTimeUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));

        // Get the offset at this instant
        var offset = dateTimeZone.GetUtcOffset(instant);

        return offset.ToTimeSpan();
    }

    /// <summary>
    /// Gets a NodaTime DateTimeZone from IANA timezone ID.
    /// Handles both IANA IDs (e.g., "America/New_York") and fallback to UTC.
    /// </summary>
    private static DateTimeZone GetDateTimeZone(string timeZoneId)
    {
        if (string.IsNullOrWhiteSpace(timeZoneId))
            return DateTimeZone.Utc;

        try
        {
            // Try IANA timezone ID (preferred)
            return TimeZoneProvider[timeZoneId];
        }
        catch (DateTimeZoneNotFoundException)
        {
            // Fallback: Try common mappings
            var mapped = MapToIanaTimezone(timeZoneId);
            if (mapped != null)
            {
                try
                {
                    return TimeZoneProvider[mapped];
                }
                catch
                {
                    // Fall through to UTC
                }
            }

            // Ultimate fallback to UTC
            return DateTimeZone.Utc;
        }
    }

    /// <summary>
    /// Maps common Windows timezone IDs to IANA timezone IDs.
    /// Yahoo Finance uses IANA IDs, but some clients might pass Windows IDs.
    /// </summary>
    private static string? MapToIanaTimezone(string timeZoneId)
    {
        // Common Windows to IANA mappings
        var mappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Eastern Standard Time", "America/New_York" },
            { "Central Standard Time", "America/Chicago" },
            { "Mountain Standard Time", "America/Denver" },
            { "Pacific Standard Time", "America/Los_Angeles" },
            { "GMT Standard Time", "Europe/London" },
            { "Central Europe Standard Time", "Europe/Paris" },
            { "Tokyo Standard Time", "Asia/Tokyo" },
            { "China Standard Time", "Asia/Shanghai" },
            { "India Standard Time", "Asia/Kolkata" },
            { "AUS Eastern Standard Time", "Australia/Sydney" }
        };

        return mappings.TryGetValue(timeZoneId, out var ianaId) ? ianaId : null;
    }
}
