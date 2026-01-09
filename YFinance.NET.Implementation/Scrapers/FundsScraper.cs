using System.Text.Json;
using YFinance.NET.Implementation.Constants;
using YFinance.NET.Implementation.Utils;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for ETF and mutual fund data.
/// </summary>
public class FundsScraper : IFundsScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;

    /// <summary>
    /// Initializes a new instance of the <see cref="FundsScraper"/> class.
    /// </summary>
    /// <param name="client">The Yahoo Finance HTTP client.</param>
    /// <param name="dataParser">The data parser for JSON processing.</param>
    public FundsScraper(IYahooFinanceClient client, IDataParser dataParser)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
    }

    /// <summary>
    /// Gets fund data (ETF or mutual fund) for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Fund data for the symbol.</returns>
    public async Task<FundsData> GetFundsDataAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or whitespace.", nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            ["modules"] = "quoteType,summaryProfile,topHoldings,fundProfile",
            ["corsDomain"] = "finance.yahoo.com",
            ["symbol"] = symbol,
            ["formatted"] = "false"
        };

        var endpoint = string.Format(YahooFinanceConstants.Endpoints.QuoteSummary, symbol);
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseFundsData(symbol, jsonResponse);
    }

    private FundsData ParseFundsData(string symbol, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.ValueKind != JsonValueKind.Array ||
            results.GetArrayLength() == 0)
        {
            return new FundsData { Symbol = symbol };
        }

        var data = results[0];
        var result = new FundsData { Symbol = symbol };

        if (data.TryGetProperty("quoteType", out var quoteType))
            result.QuoteType = quoteType.GetStringOrNull("quoteType");

        if (data.TryGetProperty("summaryProfile", out var summaryProfile))
            result.Description = summaryProfile.GetStringOrNull("longBusinessSummary");

        if (data.TryGetProperty("fundProfile", out var fundProfile))
        {
            result.Overview = new FundOverview
            {
                CategoryName = fundProfile.GetStringOrNull("categoryName"),
                Family = fundProfile.GetStringOrNull("family"),
                LegalType = fundProfile.GetStringOrNull("legalType")
            };

            result.FundOperations = ParseFundOperations(symbol, fundProfile);
        }

        if (data.TryGetProperty("topHoldings", out var topHoldings))
        {
            result.AssetClasses = ParseAssetClasses(topHoldings);
            result.TopHoldings = ParseTopHoldings(topHoldings);
            result.EquityHoldings = ParseEquityHoldings(topHoldings);
            result.BondHoldings = ParseBondHoldings(topHoldings);
            result.BondRatings = ParseKeyValueArray(topHoldings, "bondRatings");
            result.SectorWeightings = ParseKeyValueArray(topHoldings, "sectorWeightings");
        }

        return result;
    }

    private List<FundOperationEntry> ParseFundOperations(string symbol, JsonElement fundProfile)
    {
        var operations = new List<FundOperationEntry>();
        if (!fundProfile.TryGetProperty("feesExpensesInvestment", out var ops))
            return operations;

        fundProfile.TryGetProperty("feesExpensesInvestmentCat", out var opsCat);

        operations.Add(CreateOperation("Annual Report Expense Ratio", ops, opsCat, "annualReportExpenseRatio"));
        operations.Add(CreateOperation("Annual Holdings Turnover", ops, opsCat, "annualHoldingsTurnover"));
        operations.Add(CreateOperation("Total Net Assets", ops, opsCat, "totalNetAssets"));

        return operations.Where(entry => entry.FundValue.HasValue || entry.CategoryAverage.HasValue).ToList();
    }

    private FundOperationEntry CreateOperation(string attribute, JsonElement ops, JsonElement opsCat, string field)
    {
        return new FundOperationEntry
        {
            Attribute = attribute,
            FundValue = GetDecimalProperty(ops, field),
            CategoryAverage = GetDecimalProperty(opsCat, field)
        };
    }

    private Dictionary<string, decimal?> ParseAssetClasses(JsonElement topHoldings)
    {
        var assetClasses = new Dictionary<string, decimal?>();
        assetClasses["cashPosition"] = GetDecimalProperty(topHoldings, "cashPosition");
        assetClasses["stockPosition"] = GetDecimalProperty(topHoldings, "stockPosition");
        assetClasses["bondPosition"] = GetDecimalProperty(topHoldings, "bondPosition");
        assetClasses["preferredPosition"] = GetDecimalProperty(topHoldings, "preferredPosition");
        assetClasses["convertiblePosition"] = GetDecimalProperty(topHoldings, "convertiblePosition");
        assetClasses["otherPosition"] = GetDecimalProperty(topHoldings, "otherPosition");
        return assetClasses;
    }

    private List<TopHolding> ParseTopHoldings(JsonElement topHoldings)
    {
        var holdings = new List<TopHolding>();
        if (!topHoldings.TryGetProperty("holdings", out var holdingsElement) ||
            holdingsElement.ValueKind != JsonValueKind.Array)
        {
            return holdings;
        }

        foreach (var holding in holdingsElement.EnumerateArray())
        {
            holdings.Add(new TopHolding
            {
                Symbol = holding.GetStringOrNull("symbol"),
                Name = holding.GetStringOrNull("holdingName"),
                HoldingPercent = GetDecimalProperty(holding, "holdingPercent")
            });
        }

        return holdings;
    }

    private List<FundMetricEntry> ParseEquityHoldings(JsonElement topHoldings)
    {
        if (!topHoldings.TryGetProperty("equityHoldings", out var equityHoldings))
            return new List<FundMetricEntry>();

        return new List<FundMetricEntry>
        {
            CreateMetric("Price/Earnings", equityHoldings, "priceToEarnings", "priceToEarningsCat"),
            CreateMetric("Price/Book", equityHoldings, "priceToBook", "priceToBookCat"),
            CreateMetric("Price/Sales", equityHoldings, "priceToSales", "priceToSalesCat"),
            CreateMetric("Price/Cashflow", equityHoldings, "priceToCashflow", "priceToCashflowCat"),
            CreateMetric("Median Market Cap", equityHoldings, "medianMarketCap", "medianMarketCapCat"),
            CreateMetric("3 Year Earnings Growth", equityHoldings, "threeYearEarningsGrowth", "threeYearEarningsGrowthCat")
        }.Where(entry => entry.FundValue.HasValue || entry.CategoryAverage.HasValue).ToList();
    }

    private List<FundMetricEntry> ParseBondHoldings(JsonElement topHoldings)
    {
        if (!topHoldings.TryGetProperty("bondHoldings", out var bondHoldings))
            return new List<FundMetricEntry>();

        return new List<FundMetricEntry>
        {
            CreateMetric("Duration", bondHoldings, "duration", "durationCat"),
            CreateMetric("Maturity", bondHoldings, "maturity", "maturityCat"),
            CreateMetric("Credit Quality", bondHoldings, "creditQuality", "creditQualityCat")
        }.Where(entry => entry.FundValue.HasValue || entry.CategoryAverage.HasValue).ToList();
    }

    private FundMetricEntry CreateMetric(string metric, JsonElement element, string field, string categoryField)
    {
        return new FundMetricEntry
        {
            Metric = metric,
            FundValue = GetDecimalProperty(element, field),
            CategoryAverage = GetDecimalProperty(element, categoryField)
        };
    }

    private Dictionary<string, decimal?> ParseKeyValueArray(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var array) ||
            array.ValueKind != JsonValueKind.Array)
        {
            return new Dictionary<string, decimal?>();
        }

        var result = new Dictionary<string, decimal?>();
        foreach (var item in array.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.Object)
                continue;

            foreach (var property in item.EnumerateObject())
            {
                result[property.Name] = ExtractDecimal(property.Value);
            }
        }

        return result;
    }

    private decimal? GetDecimalProperty(JsonElement element, string propertyName)
    {
        return element.TryGetProperty(propertyName, out var value)
            ? ExtractDecimal(value)
            : null;
    }

    private decimal? ExtractDecimal(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Null || element.ValueKind == JsonValueKind.Undefined)
            return null;

        return _dataParser.ExtractDecimal(element);
    }
}
