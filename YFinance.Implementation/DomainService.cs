using System.Text.Json;
using YFinance.Interfaces;
using YFinance.Models;

namespace YFinance.Implementation;

/// <summary>
/// Service for sector, industry, and market domain data.
/// </summary>
public class DomainService : IDomainService
{
    private readonly IYahooFinanceClient _client;

    public DomainService(IYahooFinanceClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<SectorData> GetSectorAsync(string sectorKey, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sectorKey))
            throw new ArgumentException("Sector key cannot be null or empty.", nameof(sectorKey));

        var endpoint = $"/v1/finance/sectors/{sectorKey}";
        var json = await _client.GetAsync(endpoint, BuildDomainParams(), cancellationToken).ConfigureAwait(false);
        return ParseSector(sectorKey, json);
    }

    public async Task<IndustryData> GetIndustryAsync(string industryKey, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(industryKey))
            throw new ArgumentException("Industry key cannot be null or empty.", nameof(industryKey));

        var endpoint = $"/v1/finance/industries/{industryKey}";
        var json = await _client.GetAsync(endpoint, BuildDomainParams(), cancellationToken).ConfigureAwait(false);
        return ParseIndustry(industryKey, json);
    }

    public async Task<MarketData> GetMarketAsync(string market, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(market))
            throw new ArgumentException("Market cannot be null or empty.", nameof(market));

        var summary = await _client.GetAsync(
            "/v6/finance/quote/marketSummary",
            new Dictionary<string, string>
            {
                ["fields"] = "shortName,regularMarketPrice,regularMarketChange,regularMarketChangePercent",
                ["formatted"] = "false",
                ["lang"] = "en-US",
                ["market"] = market
            },
            cancellationToken).ConfigureAwait(false);

        var status = await _client.GetAsync(
            "/v6/finance/markettime",
            new Dictionary<string, string>
            {
                ["formatted"] = "true",
                ["key"] = "finance",
                ["lang"] = "en-US",
                ["market"] = market
            },
            cancellationToken).ConfigureAwait(false);

        return ParseMarket(market, summary, status);
    }

    private static Dictionary<string, string> BuildDomainParams()
    {
        return new Dictionary<string, string>
        {
            ["formatted"] = "true",
            ["withReturns"] = "true",
            ["lang"] = "en-US",
            ["region"] = "US"
        };
    }

    private static SectorData ParseSector(string key, string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("data", out var data))
            return new SectorData { Key = key };

        var sector = new SectorData
        {
            Key = key,
            Name = data.TryGetProperty("name", out var name) ? name.GetString() : null,
            Symbol = data.TryGetProperty("symbol", out var symbol) ? symbol.GetString() : null,
            Overview = ParseOverview(data)
        };

        sector.TopCompanies = ParseTopCompanies(data);
        sector.TopEtfs = ParseSymbolDictionary(data, "topETFs");
        sector.TopMutualFunds = ParseSymbolDictionary(data, "topMutualFunds");
        sector.Industries = ParseIndustries(data);
        sector.ResearchReports = ParseResearchReports(data);

        return sector;
    }

    private static IndustryData ParseIndustry(string key, string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("data", out var data))
            return new IndustryData { Key = key };

        var industry = new IndustryData
        {
            Key = key,
            Name = data.TryGetProperty("name", out var name) ? name.GetString() : null,
            Symbol = data.TryGetProperty("symbol", out var symbol) ? symbol.GetString() : null,
            SectorKey = data.TryGetProperty("sectorKey", out var sectorKey) ? sectorKey.GetString() : null,
            SectorName = data.TryGetProperty("sectorName", out var sectorName) ? sectorName.GetString() : null,
            Overview = ParseOverview(data)
        };

        industry.TopCompanies = ParseTopCompanies(data);
        industry.TopPerformingCompanies = ParsePerformanceCompanies(data, "topPerformingCompanies");
        industry.TopGrowthCompanies = ParseGrowthCompanies(data, "topGrowthCompanies");
        industry.ResearchReports = ParseResearchReports(data);

        return industry;
    }

    private static MarketData ParseMarket(string market, string summaryJson, string statusJson)
    {
        var marketData = new MarketData { Market = market };
        marketData.Summary = ParseMarketSummary(summaryJson);
        marketData.Status = ParseMarketStatus(statusJson);
        return marketData;
    }

    private static DomainOverview? ParseOverview(JsonElement data)
    {
        if (!data.TryGetProperty("overview", out var overview))
            return null;

        return new DomainOverview
        {
            CompaniesCount = TryGetInt(overview, "companiesCount"),
            MarketCap = TryGetDecimalRaw(overview, "marketCap"),
            MessageBoardId = overview.TryGetProperty("messageBoardId", out var messageBoard) ? messageBoard.GetString() : null,
            Description = overview.TryGetProperty("description", out var description) ? description.GetString() : null,
            IndustriesCount = TryGetInt(overview, "industriesCount"),
            MarketWeight = TryGetDecimalRaw(overview, "marketWeight"),
            EmployeeCount = TryGetLongRaw(overview, "employeeCount")
        };
    }

    private static List<DomainCompany> ParseTopCompanies(JsonElement data)
    {
        var output = new List<DomainCompany>();
        if (!data.TryGetProperty("topCompanies", out var companies) || companies.ValueKind != JsonValueKind.Array)
            return output;

        foreach (var entry in companies.EnumerateArray())
        {
            output.Add(new DomainCompany
            {
                Symbol = entry.TryGetProperty("symbol", out var symbol) ? symbol.GetString() ?? string.Empty : string.Empty,
                Name = entry.TryGetProperty("name", out var name) ? name.GetString() : null,
                Rating = TryGetDecimal(entry, "rating"),
                MarketWeight = TryGetDecimalRaw(entry, "marketWeight")
            });
        }

        return output;
    }

    private static List<IndustryEntry> ParseIndustries(JsonElement data)
    {
        var output = new List<IndustryEntry>();
        if (!data.TryGetProperty("industries", out var industries) || industries.ValueKind != JsonValueKind.Array)
            return output;

        foreach (var entry in industries.EnumerateArray())
        {
            var name = entry.TryGetProperty("name", out var nameElement) ? nameElement.GetString() : null;
            if (string.Equals(name, "All Industries", StringComparison.OrdinalIgnoreCase))
                continue;

            output.Add(new IndustryEntry
            {
                Key = entry.TryGetProperty("key", out var keyElement) ? keyElement.GetString() ?? string.Empty : string.Empty,
                Name = name,
                Symbol = entry.TryGetProperty("symbol", out var symbolElement) ? symbolElement.GetString() : null,
                MarketWeight = TryGetDecimalRaw(entry, "marketWeight")
            });
        }

        return output;
    }

    private static List<IndustryPerformanceEntry> ParsePerformanceCompanies(JsonElement data, string propertyName)
    {
        var output = new List<IndustryPerformanceEntry>();
        if (!data.TryGetProperty(propertyName, out var companies) || companies.ValueKind != JsonValueKind.Array)
            return output;

        foreach (var entry in companies.EnumerateArray())
        {
            output.Add(new IndustryPerformanceEntry
            {
                Symbol = entry.TryGetProperty("symbol", out var symbol) ? symbol.GetString() ?? string.Empty : string.Empty,
                Name = entry.TryGetProperty("name", out var name) ? name.GetString() : null,
                YtdReturn = TryGetDecimalRaw(entry, "ytdReturn"),
                LastPrice = TryGetDecimalRaw(entry, "lastPrice"),
                TargetPrice = TryGetDecimalRaw(entry, "targetPrice")
            });
        }

        return output;
    }

    private static List<IndustryGrowthEntry> ParseGrowthCompanies(JsonElement data, string propertyName)
    {
        var output = new List<IndustryGrowthEntry>();
        if (!data.TryGetProperty(propertyName, out var companies) || companies.ValueKind != JsonValueKind.Array)
            return output;

        foreach (var entry in companies.EnumerateArray())
        {
            output.Add(new IndustryGrowthEntry
            {
                Symbol = entry.TryGetProperty("symbol", out var symbol) ? symbol.GetString() ?? string.Empty : string.Empty,
                Name = entry.TryGetProperty("name", out var name) ? name.GetString() : null,
                YtdReturn = TryGetDecimalRaw(entry, "ytdReturn"),
                GrowthEstimate = TryGetDecimalRaw(entry, "growthEstimate")
            });
        }

        return output;
    }

    private static Dictionary<string, string> ParseSymbolDictionary(JsonElement data, string propertyName)
    {
        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (!data.TryGetProperty(propertyName, out var items) || items.ValueKind != JsonValueKind.Array)
            return result;

        foreach (var entry in items.EnumerateArray())
        {
            var symbol = entry.TryGetProperty("symbol", out var symbolElement) ? symbolElement.GetString() : null;
            var name = entry.TryGetProperty("name", out var nameElement) ? nameElement.GetString() : null;
            if (!string.IsNullOrWhiteSpace(symbol) && !string.IsNullOrWhiteSpace(name))
                result[symbol] = name;
        }

        return result;
    }

    private static List<DomainResearchReport> ParseResearchReports(JsonElement data)
    {
        var reports = new List<DomainResearchReport>();
        if (!data.TryGetProperty("researchReports", out var items) || items.ValueKind != JsonValueKind.Array)
            return reports;

        foreach (var entry in items.EnumerateArray())
        {
            var report = new DomainResearchReport
            {
                Title = entry.TryGetProperty("title", out var title) ? title.GetString() : null,
                Url = entry.TryGetProperty("link", out var link) ? link.GetString() : null
            };

            if (entry.TryGetProperty("pubDate", out var pubDate) && pubDate.ValueKind == JsonValueKind.String &&
                DateTime.TryParse(pubDate.GetString(), out var parsed))
            {
                report.PublishedDate = parsed;
            }

            if (entry.TryGetProperty("publisher", out var publisher) && publisher.ValueKind == JsonValueKind.String)
                report.Publisher = publisher.GetString();

            reports.Add(report);
        }

        return reports;
    }

    private static Dictionary<string, MarketSummaryEntry> ParseMarketSummary(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        var output = new Dictionary<string, MarketSummaryEntry>(StringComparer.OrdinalIgnoreCase);

        if (!root.TryGetProperty("marketSummaryResponse", out var response) ||
            !response.TryGetProperty("result", out var results) ||
            results.ValueKind != JsonValueKind.Array)
        {
            return output;
        }

        foreach (var entry in results.EnumerateArray())
        {
            var exchange = entry.TryGetProperty("exchange", out var exchangeElement) ? exchangeElement.GetString() : null;
            if (string.IsNullOrWhiteSpace(exchange))
                continue;

            output[exchange] = new MarketSummaryEntry
            {
                Exchange = exchange,
                ShortName = entry.TryGetProperty("shortName", out var shortName) ? shortName.GetString() : null,
                RegularMarketPrice = TryGetDecimal(entry, "regularMarketPrice"),
                RegularMarketChange = TryGetDecimal(entry, "regularMarketChange"),
                RegularMarketChangePercent = TryGetDecimal(entry, "regularMarketChangePercent")
            };
        }

        return output;
    }

    private static MarketStatus? ParseMarketStatus(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("finance", out var finance) ||
            !finance.TryGetProperty("marketTimes", out var marketTimes) ||
            marketTimes.ValueKind != JsonValueKind.Array ||
            marketTimes.GetArrayLength() == 0)
        {
            return null;
        }

        var marketTime = marketTimes[0];
        if (!marketTime.TryGetProperty("marketTime", out var marketTimeArray) ||
            marketTimeArray.ValueKind != JsonValueKind.Array ||
            marketTimeArray.GetArrayLength() == 0)
        {
            return null;
        }

        var details = marketTimeArray[0];
        var status = new MarketStatus
        {
            MarketState = details.TryGetProperty("marketState", out var state) ? state.GetString() : null
        };

        if (details.TryGetProperty("open", out var open) && open.ValueKind == JsonValueKind.String &&
            DateTime.TryParse(open.GetString(), out var openDate))
        {
            status.Open = openDate;
        }

        if (details.TryGetProperty("close", out var close) && close.ValueKind == JsonValueKind.String &&
            DateTime.TryParse(close.GetString(), out var closeDate))
        {
            status.Close = closeDate;
        }

        if (details.TryGetProperty("timezone", out var timezone))
        {
            var tzObject = timezone.ValueKind == JsonValueKind.Array && timezone.GetArrayLength() > 0
                ? timezone[0]
                : timezone;

            if (tzObject.ValueKind == JsonValueKind.Object)
            {
                status.Timezone = new MarketTimezone
                {
                    ShortName = tzObject.TryGetProperty("short", out var shortName) ? shortName.GetString() : null,
                    GmtOffset = tzObject.TryGetProperty("gmtoffset", out var offset) && offset.ValueKind == JsonValueKind.Number
                        ? offset.GetInt32()
                        : null
                };
            }
        }

        return status;
    }

    private static decimal? TryGetDecimal(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property) || property.ValueKind != JsonValueKind.Number)
            return null;

        return ReadDecimal(property);
    }

    private static decimal? TryGetDecimalRaw(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property) || property.ValueKind != JsonValueKind.Object)
            return null;

        if (!property.TryGetProperty("raw", out var raw) || raw.ValueKind != JsonValueKind.Number)
            return null;

        return ReadDecimal(raw);
    }

    private static long? TryGetLongRaw(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property) || property.ValueKind != JsonValueKind.Object)
            return null;

        if (!property.TryGetProperty("raw", out var raw) || raw.ValueKind != JsonValueKind.Number)
            return null;

        return raw.TryGetInt64(out var value) ? value : null;
    }

    private static int? TryGetInt(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property) || property.ValueKind != JsonValueKind.Number)
            return null;

        return property.TryGetInt32(out var value) ? value : null;
    }

    private static decimal? ReadDecimal(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Number)
            return null;

        try
        {
            return element.GetDecimal();
        }
        catch (FormatException)
        {
            if (element.TryGetInt64(out var longValue))
                return longValue;
        }
        catch (OverflowException)
        {
            if (element.TryGetInt64(out var longValue))
                return longValue;
        }

        return null;
    }
}
