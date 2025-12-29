using YFinance.NET.Models;

namespace YFinance.NET.Interfaces;

/// <summary>
/// Service for Yahoo Finance calendar visualization endpoints.
/// </summary>
public interface ICalendarService
{
    Task<CalendarResult> GetCalendarAsync(CalendarRequest request, CancellationToken cancellationToken = default);
    Task<CalendarResult> GetEarningsCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default);
    Task<CalendarResult> GetIpoCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default);
    Task<CalendarResult> GetEconomicEventsCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default);
    Task<CalendarResult> GetSplitsCalendarAsync(DateTime? start = null, DateTime? end = null, int limit = 12, int offset = 0, CancellationToken cancellationToken = default);
}
