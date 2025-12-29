namespace YFinance.NET.Interfaces.Utils;

/// <summary>
/// Utility interface for timezone handling and DST fixes.
/// Handles Yahoo Finance's timezone metadata and DST transition issues.
/// </summary>
public interface ITimezoneHelper
{
    /// <summary>
    /// Converts UTC datetime to exchange timezone.
    /// </summary>
    /// <param name="utcDateTime">DateTime in UTC</param>
    /// <param name="timeZoneId">IANA timezone identifier (e.g., "America/New_York")</param>
    /// <returns>DateTime in exchange timezone</returns>
    DateTime ConvertToExchangeTime(DateTime utcDateTime, string timeZoneId);

    /// <summary>
    /// Fixes Yahoo Finance DST issues in historical data.
    /// </summary>
    /// <param name="timestamps">Array of timestamps to fix</param>
    /// <param name="timeZoneId">IANA timezone identifier</param>
    /// <returns>Corrected timestamps</returns>
    DateTime[] FixDstIssues(DateTime[] timestamps, string timeZoneId);

    /// <summary>
    /// Gets the timezone offset for a specific date and timezone.
    /// </summary>
    /// <param name="dateTime">DateTime to get offset for</param>
    /// <param name="timeZoneId">IANA timezone identifier</param>
    /// <returns>Offset from UTC</returns>
    TimeSpan GetTimezoneOffset(DateTime dateTime, string timeZoneId);
}
