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
            { "modules", "financialData,quoteType,defaultKeyStatistics,assetProfile,summaryDetail,calendarEvents,secFilings" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);

        var quote = ParseQuoteData(symbol, jsonResponse);

        // Enrich with query1 quote endpoint for additional fields
        var query1Response = await _client.GetAsync(
            "/v7/finance/quote",
            new Dictionary<string, string> { { "symbols", symbol } },
            cancellationToken).ConfigureAwait(false);

        EnrichFromQuery1(quote, query1Response);

        // Enrich with timeseries endpoint (trailing PEG)
        var now = DateTimeOffset.UtcNow;
        var period1 = now.AddYears(-2).ToUnixTimeSeconds().ToString();
        var period2 = now.ToUnixTimeSeconds().ToString();
        var timeseriesResponse = await _client.GetAsync(
            $"/ws/fundamentals-timeseries/v1/finance/timeseries/{symbol}",
            new Dictionary<string, string>
            {
                { "type", "trailingPegRatio" },
                { "period1", period1 },
                { "period2", period2 },
                { "merge", "false" }
            },
            cancellationToken).ConfigureAwait(false);

        EnrichFromTimeseries(quote, timeseriesResponse);
        return quote;
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

            if (summaryDetail.TryGetProperty("exDividendDate", out var exDiv) && exDiv.ValueKind != JsonValueKind.Null &&
                exDiv.TryGetProperty("raw", out var exDivRaw) && exDivRaw.ValueKind == JsonValueKind.Number)
            {
                quoteData.ExDividendDate = DateTimeOffset.FromUnixTimeSeconds(exDivRaw.GetInt64()).UtcDateTime;
            }
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

        if (result.TryGetProperty("calendarEvents", out var calendarEvents) &&
            calendarEvents.TryGetProperty("earnings", out var earnings))
        {
            if (earnings.TryGetProperty("earningsDate", out var earningsDateArray) &&
                earningsDateArray.ValueKind == JsonValueKind.Array &&
                earningsDateArray.GetArrayLength() > 0)
            {
                var first = earningsDateArray[0];
                if (first.TryGetProperty("raw", out var earningsRaw) && earningsRaw.ValueKind == JsonValueKind.Number)
                {
                    quoteData.NextEarningsDate = DateTimeOffset.FromUnixTimeSeconds(earningsRaw.GetInt64()).UtcDateTime;
                }
            }

            quoteData.EarningsAverage = ExtractDecimalFromProperty(earnings, "earningsAverage");
            quoteData.EarningsLow = ExtractDecimalFromProperty(earnings, "earningsLow");
            quoteData.EarningsHigh = ExtractDecimalFromProperty(earnings, "earningsHigh");
            quoteData.RevenueAverage = ExtractDecimalFromProperty(earnings, "revenueAverage");
            quoteData.RevenueLow = ExtractDecimalFromProperty(earnings, "revenueLow");
            quoteData.RevenueHigh = ExtractDecimalFromProperty(earnings, "revenueHigh");
        }

        if (result.TryGetProperty("secFilings", out var secFilings) &&
            secFilings.TryGetProperty("filings", out var filings) &&
            filings.ValueKind == JsonValueKind.Array)
        {
            quoteData.SecFilings = new List<SecFiling>();

            foreach (var filing in filings.EnumerateArray())
            {
                var entry = new SecFiling
                {
                    Title = filing.TryGetProperty("title", out var title) && title.ValueKind == JsonValueKind.String
                        ? title.GetString() ?? string.Empty
                        : string.Empty,
                    FormType = filing.TryGetProperty("type", out var type) && type.ValueKind == JsonValueKind.String
                        ? type.GetString() ?? string.Empty
                        : string.Empty,
                    EdgarUrl = filing.TryGetProperty("edgarUrl", out var url) && url.ValueKind == JsonValueKind.String
                        ? url.GetString()
                        : null,
                    FilingDate = ParseFilingDate(filing)
                };

                quoteData.SecFilings.Add(entry);
            }
        }

        return quoteData;
    }

    private void EnrichFromQuery1(QuoteData quote, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteResponse", out var quoteResponse) ||
            !quoteResponse.TryGetProperty("result", out var results) ||
            results.GetArrayLength() == 0)
        {
            return;
        }

        var result = results[0];

        if (result.TryGetProperty("regularMarketPrice", out var price) && price.ValueKind == JsonValueKind.Number)
            quote.RegularMarketPrice = price.GetDecimal();
        if (result.TryGetProperty("regularMarketOpen", out var open) && open.ValueKind == JsonValueKind.Number)
            quote.RegularMarketOpen = open.GetDecimal();
        if (result.TryGetProperty("regularMarketDayHigh", out var high) && high.ValueKind == JsonValueKind.Number)
            quote.RegularMarketDayHigh = high.GetDecimal();
        if (result.TryGetProperty("regularMarketDayLow", out var low) && low.ValueKind == JsonValueKind.Number)
            quote.RegularMarketDayLow = low.GetDecimal();
        if (result.TryGetProperty("regularMarketPreviousClose", out var prev) && prev.ValueKind == JsonValueKind.Number)
            quote.RegularMarketPreviousClose = prev.GetDecimal();
        if (result.TryGetProperty("regularMarketVolume", out var volume) && volume.ValueKind == JsonValueKind.Number)
            quote.RegularMarketVolume = volume.GetInt64();

        if (result.TryGetProperty("marketCap", out var mcap) && mcap.ValueKind == JsonValueKind.Number)
            quote.MarketCap = mcap.GetDecimal();

        if (result.TryGetProperty("trailingPE", out var pe) && pe.ValueKind == JsonValueKind.Number)
            quote.PeRatio = pe.GetDecimal();
        if (result.TryGetProperty("forwardPE", out var fpe) && fpe.ValueKind == JsonValueKind.Number)
            quote.ForwardPE = fpe.GetDecimal();
        if (result.TryGetProperty("pegRatio", out var peg) && peg.ValueKind == JsonValueKind.Number)
            quote.PegRatio = peg.GetDecimal();

        if (result.TryGetProperty("priceToBook", out var pb) && pb.ValueKind == JsonValueKind.Number)
            quote.PriceToBook = pb.GetDecimal();
        if (result.TryGetProperty("trailingEps", out var eps) && eps.ValueKind == JsonValueKind.Number)
            quote.EarningsPerShare = eps.GetDecimal();
        if (result.TryGetProperty("beta", out var beta) && beta.ValueKind == JsonValueKind.Number)
            quote.Beta = beta.GetDecimal();

        if (result.TryGetProperty("fiftyTwoWeekHigh", out var high52) && high52.ValueKind == JsonValueKind.Number)
            quote.FiftyTwoWeekHigh = high52.GetDecimal();
        if (result.TryGetProperty("fiftyTwoWeekLow", out var low52) && low52.ValueKind == JsonValueKind.Number)
            quote.FiftyTwoWeekLow = low52.GetDecimal();

        if (result.TryGetProperty("averageDailyVolume10Day", out var vol10) && vol10.ValueKind == JsonValueKind.Number)
            quote.AverageDailyVolume10Day = vol10.GetInt64();
        if (result.TryGetProperty("averageDailyVolume3Month", out var vol3m) && vol3m.ValueKind == JsonValueKind.Number)
            quote.AverageDailyVolume3Month = vol3m.GetInt64();

        if (result.TryGetProperty("currency", out var currency) && currency.ValueKind == JsonValueKind.String)
            quote.Currency = currency.GetString() ?? quote.Currency;
        if (result.TryGetProperty("exchange", out var exchange) && exchange.ValueKind == JsonValueKind.String)
            quote.Exchange = exchange.GetString() ?? quote.Exchange;
        if (result.TryGetProperty("shortName", out var shortName) && shortName.ValueKind == JsonValueKind.String)
            quote.ShortName = shortName.GetString() ?? quote.ShortName;
        if (result.TryGetProperty("longName", out var longName) && longName.ValueKind == JsonValueKind.String)
            quote.LongName = longName.GetString() ?? quote.LongName;
        if (result.TryGetProperty("quoteType", out var quoteType) && quoteType.ValueKind == JsonValueKind.String)
            quote.QuoteType = quoteType.GetString() ?? quote.QuoteType;
        if (result.TryGetProperty("marketTimezone", out var tz) && tz.ValueKind == JsonValueKind.String)
            quote.TimeZone = tz.GetString() ?? quote.TimeZone;
    }

    private void EnrichFromTimeseries(QuoteData quote, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("timeseries", out var timeseries) ||
            !timeseries.TryGetProperty("result", out var results) ||
            results.ValueKind != JsonValueKind.Array ||
            results.GetArrayLength() == 0)
        {
            return;
        }

        var result = results[0];
        if (!result.TryGetProperty("trailingPegRatio", out var pegSeries) ||
            pegSeries.ValueKind != JsonValueKind.Array ||
            pegSeries.GetArrayLength() == 0)
        {
            return;
        }

        var last = pegSeries[pegSeries.GetArrayLength() - 1];
        if (last.TryGetProperty("reportedValue", out var reportedValue))
            quote.TrailingPegRatio = _dataParser.ExtractDecimal(reportedValue);
    }

    private decimal? ExtractDecimalFromProperty(JsonElement element, string propertyName)
    {
        return element.TryGetProperty(propertyName, out var value) ? _dataParser.ExtractDecimal(value) : null;
    }

    private static DateTime? ParseFilingDate(JsonElement filing)
    {
        if (filing.TryGetProperty("epochDate", out var epochDate) && epochDate.ValueKind == JsonValueKind.Number)
            return DateTimeOffset.FromUnixTimeSeconds(epochDate.GetInt64()).UtcDateTime;

        if (filing.TryGetProperty("date", out var date) && date.ValueKind == JsonValueKind.String &&
            DateTime.TryParse(date.GetString(), out var parsed))
            return DateTime.SpecifyKind(parsed, DateTimeKind.Utc);

        return null;
    }
}
