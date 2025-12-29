# YFinance - C# Port

[![CI](https://github.com/CalvinPangch/yfinance/actions/workflows/ci.yml/badge.svg)](https://github.com/CalvinPangch/yfinance/actions/workflows/ci.yml)
[![.NET](https://img.shields.io/badge/.NET-10.0-purple)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](LICENSE)

A C# port of the popular [yfinance Python library](https://github.com/ranaroussi/yfinance) for accessing Yahoo! Finance market and financial data. This library provides a clean, dependency-injection-based API for retrieving stock quotes, historical prices, financial statements, and other market data.

> **Note:** This library is intended for research and educational purposes only. It is not affiliated with, endorsed, or vetted by Yahoo, Inc.

## Features

- ✅ **Historical Price Data** - OHLC prices, volume, dividends, splits, capital gains
- ✅ **Real-time Quotes** - Current prices and market data
- ✅ **Info & Fast Info** - Full quote summary plus fast snapshot fields
- ✅ **Financial Statements** - Income statements, balance sheets, cash flow
- ✅ **Analyst Data** - Recommendations, upgrades/downgrades, earnings estimates
- ✅ **Holder Information** - Institutional holdings, insider transactions, fund holders
- ✅ **Options Data** - Option chains and expirations
- ✅ **ESG Scores** - Environmental, social, and governance data
- ✅ **Calendar Events** - Earnings dates, dividends, capital gains
- ✅ **Shares History** - Shares outstanding and float history
- ✅ **News** - Latest ticker news items
- ✅ **Funds Data** - Fund profile and holdings
- ✅ **Search/Lookup/Screener** - Market-wide discovery and filters
- ✅ **Domain Data** - Sector, industry, and market overviews
- ✅ **ISIN Lookup** - Map tickers to ISIN identifiers
- ✅ **Live Market Streaming** - Websocket updates for price data
- ✅ **Dependency Injection** - Full DI support for ASP.NET Core and console apps
- ✅ **Async/Await** - Fully asynchronous API
- ✅ **Timezone Support** - Proper handling of market timezones
- ✅ **Clean Architecture** - Separated interfaces, models, and implementations

## Installation

### Prerequisites

- [.NET SDK 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- Internet connectivity for accessing Yahoo Finance API

### From Source

1. **Clone the repository:**
```bash
git clone https://github.com/CalvinPangch/yfinance.git
cd yfinance
```

2. **Build the solution:**
```bash
dotnet build
```

3. **Run tests (optional):**
```bash
# Run all tests
dotnet test

# Run only unit tests
dotnet test --filter "FullyQualifiedName~YFinance.Tests.Unit"

# Run only integration tests (requires internet)
dotnet test --filter "FullyQualifiedName~YFinance.Tests.Integration"
```

### Add to Your Project

**Option 1: Project Reference** (if YFinance is in your solution)
```bash
dotnet add reference path/to/YFinance.Implementation/YFinance.Implementation.csproj
```

**Option 2: NuGet Package** (once published)
```bash
dotnet add package YFinance
```

## Quick Start

### Console Application

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YFinance.Interfaces;
using YFinance.Implementation.DependencyInjection;
using YFinance.Models.Enums;
using YFinance.Models.Requests;

// Setup dependency injection
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddYFinance();
    })
    .Build();

// Get the ticker service
var tickerService = host.Services.GetRequiredService<ITickerService>();

// Fetch 1 month of Apple stock history
var history = await tickerService.GetHistoryAsync("AAPL", new HistoryRequest
{
    Period = Period.OneMonth,
    Interval = Interval.OneDay,
    AutoAdjust = true
});

// Display results
Console.WriteLine($"Symbol: {history.Symbol}");
Console.WriteLine($"Timezone: {history.TimeZone}");
Console.WriteLine($"Data points: {history.Timestamps.Length}");
Console.WriteLine("\nRecent prices:");

for (int i = Math.Max(0, history.Timestamps.Length - 5); i < history.Timestamps.Length; i++)
{
    Console.WriteLine($"{history.Timestamps[i]:yyyy-MM-dd} - " +
                     $"Open: ${history.Open[i]:F2}, " +
                     $"Close: ${history.Close[i]:F2}, " +
                     $"Volume: {history.Volume[i]:N0}");
}
```

### ASP.NET Core Integration

**Program.cs:**
```csharp
using YFinance.Implementation.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add YFinance services
builder.Services.AddYFinance();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();
```

**StockController.cs:**
```csharp
using Microsoft.AspNetCore.Mvc;
using YFinance.Interfaces;
using YFinance.Models;
using YFinance.Models.Enums;
using YFinance.Models.Requests;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly ITickerService _tickerService;

    public StockController(ITickerService tickerService)
    {
        _tickerService = tickerService;
    }

    [HttpGet("{symbol}/history")]
    public async Task<ActionResult<HistoricalData>> GetHistory(
        string symbol,
        [FromQuery] int days = 30)
    {
        var endDate = DateTime.UtcNow.Date;
        var startDate = endDate.AddDays(-days);

        var history = await _tickerService.GetHistoryAsync(symbol, new HistoryRequest
        {
            Start = startDate,
            End = endDate,
            Interval = Interval.OneDay,
            AutoAdjust = true
        });

        return Ok(history);
    }
}
```

## Usage Examples

### Historical Data with Date Range

```csharp
var request = new HistoryRequest
{
    Start = new DateTime(2024, 1, 1),
    End = new DateTime(2024, 12, 31),
    Interval = Interval.OneDay,
    AutoAdjust = true,
    Repair = false
};

var history = await tickerService.GetHistoryAsync("MSFT", request);

// Access OHLC data
for (int i = 0; i < history.Timestamps.Length; i++)
{
    Console.WriteLine($"Date: {history.Timestamps[i]:yyyy-MM-dd}");
    Console.WriteLine($"  Open: ${history.Open[i]:F2}");
    Console.WriteLine($"  High: ${history.High[i]:F2}");
    Console.WriteLine($"  Low: ${history.Low[i]:F2}");
    Console.WriteLine($"  Close: ${history.Close[i]:F2}");
    Console.WriteLine($"  Adjusted Close: ${history.AdjustedClose[i]:F2}");
    Console.WriteLine($"  Volume: {history.Volume[i]:N0}");

    if (history.Dividends != null && history.Dividends[i] > 0)
        Console.WriteLine($"  Dividend: ${history.Dividends[i]:F2}");

    if (history.StockSplits != null && history.StockSplits[i] > 0)
        Console.WriteLine($"  Stock Split: {history.StockSplits[i]}");
}
```

### Using Period Enums

```csharp
// Pre-defined time periods
var periods = new[]
{
    Period.OneDay,
    Period.FiveDays,
    Period.OneMonth,
    Period.ThreeMonths,
    Period.SixMonths,
    Period.OneYear,
    Period.TwoYears,
    Period.FiveYears,
    Period.TenYears,
    Period.YearToDate,
    Period.Max
};

foreach (var period in periods)
{
    var history = await tickerService.GetHistoryAsync("GOOGL", new HistoryRequest
    {
        Period = period,
        Interval = Interval.OneDay
    });

    Console.WriteLine($"{period}: {history.Timestamps.Length} data points");
}
```

### Different Time Intervals

```csharp
// Intraday data (1-minute intervals)
var intraday = await tickerService.GetHistoryAsync("TSLA", new HistoryRequest
{
    Period = Period.OneDay,
    Interval = Interval.OneMinute
});

// Hourly data
var hourly = await tickerService.GetHistoryAsync("TSLA", new HistoryRequest
{
    Period = Period.FiveDays,
    Interval = Interval.OneHour
});

// Weekly data
var weekly = await tickerService.GetHistoryAsync("TSLA", new HistoryRequest
{
    Period = Period.OneYear,
    Interval = Interval.OneWeek
});

// Monthly data
var monthly = await tickerService.GetHistoryAsync("TSLA", new HistoryRequest
{
    Period = Period.FiveYears,
    Interval = Interval.OneMonth
});
```

### Error Handling

```csharp
using YFinance.Models.Exceptions;

try
{
    var history = await tickerService.GetHistoryAsync("INVALID_SYMBOL", new HistoryRequest
    {
        Period = Period.OneMonth,
        Interval = Interval.OneDay
    });
}
catch (InvalidTickerException ex)
{
    Console.WriteLine($"Invalid ticker: {ex.Message}");
}
catch (RateLimitException ex)
{
    Console.WriteLine($"Rate limited: {ex.Message}");
    // Implement exponential backoff or retry logic
}
catch (YahooFinanceException ex)
{
    Console.WriteLine($"Yahoo Finance error: {ex.Message}");
}
```

### Additional APIs

```csharp
var quote = await tickerService.GetQuoteAsync("AAPL");
var holders = await tickerService.GetHoldersAsync("AAPL");
var options = await tickerService.GetOptionChainAsync(new OptionChainRequest { Symbol = "AAPL" });
var expirations = await tickerService.GetOptionsExpirationsAsync("AAPL");
var esg = await tickerService.GetEsgAsync("AAPL");
var calendar = await tickerService.GetCalendarAsync("AAPL");
var shares = await tickerService.GetSharesHistoryAsync(new SharesHistoryRequest { Symbol = "AAPL" });
var news = await tickerService.GetNewsAsync(new NewsRequest { Symbol = "AAPL", Count = 10 });
```

## Project Structure

```
YFinance/
├── YFinance.Interfaces/          # Service contracts and abstractions
│   ├── IYahooFinanceClient.cs
│   ├── ITickerService.cs
│   └── Scrapers/
│       ├── IHistoryScraper.cs
│       └── ...
├── YFinance.Models/              # Data models and DTOs
│   ├── HistoricalData.cs
│   ├── QuoteData.cs
│   ├── Enums/
│   │   ├── Period.cs
│   │   └── Interval.cs
│   ├── Requests/
│   │   └── HistoryRequest.cs
│   └── Exceptions/
│       └── YahooFinanceException.cs
├── YFinance.Implementation/      # Concrete implementations
│   ├── YahooFinanceClient.cs
│   ├── TickerService.cs
│   ├── Scrapers/
│   │   └── HistoryScraper.cs
│   └── DependencyInjection/
│       └── ServiceCollectionExtensions.cs
└── YFinance.Tests/               # Unit and integration tests
    ├── Unit/
    └── Integration/
```

## Configuration

### Dependency Injection

The library uses Microsoft.Extensions.DependencyInjection with the following service lifetimes:

- **Singleton**: `IYahooFinanceClient`, scrapers, stateless services
- **Transient**: Lightweight utilities (parsers, helpers)
- **Scoped**: Not used (all operations are stateless)

### Service Registration

```csharp
services.AddYFinance(); // Registers all required services
```

This extension method registers:
- HTTP client with proper factory pattern
- All scraper implementations
- Cache and cookie services
- Utility classes

## Testing

### Run All Tests
```bash
dotnet test
```

### Run Specific Test Categories
```bash
# Unit tests only (fast, no external dependencies)
dotnet test --filter "FullyQualifiedName~Unit"

# Integration tests only (requires internet)
dotnet test --filter "FullyQualifiedName~Integration"

# Specific test class
dotnet test --filter "FullyQualifiedName~HistoryScraperTests"
```

### Test Coverage
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## Dependencies

### Core Dependencies
- **Microsoft.Extensions.DependencyInjection** - Dependency injection
- **Microsoft.Extensions.Http** - HTTP client factory
- **Microsoft.Extensions.Caching.Memory** - Response caching
- **System.Text.Json** - JSON serialization

### Planned Dependencies
- **NodaTime** - Timezone handling (for complex DST scenarios)
- **HtmlAgilityPack** - HTML parsing (for consent page handling)

### Test Dependencies
- **xUnit** - Testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Assertion library

## API Reference

### ITickerService

Main entry point for all ticker operations.

```csharp
public interface ITickerService
{
    Task<QuoteData> GetQuoteAsync(string symbol, CancellationToken cancellationToken = default);
    Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request, CancellationToken cancellationToken = default);
    Task<FinancialStatement> GetFinancialStatementsAsync(string symbol, CancellationToken cancellationToken = default);
    Task<AnalystData> GetAnalystDataAsync(string symbol, CancellationToken cancellationToken = default);
    Task<HolderData> GetHoldersAsync(string symbol, CancellationToken cancellationToken = default);
    Task<FundsData> GetFundsDataAsync(string symbol, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<NewsItem>> GetNewsAsync(NewsRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PeriodicEstimate>> GetEarningsEstimateAsync(string symbol, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PeriodicEstimate>> GetRevenueEstimateAsync(string symbol, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EarningsHistoryEntry>> GetEarningsHistoryAsync(string symbol, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PeriodicEstimate>> GetEpsTrendAsync(string symbol, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PeriodicEstimate>> GetEpsRevisionsAsync(string symbol, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<GrowthEstimateEntry>> GetGrowthEstimatesAsync(string symbol, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<EarningsDateEntry>> GetEarningsDatesAsync(EarningsDatesRequest request, CancellationToken cancellationToken = default);
    Task<OptionChain> GetOptionChainAsync(OptionChainRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DateTime>> GetOptionsExpirationsAsync(string symbol, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RecommendationTrendEntry>> GetRecommendationsAsync(string symbol, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<UpgradeDowngradeEntry>> GetUpgradesDowngradesAsync(string symbol, CancellationToken cancellationToken = default);
    Task<EsgData> GetEsgAsync(string symbol, CancellationToken cancellationToken = default);
    Task<CalendarData> GetCalendarAsync(string symbol, CancellationToken cancellationToken = default);
    Task<SharesHistoryData> GetSharesHistoryAsync(SharesHistoryRequest request, CancellationToken cancellationToken = default);
}
```

### HistoryRequest

Request model for historical data.

```csharp
public class HistoryRequest
{
    public DateTime? Start { get; set; }           // Start date (exclusive with Period)
    public DateTime? End { get; set; }             // End date (exclusive with Period)
    public Period? Period { get; set; }            // Pre-defined period (exclusive with Start/End)
    public Interval Interval { get; set; }         // Data interval (default: OneDay)
    public bool AutoAdjust { get; set; }           // Adjust for splits/dividends (default: true)
    public bool Repair { get; set; }               // Repair bad data (default: false)
}
```

### Period Enum

Pre-defined time periods:
- `OneDay`, `FiveDays`
- `OneMonth`, `ThreeMonths`, `SixMonths`
- `OneYear`, `TwoYears`, `FiveYears`, `TenYears`
- `YearToDate`, `Max`

### Interval Enum

Data granularity:
- `OneMinute`, `TwoMinutes`, `FiveMinutes`, `FifteenMinutes`, `ThirtyMinutes`
- `OneHour`, `NinetyMinutes`
- `OneDay`, `FiveDays`, `OneWeek`, `OneMonth`, `ThreeMonths`

## Roadmap

### Implemented ✅
- [x] Historical price data (OHLC, volume, dividends, splits, capital gains)
- [x] Quote data (real-time prices)
- [x] Info + fast info endpoints
- [x] Financial statements (income, balance sheet, cash flow)
- [x] Analyst data (recommendations, upgrades/downgrades, earnings estimates)
- [x] Holder information (institutional, insider, fund holders)
- [x] Options chain data and expirations
- [x] ESG scores
- [x] Calendar events (earnings, dividends, capital gains)
- [x] Shares outstanding and float history
- [x] News endpoints
- [x] Funds data (profile, holdings)
- [x] Search, lookup, and screener endpoints
- [x] Sector, industry, and market domain summaries
- [x] ISIN lookup
- [x] Live market streaming
- [x] Dependency injection setup
- [x] Async/await pattern
- [x] Integration tests
- [x] Basic error handling
- [x] Cookie and authentication management
- [x] Response caching
- [x] Rate limiting

### In Progress 🚧
- [ ] Multiple ticker downloads (batch/parallel)
- [ ] Price repair algorithms
- [ ] Timezone DST edge-case handling

### Planned 📋
- [ ] NuGet package publication

## Contributing

Contributions are welcome! Please ensure:
1. All tests pass (`dotnet test`)
2. Code follows existing patterns (DI, async/await)
3. New features include unit tests
4. Integration tests for API interactions

## Legal Notice

This library is **not affiliated with, endorsed, or vetted by Yahoo, Inc.** It is an open-source tool that uses Yahoo's publicly available APIs and is intended for research and educational purposes only.

**Important:**
- Respect Yahoo's Terms of Service
- Do not use for commercial purposes without proper authorization
- Be mindful of rate limits and server load
- Data provided is for informational purposes only

## License

Apache Software License 2.0 (same as original yfinance Python library)

## Credits

- **Original Library**: [yfinance](https://github.com/ranaroussi/yfinance) by Ran Aroussi
- **C# Port**: This project

## References

- [Original yfinance Documentation](https://ranaroussi.github.io/yfinance)
- [Yahoo Finance](https://finance.yahoo.com)
- [yfinance GitHub Repository](https://github.com/ranaroussi/yfinance)

## Support

- **Issues**: [GitHub Issues](https://github.com/yourusername/yfinance-csharp/issues)
- **Discussions**: [GitHub Discussions](https://github.com/yourusername/yfinance-csharp/discussions)

---

**Disclaimer**: This software is provided "as is", without warranty of any kind. Use at your own risk.
