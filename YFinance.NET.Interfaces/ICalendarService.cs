using YFinance.NET.Models;

namespace YFinance.NET.Interfaces;

/// <summary>
/// Service for Yahoo Finance calendar visualization endpoints.
/// </summary>
public interface ICalendarService
{
    /// <summary>
    /// Gets calendar data based on the specified request.
    /// </summary>
    /// <param name="request">The calendar request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The calendar result data.</returns>
    Task<CalendarResult> GetCalendarAsync(CalendarRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets earnings calendar data for a specified date range.
    /// </summary>
    /// <param name="start">The start date (optional).</param>
    /// <param name="end">The end date (optional).</param>
    /// <param name="limit">The maximum number of results (default: 12).</param>
    /// <param name="offset">The offset for pagination (default: 0).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The earnings calendar result.</returns>
    Task<CalendarResult> GetEarningsCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets IPO calendar data for a specified date range.
    /// </summary>
    /// <param name="start">The start date (optional).</param>
    /// <param name="end">The end date (optional).</param>
    /// <param name="limit">The maximum number of results (default: 12).</param>
    /// <param name="offset">The offset for pagination (default: 0).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The IPO calendar result.</returns>
    Task<CalendarResult> GetIpoCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets economic events calendar data for a specified date range.
    /// </summary>
    /// <param name="start">The start date (optional).</param>
    /// <param name="end">The end date (optional).</param>
    /// <param name="limit">The maximum number of results (default: 12).</param>
    /// <param name="offset">The offset for pagination (default: 0).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The economic events calendar result.</returns>
    Task<CalendarResult> GetEconomicEventsCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets stock splits calendar data for a specified date range.
    /// </summary>
    /// <param name="start">The start date (optional).</param>
    /// <param name="end">The end date (optional).</param>
    /// <param name="limit">The maximum number of results (default: 12).</param>
    /// <param name="offset">The offset for pagination (default: 0).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The splits calendar result.</returns>
    Task<CalendarResult> GetSplitsCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default);
}
