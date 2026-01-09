using System.Net.Http;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;

namespace YFinance.NET.Implementation;

/// <summary>
/// Resolves ISIN codes via Business Insider lookup.
/// </summary>
public class IsinService : IIsinService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IInfoScraper _infoScraper;

    /// <summary>
    /// Initializes a new instance of the <see cref="IsinService"/> class.
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients.</param>
    /// <param name="infoScraper">Scraper for ticker information.</param>
    public IsinService(IHttpClientFactory httpClientFactory, IInfoScraper infoScraper)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _infoScraper = infoScraper ?? throw new ArgumentNullException(nameof(infoScraper));
    }

    /// <summary>
    /// Gets the ISIN code for the specified symbol using Business Insider lookup.
    /// </summary>
    /// <param name="symbol">The ticker symbol.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The ISIN code, or null if not found.</returns>
    public async Task<string?> GetIsinAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or whitespace.", nameof(symbol));

        var ticker = symbol.Trim().ToUpperInvariant();
        if (ticker.Contains('-') || ticker.Contains('^'))
            return "-";

        var query = ticker;
        var info = await _infoScraper.GetInfoAsync(symbol, cancellationToken).ConfigureAwait(false);
        if (info.Values.TryGetValue("shortName", out var shortName) && shortName is string name && !string.IsNullOrWhiteSpace(name))
            query = name;

        var url = $"https://markets.businessinsider.com/ajax/SearchController_Suggest?max_results=25&query={Uri.EscapeDataString(query)}";
        var client = _httpClientFactory.CreateClient();
        var data = await client.GetStringAsync(url, cancellationToken).ConfigureAwait(false);

        var search = $"\"{ticker}|";
        if (!data.Contains(search, StringComparison.OrdinalIgnoreCase))
        {
            if (data.Contains(query, StringComparison.OrdinalIgnoreCase))
            {
                search = "\"|";
                if (!data.Contains(search, StringComparison.OrdinalIgnoreCase))
                    return "-";
            }
            else
            {
                return "-";
            }
        }

        var segment = data.Split(search)[1];
        var isin = segment.Split('"')[0].Split('|')[0];
        return string.IsNullOrWhiteSpace(isin) ? "-" : isin;
    }
}
