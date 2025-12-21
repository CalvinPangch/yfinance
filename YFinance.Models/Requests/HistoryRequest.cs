using YFinance.Models.Enums;

namespace YFinance.Models.Requests;

/// <summary>
/// Request model for historical data retrieval.
/// </summary>
public class HistoryRequest
{
    /// <summary>
    /// Time period for historical data. Mutually exclusive with Start/End dates.
    /// </summary>
    public Period? Period { get; set; }

    /// <summary>
    /// Data interval (e.g., daily, weekly).
    /// </summary>
    public Interval Interval { get; set; } = Interval.OneDay;

    /// <summary>
    /// Start date for custom range. Requires End date.
    /// </summary>
    public DateTime? Start { get; set; }

    /// <summary>
    /// End date for custom range. Requires Start date.
    /// </summary>
    public DateTime? End { get; set; }

    /// <summary>
    /// Whether to apply price repair for 100x errors and bad splits.
    /// </summary>
    public bool Repair { get; set; } = false;

    /// <summary>
    /// Whether to auto-adjust prices for splits and dividends.
    /// </summary>
    public bool AutoAdjust { get; set; } = true;

    /// <summary>
    /// Validates the request parameters.
    /// </summary>
    public void Validate()
    {
        if (Period.HasValue && (Start.HasValue || End.HasValue))
        {
            throw new ArgumentException("Cannot specify both Period and Start/End dates.");
        }

        if (!Period.HasValue && (!Start.HasValue || !End.HasValue))
        {
            throw new ArgumentException("Must specify either Period or both Start and End dates.");
        }

        if (Start.HasValue && End.HasValue && Start.Value >= End.Value)
        {
            throw new ArgumentException("Start date must be before End date.");
        }
    }
}
