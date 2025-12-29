using System.Text.Json;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for holders and insider transactions.
/// </summary>
public class HoldersScraper : IHoldersScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;
    private readonly ISymbolValidator _symbolValidator;

    public HoldersScraper(IYahooFinanceClient client, IDataParser dataParser, ISymbolValidator symbolValidator)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
        _symbolValidator = symbolValidator ?? throw new ArgumentNullException(nameof(symbolValidator));
    }

    public async Task<HolderData> GetHoldersAsync(string symbol, CancellationToken cancellationToken = default)
    {
        // Validate symbol for security (prevents URL injection)
        _symbolValidator.ValidateAndThrow(symbol, nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "majorHoldersBreakdown,institutionOwnership,insiderTransactions,insiderHolders,fundOwnership,majorDirectHolders,netSharePurchaseActivity" }
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

        if (result.TryGetProperty("insiderHolders", out var insiderHolders) &&
            insiderHolders.TryGetProperty("holders", out var holders) &&
            holders.ValueKind == JsonValueKind.Array)
        {
            holderData.InsiderHolders = new List<InsiderHolder>();

            foreach (var entry in holders.EnumerateArray())
            {
                holderData.InsiderHolders.Add(new InsiderHolder
                {
                    Name = entry.TryGetProperty("name", out var name) && name.ValueKind == JsonValueKind.String
                        ? name.GetString() ?? string.Empty
                        : string.Empty,
                    Relation = entry.TryGetProperty("relation", out var relation) && relation.ValueKind == JsonValueKind.String
                        ? relation.GetString()
                        : null,
                    Url = entry.TryGetProperty("url", out var url) && url.ValueKind == JsonValueKind.String
                        ? url.GetString()
                        : null,
                    TransactionDescription = entry.TryGetProperty("transactionDescription", out var description) && description.ValueKind == JsonValueKind.String
                        ? description.GetString()
                        : null,
                    LatestTransDate = ParseDate(entry, "latestTransDate"),
                    PositionDirect = _dataParser.ExtractDecimal(entry.TryGetProperty("positionDirect", out var positionDirect) ? positionDirect : default),
                    PositionDirectDate = ParseDate(entry, "positionDirectDate"),
                    PositionIndirect = _dataParser.ExtractDecimal(entry.TryGetProperty("positionIndirect", out var positionIndirect) ? positionIndirect : default),
                    PositionIndirectDate = ParseDate(entry, "positionIndirectDate")
                });
            }
        }

        if (result.TryGetProperty("fundOwnership", out var fundOwnership) &&
            fundOwnership.TryGetProperty("ownershipList", out var fundList) &&
            fundList.ValueKind == JsonValueKind.Array)
        {
            holderData.FundHolders = new List<FundHolder>();

            foreach (var entry in fundList.EnumerateArray())
            {
                holderData.FundHolders.Add(new FundHolder
                {
                    Holder = entry.TryGetProperty("organization", out var organization) && organization.ValueKind == JsonValueKind.String
                        ? organization.GetString() ?? string.Empty
                        : string.Empty,
                    Shares = entry.TryGetProperty("position", out var shares) && shares.ValueKind == JsonValueKind.Number ? shares.GetInt64() : 0L,
                    DateReported = entry.TryGetProperty("reportDate", out var reportDate) && reportDate.ValueKind == JsonValueKind.Number
                        ? DateTimeOffset.FromUnixTimeSeconds(reportDate.GetInt64()).UtcDateTime
                        : DateTime.MinValue,
                    PercentOut = entry.TryGetProperty("pctHeld", out var pct) ? _dataParser.ExtractDecimal(pct) ?? 0m : 0m,
                    Value = entry.TryGetProperty("value", out var value) ? _dataParser.ExtractDecimal(value) ?? 0m : 0m
                });
            }
        }

        if (result.TryGetProperty("majorDirectHolders", out var directHolders) &&
            directHolders.TryGetProperty("holders", out var directList) &&
            directList.ValueKind == JsonValueKind.Array)
        {
            holderData.MajorDirectHolders = new List<MajorDirectHolder>();

            foreach (var entry in directList.EnumerateArray())
            {
                holderData.MajorDirectHolders.Add(new MajorDirectHolder
                {
                    Holder = entry.TryGetProperty("organization", out var organization) && organization.ValueKind == JsonValueKind.String
                        ? organization.GetString() ?? string.Empty
                        : string.Empty,
                    Shares = entry.TryGetProperty("positionDirect", out var shares) && shares.ValueKind == JsonValueKind.Number ? shares.GetInt64() : 0L,
                    DateReported = entry.TryGetProperty("reportDate", out var reportDate) && reportDate.ValueKind == JsonValueKind.Number
                        ? DateTimeOffset.FromUnixTimeSeconds(reportDate.GetInt64()).UtcDateTime
                        : DateTime.MinValue,
                    Value = entry.TryGetProperty("valueDirect", out var value) ? _dataParser.ExtractDecimal(value) ?? 0m : 0m
                });
            }
        }

        if (result.TryGetProperty("netSharePurchaseActivity", out var netSharePurchase) &&
            netSharePurchase.ValueKind == JsonValueKind.Object)
        {
            holderData.InsiderPurchases = new InsiderPurchaseActivity
            {
                Period = netSharePurchase.TryGetProperty("period", out var period) && period.ValueKind == JsonValueKind.String
                    ? period.GetString() ?? string.Empty
                    : string.Empty,
                BuyInfoShares = _dataParser.ExtractDecimal(netSharePurchase.GetPropertyOrDefault("buyInfoShares")),
                SellInfoShares = _dataParser.ExtractDecimal(netSharePurchase.GetPropertyOrDefault("sellInfoShares")),
                NetInfoShares = _dataParser.ExtractDecimal(netSharePurchase.GetPropertyOrDefault("netInfoShares")),
                TotalInsiderShares = _dataParser.ExtractDecimal(netSharePurchase.GetPropertyOrDefault("totalInsiderShares")),
                NetPercentInsiderShares = _dataParser.ExtractDecimal(netSharePurchase.GetPropertyOrDefault("netPercentInsiderShares")),
                BuyPercentInsiderShares = _dataParser.ExtractDecimal(netSharePurchase.GetPropertyOrDefault("buyPercentInsiderShares")),
                SellPercentInsiderShares = _dataParser.ExtractDecimal(netSharePurchase.GetPropertyOrDefault("sellPercentInsiderShares")),
                BuyInfoCount = _dataParser.ExtractDecimal(netSharePurchase.GetPropertyOrDefault("buyInfoCount")),
                SellInfoCount = _dataParser.ExtractDecimal(netSharePurchase.GetPropertyOrDefault("sellInfoCount")),
                NetInfoCount = _dataParser.ExtractDecimal(netSharePurchase.GetPropertyOrDefault("netInfoCount"))
            };
        }

        return holderData;
    }

    private DateTime? ParseDate(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var value) || value.ValueKind == JsonValueKind.Null)
            return null;

        if (value.ValueKind == JsonValueKind.Number && value.TryGetInt64(out var unix))
            return DateTimeOffset.FromUnixTimeSeconds(unix).UtcDateTime;

        if (value.ValueKind == JsonValueKind.Object &&
            value.TryGetProperty("raw", out var raw) &&
            raw.ValueKind == JsonValueKind.Number)
        {
            return DateTimeOffset.FromUnixTimeSeconds(raw.GetInt64()).UtcDateTime;
        }

        return null;
    }
}
