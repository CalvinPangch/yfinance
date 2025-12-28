using System.Text.Json;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Interfaces.Utils;
using YFinance.Models;

namespace YFinance.Implementation.Scrapers;

/// <summary>
/// Scraper for holders and insider transactions.
/// </summary>
public class HoldersScraper : IHoldersScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;

    public HoldersScraper(IYahooFinanceClient client, IDataParser dataParser)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
    }

    public async Task<HolderData> GetHoldersAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or whitespace.", nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "majorHoldersBreakdown,institutionOwnership,insiderTransactions" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseHolderData(symbol, jsonResponse);
    }

    private HolderData ParseHolderData(string symbol, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.GetArrayLength() == 0)
        {
            return new HolderData { Symbol = symbol };
        }

        var result = results[0];
        var holderData = new HolderData { Symbol = symbol };

        if (result.TryGetProperty("majorHoldersBreakdown", out var major))
        {
            holderData.InsidersPercentHeld = _dataParser.ExtractDecimal(major.GetPropertyOrDefault("insidersPercentHeld"));
            holderData.InstitutionsPercentHeld = _dataParser.ExtractDecimal(major.GetPropertyOrDefault("institutionsPercentHeld"));
            holderData.InstitutionsFloatPercentHeld = _dataParser.ExtractDecimal(major.GetPropertyOrDefault("institutionsFloatPercentHeld"));
            if (major.TryGetProperty("institutionsCount", out var count) && count.ValueKind == JsonValueKind.Number)
                holderData.InstitutionsCount = count.GetInt32();
        }

        if (result.TryGetProperty("institutionOwnership", out var ownership) &&
            ownership.TryGetProperty("ownershipList", out var ownershipList) &&
            ownershipList.ValueKind == JsonValueKind.Array)
        {
            holderData.InstitutionalHolders = new List<InstitutionalHolder>();

            foreach (var entry in ownershipList.EnumerateArray())
            {
                var holder = new InstitutionalHolder
                {
                    Holder = entry.GetPropertyOrDefault("organization").GetString() ?? string.Empty,
                    Shares = entry.TryGetProperty("position", out var shares) && shares.ValueKind == JsonValueKind.Number ? shares.GetInt64() : 0L,
                    DateReported = entry.TryGetProperty("reportDate", out var reportDate) && reportDate.ValueKind == JsonValueKind.Number
                        ? DateTimeOffset.FromUnixTimeSeconds(reportDate.GetInt64()).UtcDateTime
                        : DateTime.MinValue,
                    PercentOut = entry.TryGetProperty("pctHeld", out var pct) ? _dataParser.ExtractDecimal(pct) ?? 0m : 0m,
                    Value = entry.TryGetProperty("value", out var value) ? _dataParser.ExtractDecimal(value) ?? 0m : 0m
                };

                holderData.InstitutionalHolders.Add(holder);
            }
        }

        if (result.TryGetProperty("insiderTransactions", out var insider) &&
            insider.TryGetProperty("transactions", out var transactions) &&
            transactions.ValueKind == JsonValueKind.Array)
        {
            holderData.InsiderTransactions = new List<InsiderTransaction>();

            foreach (var entry in transactions.EnumerateArray())
            {
                var transaction = new InsiderTransaction
                {
                    Insider = entry.GetPropertyOrDefault("filerName").GetString() ?? string.Empty,
                    Relation = entry.GetPropertyOrDefault("filerRelation").GetString() ?? string.Empty,
                    TransactionDate = entry.TryGetProperty("startDate", out var date) && date.ValueKind == JsonValueKind.Number
                        ? DateTimeOffset.FromUnixTimeSeconds(date.GetInt64()).UtcDateTime
                        : DateTime.MinValue,
                    TransactionType = entry.GetPropertyOrDefault("transactionText").GetString() ?? string.Empty,
                    Shares = entry.TryGetProperty("shares", out var shares) && shares.ValueKind == JsonValueKind.Number ? shares.GetInt32() : 0,
                    Value = entry.TryGetProperty("value", out var value) ? _dataParser.ExtractDecimal(value) : null
                };

                holderData.InsiderTransactions.Add(transaction);
            }
        }

        return holderData;
    }
}
