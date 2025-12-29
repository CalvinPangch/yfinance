namespace YFinance.NET.Models;

/// <summary>
/// Yahoo Finance search results.
/// </summary>
public class SearchResult
{
    public string Query { get; set; } = string.Empty;
    public List<SearchQuote> Quotes { get; set; } = new();
    public List<SearchNewsItem> News { get; set; } = new();
    public List<SearchListItem> Lists { get; set; } = new();
    public List<SearchResearchReport> Research { get; set; } = new();
    public List<SearchNavLink> Nav { get; set; } = new();
    public string RawResponse { get; set; } = string.Empty;
}

public class SearchQuote
{
    public string? Symbol { get; set; }
    public string? ShortName { get; set; }
    public string? LongName { get; set; }
    public string? Exchange { get; set; }
    public string? QuoteType { get; set; }
    public string? TypeDisp { get; set; }
    public string? ExchangeDisplay { get; set; }
    public string? Index { get; set; }
    public decimal? Score { get; set; }
    public string RawJson { get; set; } = string.Empty;
}

public class SearchNewsItem
{
    public string? Title { get; set; }
    public string? Publisher { get; set; }
    public string? Link { get; set; }
    public DateTime? ProviderPublishTime { get; set; }
    public string? Type { get; set; }
    public string? Uuid { get; set; }
    public string RawJson { get; set; } = string.Empty;
}

public class SearchListItem
{
    public string? ListId { get; set; }
    public string? DisplayName { get; set; }
    public string? ShortName { get; set; }
    public string? Url { get; set; }
    public int? ItemCount { get; set; }
    public string RawJson { get; set; } = string.Empty;
}

public class SearchResearchReport
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Provider { get; set; }
    public string? Link { get; set; }
    public DateTime? ReportDate { get; set; }
    public string RawJson { get; set; } = string.Empty;
}

public class SearchNavLink
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string RawJson { get; set; } = string.Empty;
}
