using System.Text.Json;
using System.Linq;
using YFinance.NET.Implementation.Constants;
using YFinance.NET.Implementation.Utils;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance news.
/// </summary>
public class NewsScraper : INewsScraper
{
    private static readonly Dictionary<string, string> TabQueryRefs = new(StringComparer.OrdinalIgnoreCase)
    {
        ["all"] = "newsAll",
        ["news"] = "latestNews",
        ["press releases"] = "pressRelease"
    };

    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;

    public NewsScraper(IYahooFinanceClient client, IDataParser dataParser)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
    }

    public async Task<IReadOnlyList<NewsItem>> GetNewsAsync(NewsRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        if (!TabQueryRefs.TryGetValue(request.Tab, out var queryRef))
            throw new ArgumentException($"Invalid tab name '{request.Tab}'.", nameof(request.Tab));

        var endpoint = $"{YahooFinanceConstants.BaseUrls.Finance}/xhr/ncp?queryRef={queryRef}&serviceKey=ncp_fin";
        var payload = new
        {
            serviceConfig = new
            {
                snippetCount = request.Count,
                s = new[] { request.Symbol }
            }
        };

        var jsonBody = JsonSerializer.Serialize(payload);
        var jsonResponse = await _client.PostAsync(endpoint, jsonBody, cancellationToken).ConfigureAwait(false);
        return ParseNews(jsonResponse);
    }

    private IReadOnlyList<NewsItem> ParseNews(string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;
        var items = new List<NewsItem>();

        if (!root.TryGetProperty("data", out var data) ||
            !data.TryGetProperty("tickerStream", out var tickerStream) ||
            !tickerStream.TryGetProperty("stream", out var stream) ||
            stream.ValueKind != JsonValueKind.Array)
        {
            return items;
        }

        foreach (var article in stream.EnumerateArray())
        {
            if (article.TryGetProperty("ad", out var adElement) &&
                adElement.ValueKind == JsonValueKind.Array &&
                adElement.GetArrayLength() > 0)
            {
                continue;
            }

            items.Add(ParseNewsItem(article));
        }

        return items;
    }

    private NewsItem ParseNewsItem(JsonElement element)
    {
        var publishTime = element.GetLongOrNull("providerPublishTime");
        var related = new List<string>();

        if (element.TryGetProperty("relatedTickers", out var relatedElement) &&
            relatedElement.ValueKind == JsonValueKind.Array)
        {
            foreach (var ticker in relatedElement.EnumerateArray())
            {
                if (ticker.ValueKind == JsonValueKind.String)
                    related.Add(ticker.GetString() ?? string.Empty);
            }
        }

        return new NewsItem
        {
            Title = element.GetStringOrNull("title"),
            Publisher = element.GetStringOrNull("publisher"),
            Link = element.GetStringOrNull("link"),
            ProviderPublishTime = publishTime.HasValue ? _dataParser.UnixTimestampToDateTime(publishTime.Value) : null,
            Type = element.GetStringOrNull("type"),
            Uuid = element.GetStringOrNull("uuid"),
            RelatedTickers = related.Where(value => !string.IsNullOrWhiteSpace(value)).ToList(),
            RawJson = element.GetRawText()
        };
    }
}
