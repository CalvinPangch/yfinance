using System.Text.Json;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Interfaces.Utils;
using YFinance.Models;

namespace YFinance.Implementation.Scrapers;

/// <summary>
/// Scraper for retrieving quote data from Yahoo Finance.
/// </summary>
public class QuoteScraper : IQuoteScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;

    /// <summary>
    /// Initializes a new instance of the <see cref="QuoteScraper"/> class.
    /// </summary>
    /// <param name="client">The Yahoo Finance HTTP client.</param>
    /// <param name="dataParser">The data parser for JSON processing.</param>
    /// <exception cref="ArgumentNullException">Thrown when any parameter is null.</exception>
    public QuoteScraper(IYahooFinanceClient client, IDataParser dataParser)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
    }

    /// <inheritdoc />
    public async Task<QuoteData> GetQuoteAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or whitespace.", nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "financialData,quoteType,defaultKeyStatistics,assetProfile,summaryDetail" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);

        return ParseQuoteData(symbol, jsonResponse);
    }

    private QuoteData ParseQuoteData(string symbol, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        // Navigate to quoteSummary.result[0]
        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.GetArrayLength() == 0)
        {
            return new QuoteData { Symbol = symbol };
        }

        var result = results[0];

        var quoteData = new QuoteData
        {
            Symbol = symbol
        };

        // Extract from quoteType module
        if (result.TryGetProperty("quoteType", out var quoteType))
        {
            quoteData.ShortName = quoteType.TryGetProperty("shortName", out var shortName)
                ? shortName.GetString() ?? string.Empty
                : string.Empty;
            quoteData.LongName = quoteType.TryGetProperty("longName", out var longName)
                ? longName.GetString() ?? string.Empty
                : string.Empty;
            quoteData.Exchange = quoteType.TryGetProperty("exchange", out var exchange)
                ? exchange.GetString() ?? string.Empty
                : string.Empty;
            quoteData.QuoteType = quoteType.TryGetProperty("quoteType", out var qt)
                ? qt.GetString() ?? string.Empty
                : string.Empty;
            quoteData.TimeZone = quoteType.TryGetProperty("timeZoneFullName", out var tz)
                ? tz.GetString()
                : null;
        }

        // Extract from summaryDetail module
        if (result.TryGetProperty("summaryDetail", out var summaryDetail))
        {
            if (summaryDetail.TryGetProperty("open", out var open) && open.ValueKind != JsonValueKind.Null)
                quoteData.RegularMarketOpen = _dataParser.ExtractDecimal(open);
            if (summaryDetail.TryGetProperty("dayHigh", out var dayHigh) && dayHigh.ValueKind != JsonValueKind.Null)
                quoteData.RegularMarketDayHigh = _dataParser.ExtractDecimal(dayHigh);
            if (summaryDetail.TryGetProperty("dayLow", out var dayLow) && dayLow.ValueKind != JsonValueKind.Null)
                quoteData.RegularMarketDayLow = _dataParser.ExtractDecimal(dayLow);
            if (summaryDetail.TryGetProperty("previousClose", out var prevClose) && prevClose.ValueKind != JsonValueKind.Null)
                quoteData.RegularMarketPreviousClose = _dataParser.ExtractDecimal(prevClose);
            if (summaryDetail.TryGetProperty("volume", out var volume) && volume.ValueKind != JsonValueKind.Null &&
                volume.TryGetProperty("raw", out var volRaw) && volRaw.ValueKind == JsonValueKind.Number)
                quoteData.RegularMarketVolume = volRaw.GetInt64();
            if (summaryDetail.TryGetProperty("dividendYield", out var divYield) && divYield.ValueKind != JsonValueKind.Null)
                quoteData.DividendYield = _dataParser.ExtractDecimal(divYield);
            if (summaryDetail.TryGetProperty("beta", out var beta) && beta.ValueKind != JsonValueKind.Null)
                quoteData.Beta = _dataParser.ExtractDecimal(beta);
            if (summaryDetail.TryGetProperty("fiftyTwoWeekHigh", out var high52) && high52.ValueKind != JsonValueKind.Null)
                quoteData.FiftyTwoWeekHigh = _dataParser.ExtractDecimal(high52);
            if (summaryDetail.TryGetProperty("fiftyTwoWeekLow", out var low52) && low52.ValueKind != JsonValueKind.Null)
                quoteData.FiftyTwoWeekLow = _dataParser.ExtractDecimal(low52);
            if (summaryDetail.TryGetProperty("averageDailyVolume10Day", out var vol10) && vol10.ValueKind != JsonValueKind.Null &&
                vol10.TryGetProperty("raw", out var vol10Raw) && vol10Raw.ValueKind == JsonValueKind.Number)
                quoteData.AverageDailyVolume10Day = vol10Raw.GetInt64();

            quoteData.Currency = summaryDetail.TryGetProperty("currency", out var currency) && currency.ValueKind == JsonValueKind.String
                ? currency.GetString() ?? string.Empty
                : string.Empty;
        }

        // Extract from financialData module
        if (result.TryGetProperty("financialData", out var financialData))
        {
            if (financialData.TryGetProperty("currentPrice", out var currentPrice) && currentPrice.ValueKind != JsonValueKind.Null)
                quoteData.RegularMarketPrice = _dataParser.ExtractDecimal(currentPrice);
        }

        // Extract from defaultKeyStatistics module
        if (result.TryGetProperty("defaultKeyStatistics", out var keyStats))
        {
            if (keyStats.TryGetProperty("trailingPE", out var pe) && pe.ValueKind != JsonValueKind.Null)
                quoteData.PeRatio = _dataParser.ExtractDecimal(pe);
            if (keyStats.TryGetProperty("forwardPE", out var fpe) && fpe.ValueKind != JsonValueKind.Null)
                quoteData.ForwardPE = _dataParser.ExtractDecimal(fpe);
            if (keyStats.TryGetProperty("pegRatio", out var peg) && peg.ValueKind != JsonValueKind.Null)
                quoteData.PegRatio = _dataParser.ExtractDecimal(peg);
            if (keyStats.TryGetProperty("priceToBook", out var pb) && pb.ValueKind != JsonValueKind.Null)
                quoteData.PriceToBook = _dataParser.ExtractDecimal(pb);
            if (keyStats.TryGetProperty("trailingEps", out var eps) && eps.ValueKind != JsonValueKind.Null)
                quoteData.EarningsPerShare = _dataParser.ExtractDecimal(eps);
            if (keyStats.TryGetProperty("marketCap", out var mcap) && mcap.ValueKind != JsonValueKind.Null &&
                mcap.TryGetProperty("raw", out var mcapRaw) && mcapRaw.ValueKind == JsonValueKind.Number)
                quoteData.MarketCap = mcapRaw.GetDecimal();

            // Extract earnings timestamp if available
            if (keyStats.TryGetProperty("nextEarningsDate", out var earningsDate) && earningsDate.ValueKind != JsonValueKind.Null &&
                earningsDate.TryGetProperty("raw", out var earningsRaw) && earningsRaw.ValueKind == JsonValueKind.Number)
            {
                quoteData.EarningsTimestamp = DateTimeOffset.FromUnixTimeSeconds(earningsRaw.GetInt64()).UtcDateTime;
            }
        }

        return quoteData;
    }
}
