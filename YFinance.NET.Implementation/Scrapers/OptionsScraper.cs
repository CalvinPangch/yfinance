using System.Text.Json;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper for Yahoo Finance option chain data.
/// </summary>
public class OptionsScraper : IOptionsScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;
    private readonly ISymbolValidator _symbolValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="OptionsScraper"/> class.
    /// </summary>
    /// <param name="client">The Yahoo Finance HTTP client.</param>
    /// <param name="dataParser">The data parser for JSON processing.</param>
    /// <param name="symbolValidator">The symbol validator for security.</param>
    public OptionsScraper(IYahooFinanceClient client, IDataParser dataParser, ISymbolValidator symbolValidator)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
        _symbolValidator = symbolValidator ?? throw new ArgumentNullException(nameof(symbolValidator));
    }

    /// <summary>
    /// Gets the complete option chain for the specified symbol and expiration.
    /// </summary>
    /// <param name="request">The option chain request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Option chain data including calls and puts.</returns>
    public async Task<OptionChain> GetOptionChainAsync(
        OptionChainRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        // Validate symbol for security (prevents URL injection)
        _symbolValidator.ValidateAndThrow(request.Symbol, nameof(request.Symbol));

        var endpoint = $"/v7/finance/options/{request.Symbol}";
        Dictionary<string, string>? queryParams = null;

        if (request.ExpirationDate.HasValue)
        {
            var unix = new DateTimeOffset(EnsureUtc(request.ExpirationDate.Value)).ToUnixTimeSeconds();
            queryParams = new Dictionary<string, string>
            {
                ["date"] = unix.ToString()
            };
        }

        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseOptionChain(request.Symbol, jsonResponse);
    }

    /// <summary>
    /// Gets available option expiration dates for the specified symbol.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of available expiration dates.</returns>
    public async Task<IReadOnlyList<DateTime>> GetExpirationsAsync(
        string symbol,
        CancellationToken cancellationToken = default)
    {
        // Validate symbol for security (prevents URL injection)
        _symbolValidator.ValidateAndThrow(symbol, nameof(symbol));

        var endpoint = $"/v7/finance/options/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, null, cancellationToken).ConfigureAwait(false);
        return ParseExpirations(jsonResponse);
    }

    private OptionChain ParseOptionChain(string symbol, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("optionChain", out var optionChain) ||
            !optionChain.TryGetProperty("result", out var results) ||
            results.ValueKind != JsonValueKind.Array ||
            results.GetArrayLength() == 0)
        {
            return new OptionChain { Symbol = symbol };
        }

        var result = results[0];
        var chain = new OptionChain
        {
            Symbol = result.TryGetProperty("underlyingSymbol", out var underlyingSymbol) && underlyingSymbol.ValueKind == JsonValueKind.String
                ? underlyingSymbol.GetString() ?? symbol
                : symbol,
            RawJson = result.GetRawText()
        };

        if (result.TryGetProperty("expirationDates", out var expirationDates) &&
            expirationDates.ValueKind == JsonValueKind.Array)
        {
            chain.ExpirationDates = new List<DateTime>();
            foreach (var entry in expirationDates.EnumerateArray())
            {
                if (entry.ValueKind == JsonValueKind.Number && entry.TryGetInt64(out var unix))
                    chain.ExpirationDates.Add(_dataParser.UnixTimestampToDateTime(unix));
            }
        }

        if (result.TryGetProperty("quote", out var quote) && quote.ValueKind == JsonValueKind.Object)
            chain.Underlying = ParseUnderlying(quote);

        if (result.TryGetProperty("options", out var options) &&
            options.ValueKind == JsonValueKind.Array &&
            options.GetArrayLength() > 0)
        {
            var optionEntry = options[0];
            chain.ExpirationDate = GetDateValue(optionEntry, "expirationDate");
            chain.Calls = ParseContracts(optionEntry, "calls");
            chain.Puts = ParseContracts(optionEntry, "puts");
        }

        return chain;
    }

    private IReadOnlyList<DateTime> ParseExpirations(string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;
        var output = new List<DateTime>();

        if (!root.TryGetProperty("optionChain", out var optionChain) ||
            !optionChain.TryGetProperty("result", out var results) ||
            results.ValueKind != JsonValueKind.Array ||
            results.GetArrayLength() == 0)
        {
            return output;
        }

        var result = results[0];
        if (!result.TryGetProperty("expirationDates", out var expirationDates) ||
            expirationDates.ValueKind != JsonValueKind.Array)
        {
            return output;
        }

        foreach (var entry in expirationDates.EnumerateArray())
        {
            if (entry.ValueKind == JsonValueKind.Number && entry.TryGetInt64(out var unix))
                output.Add(_dataParser.UnixTimestampToDateTime(unix));
        }

        return output;
    }

    private OptionUnderlying ParseUnderlying(JsonElement element)
    {
        return new OptionUnderlying
        {
            Symbol = GetStringValue(element, "symbol"),
            RegularMarketPrice = GetDecimalValue(element, "regularMarketPrice"),
            RegularMarketChange = GetDecimalValue(element, "regularMarketChange"),
            RegularMarketChangePercent = GetDecimalValue(element, "regularMarketChangePercent"),
            Currency = GetStringValue(element, "currency"),
            Exchange = GetStringValue(element, "exchange"),
            QuoteType = GetStringValue(element, "quoteType"),
            ShortName = GetStringValue(element, "shortName"),
            LongName = GetStringValue(element, "longName"),
            TimeZone = GetStringValue(element, "exchangeTimezoneName"),
            RawJson = element.GetRawText()
        };
    }

    private List<OptionContract> ParseContracts(JsonElement optionEntry, string key)
    {
        var output = new List<OptionContract>();

        if (!optionEntry.TryGetProperty(key, out var contracts) ||
            contracts.ValueKind != JsonValueKind.Array)
        {
            return output;
        }

        foreach (var contract in contracts.EnumerateArray())
        {
            output.Add(new OptionContract
            {
                ContractSymbol = GetStringValue(contract, "contractSymbol"),
                Strike = GetDecimalValue(contract, "strike"),
                LastPrice = GetDecimalValue(contract, "lastPrice"),
                Bid = GetDecimalValue(contract, "bid"),
                Ask = GetDecimalValue(contract, "ask"),
                Change = GetDecimalValue(contract, "change"),
                PercentChange = GetDecimalValue(contract, "percentChange"),
                Volume = GetLongValue(contract, "volume"),
                OpenInterest = GetLongValue(contract, "openInterest"),
                ImpliedVolatility = GetDecimalValue(contract, "impliedVolatility"),
                InTheMoney = GetBoolValue(contract, "inTheMoney"),
                ContractSize = GetStringValue(contract, "contractSize"),
                Currency = GetStringValue(contract, "currency"),
                Expiration = GetDateValue(contract, "expiration"),
                LastTradeDate = GetDateValue(contract, "lastTradeDate"),
                RawJson = contract.GetRawText()
            });
        }

        return output;
    }

    private decimal? GetDecimalValue(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var value) || value.ValueKind == JsonValueKind.Null)
            return null;

        return _dataParser.ExtractDecimal(value);
    }

    private long? GetLongValue(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var value) || value.ValueKind == JsonValueKind.Null)
            return null;

        if (value.ValueKind == JsonValueKind.Number && value.TryGetInt64(out var parsed))
            return parsed;

        if (value.ValueKind == JsonValueKind.String && long.TryParse(value.GetString(), out var parsedString))
            return parsedString;

        return null;
    }

    private bool? GetBoolValue(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var value) || value.ValueKind == JsonValueKind.Null)
            return null;

        if (value.ValueKind == JsonValueKind.True)
            return true;
        if (value.ValueKind == JsonValueKind.False)
            return false;

        if (value.ValueKind == JsonValueKind.String && bool.TryParse(value.GetString(), out var parsed))
            return parsed;

        return null;
    }

    private string? GetStringValue(JsonElement element, string propertyName)
    {
        return element.TryGetProperty(propertyName, out var value) && value.ValueKind == JsonValueKind.String
            ? value.GetString()
            : null;
    }

    private DateTime? GetDateValue(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var value) || value.ValueKind == JsonValueKind.Null)
            return null;

        if (value.ValueKind == JsonValueKind.Number && value.TryGetInt64(out var unix))
            return _dataParser.UnixTimestampToDateTime(unix);

        if (value.ValueKind == JsonValueKind.String && long.TryParse(value.GetString(), out var parsed))
            return _dataParser.UnixTimestampToDateTime(parsed);

        return null;
    }

    private static DateTime EnsureUtc(DateTime value)
    {
        return value.Kind switch
        {
            DateTimeKind.Unspecified => DateTime.SpecifyKind(value, DateTimeKind.Utc),
            DateTimeKind.Local => value.ToUniversalTime(),
            _ => value
        };
    }
}
