using System.Text.Json;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance ESG data.
/// </summary>
public class EsgScraper : IEsgScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;
    private readonly ISymbolValidator _symbolValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="EsgScraper"/> class.
    /// </summary>
    /// <param name="client">The Yahoo Finance HTTP client.</param>
    /// <param name="dataParser">The data parser for JSON processing.</param>
    /// <param name="symbolValidator">The symbol validator for security.</param>
    public EsgScraper(IYahooFinanceClient client, IDataParser dataParser, ISymbolValidator symbolValidator)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
        _symbolValidator = symbolValidator ?? throw new ArgumentNullException(nameof(symbolValidator));
    }

    /// <summary>
    /// Gets ESG (Environmental, Social, and Governance) scores for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>ESG data for the symbol.</returns>
    public async Task<EsgData> GetEsgAsync(string symbol, CancellationToken cancellationToken = default)
    {
        // Validate symbol for security (prevents URL injection)
        _symbolValidator.ValidateAndThrow(symbol, nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "esgScores" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseEsgData(symbol, jsonResponse);
    }

    private EsgData ParseEsgData(string symbol, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.ValueKind != JsonValueKind.Array ||
            results.GetArrayLength() == 0)
        {
            return new EsgData { Symbol = symbol };
        }

        var result = results[0];
        if (!result.TryGetProperty("esgScores", out var esgScores) ||
            esgScores.ValueKind != JsonValueKind.Object)
        {
            return new EsgData { Symbol = symbol };
        }

        return new EsgData
        {
            Symbol = symbol,
            TotalEsg = _dataParser.ExtractDecimal(esgScores.TryGetProperty("totalEsg", out var totalEsg) ? totalEsg : default),
            EnvironmentScore = _dataParser.ExtractDecimal(esgScores.TryGetProperty("environmentScore", out var envScore) ? envScore : default),
            SocialScore = _dataParser.ExtractDecimal(esgScores.TryGetProperty("socialScore", out var socialScore) ? socialScore : default),
            GovernanceScore = _dataParser.ExtractDecimal(esgScores.TryGetProperty("governanceScore", out var govScore) ? govScore : default),
            RatingYear = GetIntValue(esgScores, "ratingYear"),
            RatingMonth = GetIntValue(esgScores, "ratingMonth"),
            HighestControversy = GetIntValue(esgScores, "highestControversy"),
            PeerCount = GetIntValue(esgScores, "peerCount"),
            PeerGroup = GetStringValue(esgScores, "peerGroup"),
            EsgPerformance = GetStringValue(esgScores, "esgPerformance"),
            PeerEsgScorePerformance = GetStringValue(esgScores, "peerEsgScorePerformance"),
            PeerEnvironmentPerformance = GetStringValue(esgScores, "peerEnvironmentPerformance"),
            PeerSocialPerformance = GetStringValue(esgScores, "peerSocialPerformance"),
            PeerGovernancePerformance = GetStringValue(esgScores, "peerGovernancePerformance"),
            Percentile = _dataParser.ExtractDecimal(esgScores.TryGetProperty("percentile", out var percentile) ? percentile : default),
            EnvironmentPercentile = _dataParser.ExtractDecimal(esgScores.TryGetProperty("environmentPercentile", out var envPercentile) ? envPercentile : default),
            SocialPercentile = _dataParser.ExtractDecimal(esgScores.TryGetProperty("socialPercentile", out var socialPercentile) ? socialPercentile : default),
            GovernancePercentile = _dataParser.ExtractDecimal(esgScores.TryGetProperty("governancePercentile", out var govPercentile) ? govPercentile : default)
        };
    }

    private static int? GetIntValue(JsonElement element, string propertyName)
    {
        return element.TryGetProperty(propertyName, out var value) && value.ValueKind == JsonValueKind.Number
            ? value.GetInt32()
            : null;
    }

    private static string? GetStringValue(JsonElement element, string propertyName)
    {
        return element.TryGetProperty(propertyName, out var value) && value.ValueKind == JsonValueKind.String
            ? value.GetString()
            : null;
    }
}
