using YFinance.NET.Models.Enums;

namespace YFinance.NET.Models.Requests;

/// <summary>
/// Request model for lookup queries.
/// </summary>
public class LookupRequest
{
    public string Query { get; set; } = string.Empty;
    public LookupType Type { get; set; } = LookupType.All;
    public int Count { get; set; } = 25;
    public bool FetchPricingData { get; set; } = true;
    public string Lang { get; set; } = "en-US";
    public string Region { get; set; } = "US";

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Query))
            throw new ArgumentException("Query cannot be null or empty.", nameof(Query));

        if (Count <= 0)
            throw new ArgumentException("Count must be greater than zero.", nameof(Count));
    }
}
