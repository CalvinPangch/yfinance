namespace YFinance.Models.Requests;

/// <summary>
/// Request model for Yahoo Finance screeners.
/// </summary>
public class ScreenerRequest
{
    public string? PredefinedId { get; set; }
    public ScreenerQuery? Query { get; set; }
    public int? Offset { get; set; }
    public int? Count { get; set; }
    public int? Size { get; set; }
    public string? SortField { get; set; }
    public bool? SortAsc { get; set; }
    public string? UserId { get; set; }
    public string? UserIdType { get; set; }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(PredefinedId) && Query == null)
            throw new ArgumentException("PredefinedId or Query must be provided.");

        if (!string.IsNullOrWhiteSpace(PredefinedId) && Query != null)
            throw new ArgumentException("PredefinedId and Query are mutually exclusive.");

        if (Count.HasValue && Count.Value > 250)
            throw new ArgumentException("Yahoo limits query count to 250, reduce Count.");

        if (Size.HasValue && Size.Value > 250)
            throw new ArgumentException("Yahoo limits query size to 250, reduce Size.");
    }
}
