namespace YFinance.Models;

/// <summary>
/// Yahoo Finance news item.
/// </summary>
public class NewsItem
{
    public string? Title { get; set; }
    public string? Publisher { get; set; }
    public string? Link { get; set; }
    public DateTime? ProviderPublishTime { get; set; }
    public string? Type { get; set; }
    public string? Uuid { get; set; }
    public List<string> RelatedTickers { get; set; } = new();
    public string RawJson { get; set; } = string.Empty;
}
