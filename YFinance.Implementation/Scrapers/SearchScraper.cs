using System.Text.Json;
using YFinance.Implementation.Constants;
using YFinance.Implementation.Utils;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Interfaces.Utils;
using YFinance.Models;
using YFinance.Models.Requests;

namespace YFinance.Implementation.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance search results.
/// </summary>
public class SearchScraper : ISearchScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;

    public SearchScraper(IYahooFinanceClient client, IDataParser dataParser)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
    }

    public async Task<SearchResult> SearchAsync(SearchRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        var queryParams = new Dictionary<string, string>
        {
            ["q"] = request.Query,
            ["quotesCount"] = request.QuotesCount.ToString(),
            ["enableFuzzyQuery"] = request.EnableFuzzyQuery.ToString().ToLowerInvariant(),
            ["newsCount"] = request.NewsCount.ToString(),
            ["quotesQueryId"] = "tss_match_phrase_query",
            ["newsQueryId"] = "news_cie_vespa",
            ["listsCount"] = request.ListsCount.ToString(),
            ["enableCb"] = request.IncludeCompanyBreakdown.ToString().ToLowerInvariant(),
            ["enableNavLinks"] = request.IncludeNavLinks.ToString().ToLowerInvariant(),
            ["enableResearchReports"] = request.IncludeResearchReports.ToString().ToLowerInvariant(),
            ["enableCulturalAssets"] = request.IncludeCulturalAssets.ToString().ToLowerInvariant(),
            ["recommendedCount"] = request.RecommendedCount.ToString()
        };

        var endpoint = $"{YahooFinanceConstants.BaseUrls.Query2}{YahooFinanceConstants.Endpoints.Search}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseSearchResult(request.Query, jsonResponse);
    }

    private SearchResult ParseSearchResult(string query, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        var result = new SearchResult
        {
            Query = query,
            RawResponse = jsonResponse
        };

        if (root.TryGetProperty("quotes", out var quotesElement) &&
            quotesElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var quoteElement in quotesElement.EnumerateArray())
            {
                if (quoteElement.TryGetProperty("symbol", out var _))
                    result.Quotes.Add(ParseQuote(quoteElement));
            }
        }

        if (root.TryGetProperty("news", out var newsElement) &&
            newsElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var newsItem in newsElement.EnumerateArray())
            {
                result.News.Add(ParseNewsItem(newsItem));
            }
        }

        if (root.TryGetProperty("lists", out var listsElement) &&
            listsElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var listItem in listsElement.EnumerateArray())
            {
                result.Lists.Add(ParseListItem(listItem));
            }
        }

        if (root.TryGetProperty("researchReports", out var researchElement) &&
            researchElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var report in researchElement.EnumerateArray())
            {
                result.Research.Add(ParseResearchReport(report));
            }
        }

        if (root.TryGetProperty("nav", out var navElement) &&
            navElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var navItem in navElement.EnumerateArray())
            {
                result.Nav.Add(ParseNavLink(navItem));
            }
        }

        return result;
    }

    private SearchQuote ParseQuote(JsonElement element)
    {
        return new SearchQuote
        {
            Symbol = element.GetStringOrNull("symbol"),
            ShortName = element.GetStringOrNull("shortname", "shortName"),
            LongName = element.GetStringOrNull("longname", "longName"),
            Exchange = element.GetStringOrNull("exchange"),
            QuoteType = element.GetStringOrNull("quoteType"),
            TypeDisp = element.GetStringOrNull("typeDisp"),
            ExchangeDisplay = element.GetStringOrNull("exchDisp"),
            Index = element.GetStringOrNull("index"),
            Score = element.GetDecimalOrNull("score"),
            RawJson = element.GetRawText()
        };
    }

    private SearchNewsItem ParseNewsItem(JsonElement element)
    {
        var publishTime = element.GetLongOrNull("providerPublishTime");

        return new SearchNewsItem
        {
            Title = element.GetStringOrNull("title"),
            Publisher = element.GetStringOrNull("publisher"),
            Link = element.GetStringOrNull("link"),
            ProviderPublishTime = publishTime.HasValue ? _dataParser.UnixTimestampToDateTime(publishTime.Value) : null,
            Type = element.GetStringOrNull("type"),
            Uuid = element.GetStringOrNull("uuid"),
            RawJson = element.GetRawText()
        };
    }

    private static SearchListItem ParseListItem(JsonElement element)
    {
        return new SearchListItem
        {
            ListId = element.GetStringOrNull("listId", "listID"),
            DisplayName = element.GetStringOrNull("displayName", "name"),
            ShortName = element.GetStringOrNull("shortName", "shortname"),
            Url = element.GetStringOrNull("url"),
            ItemCount = element.GetLongOrNull("itemCount") is { } count ? (int?)count : null,
            RawJson = element.GetRawText()
        };
    }

    private SearchResearchReport ParseResearchReport(JsonElement element)
    {
        var reportTime = element.GetLongOrNull("reportDate");

        return new SearchResearchReport
        {
            Id = element.GetStringOrNull("id"),
            Title = element.GetStringOrNull("title"),
            Provider = element.GetStringOrNull("provider"),
            Link = element.GetStringOrNull("link"),
            ReportDate = reportTime.HasValue ? _dataParser.UnixTimestampToDateTime(reportTime.Value) : null,
            RawJson = element.GetRawText()
        };
    }

    private static SearchNavLink ParseNavLink(JsonElement element)
    {
        return new SearchNavLink
        {
            Id = element.GetStringOrNull("id"),
            Name = element.GetStringOrNull("name"),
            Url = element.GetStringOrNull("url"),
            RawJson = element.GetRawText()
        };
    }
}
