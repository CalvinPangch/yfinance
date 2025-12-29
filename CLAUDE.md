# CLAUDE.md - AI Assistant Guide for YFinance.NET C# Project

**Version**: 1.0
**Last Updated**: 2025-12-29
**Target Framework**: .NET 10.0

This document provides comprehensive guidance for AI assistants (like Claude) working on the YFinance.NET C# codebase. It covers architecture, conventions, workflows, and best practices to ensure consistent, high-quality contributions.

---

## Table of Contents

1. [Project Overview](#project-overview)
2. [Codebase Architecture](#codebase-architecture)
3. [Project Structure](#project-structure)
4. [Development Conventions](#development-conventions)
5. [Common Tasks & Workflows](#common-tasks--workflows)
6. [Testing Guidelines](#testing-guidelines)
7. [Code Style & Standards](#code-style--standards)
8. [Dependency Injection Patterns](#dependency-injection-patterns)
9. [Error Handling](#error-handling)
10. [CI/CD Pipeline](#cicd-pipeline)
11. [Key Files Reference](#key-files-reference)
12. [Common Pitfalls & Solutions](#common-pitfalls--solutions)

---

## Project Overview

### What is YFinance.NET?

YFinance.NET is a C# port of the popular Python [yfinance library](https://github.com/ranaroussi/yfinance) for accessing Yahoo! Finance market data. It provides a clean, dependency-injection-based API for retrieving:

- Historical price data (OHLC, volume, dividends, splits)
- Real-time quotes
- Financial statements
- Analyst data (recommendations, earnings estimates)
- Holder information
- Options chains
- ESG scores
- Calendar events
- News feeds
- And more...

### Technology Stack

- **.NET Version**: 10.0
- **Language**: C# with nullable reference types enabled
- **HTTP Client**: Microsoft.Extensions.Http (IHttpClientFactory pattern)
- **DI Framework**: Microsoft.Extensions.DependencyInjection
- **Caching**: Microsoft.Extensions.Caching.Memory
- **JSON**: System.Text.Json
- **HTML Parsing**: HtmlAgilityPack 1.12.4
- **Timezone**: NodaTime 3.2.2
- **Testing**: xUnit, Moq, FluentAssertions

### Key Principles

1. **Clean Architecture**: Separation of concerns with Interfaces, Models, and Implementation
2. **Dependency Injection**: Full DI support throughout the stack
3. **Async/Await**: All I/O operations are fully asynchronous
4. **Testability**: Comprehensive unit and integration test coverage
5. **Type Safety**: Nullable reference types enabled for better compile-time safety

---

## Codebase Architecture

### Architecture Pattern

The project follows **Clean Architecture** principles with clear separation:

```
┌─────────────────────────────────────────────────┐
│           YFinance.NET.Models (DTOs/Enums)          │
│  No dependencies - pure data structures         │
└─────────────────────────────────────────────────┘
                      ▲
                      │
┌─────────────────────────────────────────────────┐
│      YFinance.NET.Interfaces (Abstractions)         │
│  Depends on: Models only                        │
└─────────────────────────────────────────────────┘
                      ▲
                      │
┌─────────────────────────────────────────────────┐
│   YFinance.NET.Implementation (Concrete Logic)      │
│  Depends on: Interfaces + Models                │
└─────────────────────────────────────────────────┘
                      ▲
                      │
┌─────────────────────────────────────────────────┐
│       YFinance.NET.Tests (Unit + Integration)       │
│  Depends on: All projects                       │
└─────────────────────────────────────────────────┘
```

### Layered Design

1. **Models Layer** (`YFinance.NET.Models`)
   - Pure data structures (POCOs)
   - Enums (Period, Interval, etc.)
   - Request objects
   - Exception hierarchy
   - Zero external dependencies

2. **Interfaces Layer** (`YFinance.NET.Interfaces`)
   - Service contracts (`ITickerService`, `IMultiTickerService`)
   - HTTP client abstraction (`IYahooFinanceClient`)
   - Scraper interfaces (18 specialized scrapers)
   - Utility abstractions (parser, timezone, cache, etc.)
   - Only depends on Models

3. **Implementation Layer** (`YFinance.NET.Implementation`)
   - Concrete implementations of all interfaces
   - HTTP client with retry/backoff logic
   - JSON parsing and data transformation
   - Cookie/authentication management
   - Response caching
   - Rate limiting
   - Depends on Interfaces + Models

4. **Tests Layer** (`YFinance.NET.Tests`)
   - Unit tests with mocked dependencies
   - Integration tests with live API calls
   - Test fixtures and builders
   - Depends on all projects

### Service Orchestration Flow

```
User/Application
      ↓
ITickerService (Facade/Orchestrator)
      ↓
Specialized Scrapers (IHistoryScraper, IQuoteScraper, etc.)
      ↓
IYahooFinanceClient (HTTP abstraction)
      ↓ ↓ ↓
ICookieService  ICacheService  IRateLimitService
      ↓
HttpClient (Microsoft.Extensions.Http)
      ↓
Yahoo Finance API
```

**Key Points**:
- `ITickerService` acts as a **facade** - users interact only with this service
- Each scraper is **specialized** for a specific data type
- `IYahooFinanceClient` handles all HTTP concerns (retry, auth, caching, rate limiting)
- Cross-cutting concerns (cookies, cache, rate limiting) are **singleton services**

---

## Project Structure

```
/home/user/yfinance/
├── .github/
│   └── workflows/
│       ├── ci.yml                    # Main CI/CD pipeline
│       ├── claude.yml                # Claude Code integration
│       └── claude-code-review.yml    # Automated PR reviews
│
├── YFinance.NET.Models/                  # Data models (0 dependencies)
│   ├── HistoricalData.cs
│   ├── QuoteData.cs
│   ├── FinancialStatement.cs
│   ├── AnalystData.cs
│   ├── HolderData.cs
│   ├── FundsData.cs
│   ├── OptionsData.cs
│   ├── EsgData.cs
│   ├── CalendarData.cs
│   ├── SharesData.cs
│   ├── NewsItem.cs
│   ├── Enums/
│   │   ├── Period.cs                 # OneDay, OneMonth, OneYear, Max, etc.
│   │   ├── Interval.cs               # OneMinute, OneHour, OneDay, etc.
│   │   ├── StatementType.cs
│   │   └── LookupType.cs
│   ├── Requests/
│   │   ├── HistoryRequest.cs
│   │   ├── OptionChainRequest.cs
│   │   ├── NewsRequest.cs
│   │   ├── SharesHistoryRequest.cs
│   │   └── ...
│   └── Exceptions/
│       ├── YahooFinanceException.cs  # Base exception
│       ├── InvalidTickerException.cs
│       ├── RateLimitException.cs
│       └── DataParsingException.cs
│
├── YFinance.NET.Interfaces/              # Service contracts
│   ├── ITickerService.cs             # Main facade (30+ methods)
│   ├── IMultiTickerService.cs        # Batch operations
│   ├── IYahooFinanceClient.cs        # HTTP client abstraction
│   ├── Scrapers/                     # 18 scraper interfaces
│   │   ├── IHistoryScraper.cs
│   │   ├── IQuoteScraper.cs
│   │   ├── IAnalysisScraper.cs
│   │   ├── IHoldersScraper.cs
│   │   ├── IFundamentalsScraper.cs
│   │   ├── IFundsScraper.cs
│   │   ├── INewsScraper.cs
│   │   ├── IEarningsScraper.cs
│   │   ├── IOptionsScraper.cs
│   │   ├── IEsgScraper.cs
│   │   ├── ICalendarScraper.cs
│   │   ├── ISharesScraper.cs
│   │   └── ...
│   ├── Services/
│   │   ├── ICookieService.cs         # Authentication/cookie management
│   │   ├── ICacheService.cs          # Response caching
│   │   └── IRateLimitService.cs      # Rate limit enforcement
│   └── Utils/
│       ├── IDataParser.cs            # JSON parsing utilities
│       ├── ITimezoneHelper.cs        # Market timezone conversions
│       └── IPriceRepair.cs           # Price data repair algorithms
│
├── YFinance.NET.Implementation/          # Concrete implementations
│   ├── TickerService.cs              # Main service (orchestrates scrapers)
│   ├── MultiTickerService.cs         # Batch/parallel operations
│   ├── YahooFinanceClient.cs         # HTTP client with retry/backoff
│   ├── Constants/
│   │   └── YahooFinanceConstants.cs  # URLs, endpoints, headers
│   ├── Scrapers/                     # 18 scraper implementations
│   │   ├── HistoryScraper.cs
│   │   ├── QuoteScraper.cs
│   │   └── ...
│   ├── Services/
│   │   ├── CookieService.cs          # Thread-safe cookie management
│   │   ├── CacheService.cs           # Memory cache with LRU
│   │   └── RateLimitService.cs       # Rate limit enforcement
│   ├── Utils/
│   │   ├── DataParser.cs             # JSON parsing/extraction
│   │   ├── JsonElementExtensions.cs  # JSON helper methods
│   │   ├── TimezoneHelper.cs         # Timezone conversions
│   │   └── PriceRepair.cs            # Fix 100x errors, bad splits
│   └── DependencyInjection/
│       └── ServiceCollectionExtensions.cs  # AddYFinance() method
│
└── YFinance.NET.Tests/                   # Tests
    ├── Unit/                         # Fast, isolated tests
    │   ├── TickerServiceTests.cs
    │   ├── MultiTickerServiceTests.cs
    │   ├── YahooFinanceClientTests.cs
    │   ├── RateLimitServiceTests.cs
    │   ├── DataParserTests.cs
    │   └── Scrapers/                 # 18 scraper test files
    ├── Integration/                  # Live API tests
    │   └── TickerServiceIntegrationTests.cs
    └── Fixtures/
        ├── TestDataBuilder.cs        # Mock API response builder
        ├── MockHttpMessageHandler.cs
        └── MockYahooFinanceClient.cs
```

### Project Files

- **YFinance.NET.sln**: Visual Studio solution file
- **global.json**: .NET SDK version pinning (10.0.101)
- **README.md**: User-facing documentation
- **CLAUDE.md**: This file (AI assistant guide)
- **.gitignore**: Standard .NET gitignore

---

## Development Conventions

### C# Code Style

1. **Nullable Reference Types**: Always enabled
   - Use `?` for nullable types
   - Avoid null returns where possible; throw exceptions or return empty collections
   - Use `ArgumentNullException.ThrowIfNull()` for validation

2. **Async/Await**:
   - All I/O methods must be async
   - Suffix async methods with `Async` (e.g., `GetHistoryAsync`)
   - Always accept `CancellationToken cancellationToken = default` as last parameter
   - Use `ConfigureAwait(false)` in library code (not UI code)

3. **Naming Conventions**:
   - Interfaces: `IServiceName`
   - Implementations: `ServiceName` (matches interface without 'I')
   - Private fields: `_camelCase` with underscore prefix
   - Public properties/methods: `PascalCase`
   - Local variables/parameters: `camelCase`
   - Constants: `PascalCase` or `ALL_CAPS` (for true constants)

4. **Access Modifiers**:
   - Default to most restrictive (private)
   - Public only for intended API surface
   - Internal for implementation details
   - Sealed classes by default (unless designed for inheritance)

5. **Immutability**:
   - Prefer `readonly` fields
   - Use `init` setters for DTOs where appropriate
   - Favor immutable collections (`IReadOnlyList<T>`, `IReadOnlyCollection<T>`)

### Project Organization Rules

1. **One class per file** (with matching filename)
2. **Namespace matches folder structure**
3. **Group related files in folders** (Scrapers/, Services/, Utils/, etc.)
4. **Keep Models pure** - no logic, just data structures
5. **Interfaces define contracts** - no implementation details

### Dependency Rules

**CRITICAL**: Follow these dependency constraints:

```
YFinance.NET.Models → (no dependencies)
YFinance.NET.Interfaces → YFinance.NET.Models
YFinance.NET.Implementation → YFinance.NET.Interfaces + YFinance.NET.Models
YFinance.NET.Tests → All projects
```

**Never**:
- Add dependencies from Models to other projects
- Add dependencies from Interfaces to Implementation
- Reference concrete types in interfaces

---

## Common Tasks & Workflows

### Adding a New API Endpoint

Follow this checklist when adding support for a new Yahoo Finance endpoint:

#### 1. Define the Model (`YFinance.NET.Models`)

```csharp
// YFinance.NET.Models/NewDataType.cs
namespace YFinance.NET.Models;

/// <summary>
/// Represents new data from Yahoo Finance.
/// </summary>
public sealed class NewDataType
{
    public string Symbol { get; init; } = string.Empty;
    public DateTime Timestamp { get; init; }
    public decimal Value { get; init; }
    // Add other properties...
}
```

**Checklist**:
- [ ] Use `sealed` class
- [ ] Use `init` setters for immutability
- [ ] Add XML documentation comments
- [ ] Use appropriate nullable annotations
- [ ] Add to namespace `YFinance.NET.Models`

#### 2. Create Request Object (if needed)

```csharp
// YFinance.NET.Models/Requests/NewDataRequest.cs
namespace YFinance.NET.Models.Requests;

/// <summary>
/// Request parameters for fetching new data.
/// </summary>
public sealed class NewDataRequest
{
    public required string Symbol { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}
```

#### 3. Define Scraper Interface (`YFinance.NET.Interfaces`)

```csharp
// YFinance.NET.Interfaces/Scrapers/INewDataScraper.cs
namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper for fetching new data from Yahoo Finance.
/// </summary>
public interface INewDataScraper
{
    /// <summary>
    /// Fetches new data for the specified symbol.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The new data.</returns>
    Task<NewDataType> FetchAsync(
        NewDataRequest request,
        CancellationToken cancellationToken = default);
}
```

**Checklist**:
- [ ] Prefix with 'I'
- [ ] Use XML documentation
- [ ] Return Task<T> for async
- [ ] Accept CancellationToken as last parameter with default
- [ ] Use request object for complex parameters

#### 4. Implement Scraper (`YFinance.NET.Implementation`)

```csharp
// YFinance.NET.Implementation/Scrapers/NewDataScraper.cs
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Implementation.Scrapers;

/// <summary>
/// Scraper implementation for new data.
/// </summary>
internal sealed class NewDataScraper : INewDataScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _parser;

    public NewDataScraper(
        IYahooFinanceClient client,
        IDataParser parser)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _parser = parser ?? throw new ArgumentNullException(nameof(parser));
    }

    public async Task<NewDataType> FetchAsync(
        NewDataRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var url = BuildUrl(request);
        var jsonElement = await _client.GetJsonAsync(url, cancellationToken)
            .ConfigureAwait(false);

        return ParseResponse(jsonElement, request.Symbol);
    }

    private static string BuildUrl(NewDataRequest request)
    {
        // Build Yahoo Finance API URL
        return $"https://query2.finance.yahoo.com/v10/finance/quoteSummary/{request.Symbol}?modules=newData";
    }

    private NewDataType ParseResponse(JsonElement root, string symbol)
    {
        // Parse JSON response
        var result = root.GetProperty("quoteSummary")
                         .GetProperty("result")[0]
                         .GetProperty("newData");

        return new NewDataType
        {
            Symbol = symbol,
            Timestamp = DateTime.UtcNow,
            Value = result.GetProperty("value").GetDecimal()
        };
    }
}
```

**Checklist**:
- [ ] Use `internal sealed` class
- [ ] Constructor injection for dependencies
- [ ] Null-check parameters (`ArgumentNullException.ThrowIfNull`)
- [ ] Use `ConfigureAwait(false)` on all awaits
- [ ] Separate URL building and parsing into private methods
- [ ] Add error handling (try/catch with custom exceptions)

#### 5. Add Method to ITickerService (`YFinance.NET.Interfaces`)

```csharp
// YFinance.NET.Interfaces/ITickerService.cs (add method)
/// <summary>
/// Gets new data for the specified symbol.
/// </summary>
Task<NewDataType> GetNewDataAsync(
    NewDataRequest request,
    CancellationToken cancellationToken = default);
```

#### 6. Implement in TickerService (`YFinance.NET.Implementation`)

```csharp
// YFinance.NET.Implementation/TickerService.cs (add method)
public async Task<NewDataType> GetNewDataAsync(
    NewDataRequest request,
    CancellationToken cancellationToken = default)
{
    ArgumentNullException.ThrowIfNull(request);
    return await _newDataScraper.FetchAsync(request, cancellationToken)
        .ConfigureAwait(false);
}
```

**Add to constructor**:
```csharp
private readonly INewDataScraper _newDataScraper;

public TickerService(
    // ... existing parameters ...
    INewDataScraper newDataScraper)
{
    // ... existing assignments ...
    _newDataScraper = newDataScraper ?? throw new ArgumentNullException(nameof(newDataScraper));
}
```

#### 7. Register in DI Container (`YFinance.NET.Implementation`)

```csharp
// YFinance.NET.Implementation/DependencyInjection/ServiceCollectionExtensions.cs
public static IServiceCollection AddYFinance(this IServiceCollection services)
{
    // ... existing registrations ...

    // Add new scraper
    services.AddSingleton<INewDataScraper, NewDataScraper>();

    // ... rest of registrations ...
}
```

#### 8. Write Unit Tests (`YFinance.NET.Tests/Unit`)

```csharp
// YFinance.NET.Tests/Unit/Scrapers/NewDataScraperTests.cs
using Xunit;
using Moq;
using FluentAssertions;
using YFinance.NET.Implementation.Scrapers;
using YFinance.NET.Interfaces;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Tests.Unit.Scrapers;

public class NewDataScraperTests
{
    [Fact]
    public async Task FetchAsync_ValidRequest_ReturnsData()
    {
        // Arrange
        var mockClient = new Mock<IYahooFinanceClient>();
        var mockParser = new Mock<IDataParser>();
        var scraper = new NewDataScraper(mockClient.Object, mockParser.Object);

        var request = new NewDataRequest { Symbol = "AAPL" };

        // Mock JSON response
        var mockJson = TestDataBuilder.CreateNewDataJson();
        mockClient.Setup(c => c.GetJsonAsync(It.IsAny<string>(), default))
                  .ReturnsAsync(mockJson);

        // Act
        var result = await scraper.FetchAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Symbol.Should().Be("AAPL");
    }

    [Fact]
    public async Task FetchAsync_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        var mockClient = new Mock<IYahooFinanceClient>();
        var mockParser = new Mock<IDataParser>();
        var scraper = new NewDataScraper(mockClient.Object, mockParser.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(
            async () => await scraper.FetchAsync(null!));
    }
}
```

#### 9. Write Integration Tests (`YFinance.NET.Tests/Integration`)

```csharp
// YFinance.NET.Tests/Integration/TickerServiceIntegrationTests.cs (add test)
[Fact]
public async Task GetNewDataAsync_AAPL_ReturnsValidData()
{
    // Arrange
    var request = new NewDataRequest { Symbol = "AAPL" };

    // Act
    var result = await _tickerService.GetNewDataAsync(request);

    // Assert
    result.Should().NotBeNull();
    result.Symbol.Should().Be("AAPL");
    result.Value.Should().BeGreaterThan(0);
}
```

#### 10. Update Documentation

- [ ] Add example to README.md
- [ ] Update API reference section
- [ ] Add to roadmap if applicable

---

### Modifying Existing Code

1. **Read before modifying**: Always read the entire file before making changes
2. **Maintain consistency**: Follow existing patterns in the file/project
3. **Update tests**: Modify corresponding unit and integration tests
4. **Check dependencies**: Ensure changes don't break dependent code
5. **Run full test suite**: `dotnet test` before committing

### Adding Dependencies

**Before adding a NuGet package**:

1. Check if functionality exists in .NET BCL
2. Evaluate package quality (downloads, maintenance, security)
3. Consider bundle size impact
4. Add to appropriate `.csproj` file only
5. Update this documentation

**Preferred packages**:
- Microsoft.Extensions.* (DI, HTTP, Caching)
- System.Text.Json (NOT Newtonsoft.Json)
- xUnit, Moq, FluentAssertions (testing only)

---

## Testing Guidelines

### Test Organization

```
YFinance.NET.Tests/
├── Unit/                          # Fast, isolated, mocked
│   ├── TickerServiceTests.cs
│   ├── Scrapers/
│   │   ├── HistoryScraperTests.cs
│   │   └── ...
│   └── Services/
│       ├── CookieServiceTests.cs
│       └── ...
└── Integration/                   # Slow, live API, internet required
    └── TickerServiceIntegrationTests.cs
```

### Unit Test Principles

1. **Fast**: < 100ms per test
2. **Isolated**: No external dependencies (DB, API, file system)
3. **Repeatable**: Same result every time
4. **Mocked**: Use Moq for all interfaces
5. **Pattern**: Arrange-Act-Assert

**Example**:
```csharp
[Fact]
public async Task GetHistoryAsync_ValidSymbol_ReturnsHistoricalData()
{
    // Arrange
    var mockScraper = new Mock<IHistoryScraper>();
    mockScraper.Setup(s => s.FetchAsync(It.IsAny<HistoryRequest>(), default))
               .ReturnsAsync(new HistoricalData { Symbol = "AAPL" });

    var service = new TickerService(mockScraper.Object /* ... other deps */);
    var request = new HistoryRequest { Period = Period.OneMonth };

    // Act
    var result = await service.GetHistoryAsync("AAPL", request);

    // Assert
    result.Should().NotBeNull();
    result.Symbol.Should().Be("AAPL");
}
```

### Integration Test Principles

1. **Live API**: Real calls to Yahoo Finance
2. **Internet required**: Tests will fail without connection
3. **Rate limited**: Be mindful of API limits
4. **Data validation**: Assert real-world data quality
5. **Retry logic**: Handle transient failures gracefully

**Example**:
```csharp
[Fact]
public async Task GetHistoryAsync_AAPL_ReturnsValidData()
{
    // Arrange
    var services = new ServiceCollection();
    services.AddYFinance();
    var provider = services.BuildServiceProvider();
    var tickerService = provider.GetRequiredService<ITickerService>();

    // Act
    var history = await tickerService.GetHistoryAsync("AAPL", new HistoryRequest
    {
        Period = Period.OneMonth,
        Interval = Interval.OneDay
    });

    // Assert
    history.Should().NotBeNull();
    history.Symbol.Should().Be("AAPL");
    history.Timestamps.Should().NotBeEmpty();
    history.Close.Should().NotBeEmpty();
    history.Close.Should().OnlyContain(c => c > 0);
}
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run only unit tests (fast)
dotnet test --filter "FullyQualifiedName~Unit"

# Run only integration tests (requires internet)
dotnet test --filter "FullyQualifiedName~Integration"

# Run specific test class
dotnet test --filter "FullyQualifiedName~HistoryScraperTests"

# Run with coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Run in watch mode (auto-rerun on file changes)
dotnet watch test
```

### Test Coverage Goals

- **Minimum**: 70% code coverage
- **Target**: 80%+ code coverage
- **Critical paths**: 100% coverage for error handling, validation, parsing

### Assertions with FluentAssertions

Use FluentAssertions for readable assertions:

```csharp
// Good
result.Should().NotBeNull();
result.Symbol.Should().Be("AAPL");
result.Close.Should().OnlyContain(c => c > 0);
result.Timestamps.Should().HaveCount(30);

// Avoid
Assert.NotNull(result);
Assert.Equal("AAPL", result.Symbol);
```

---

## Code Style & Standards

### XML Documentation

**Required** for all public members:

```csharp
/// <summary>
/// Fetches historical price data for the specified symbol.
/// </summary>
/// <param name="symbol">The ticker symbol (e.g., "AAPL").</param>
/// <param name="request">The request parameters.</param>
/// <param name="cancellationToken">Cancellation token.</param>
/// <returns>Historical OHLC data with timestamps.</returns>
/// <exception cref="ArgumentNullException">Thrown if symbol or request is null.</exception>
/// <exception cref="InvalidTickerException">Thrown if symbol is invalid.</exception>
Task<HistoricalData> GetHistoryAsync(
    string symbol,
    HistoryRequest request,
    CancellationToken cancellationToken = default);
```

### Exception Handling

**Use custom exceptions**:

```csharp
// YFinance.NET.Models/Exceptions/
YahooFinanceException        // Base
├── InvalidTickerException   // Bad symbol
├── RateLimitException       // Too many requests
└── DataParsingException     // JSON parsing failed
```

**Pattern**:
```csharp
try
{
    var json = await _client.GetJsonAsync(url, cancellationToken);
    return ParseResponse(json);
}
catch (HttpRequestException ex)
{
    throw new YahooFinanceException(
        $"Failed to fetch data for {symbol}", ex);
}
catch (JsonException ex)
{
    throw new DataParsingException(
        $"Failed to parse response for {symbol}", ex);
}
```

### Null Handling

**Parameter validation**:
```csharp
public async Task<HistoricalData> GetHistoryAsync(
    string symbol,
    HistoryRequest request,
    CancellationToken cancellationToken = default)
{
    ArgumentNullException.ThrowIfNull(symbol);
    ArgumentNullException.ThrowIfNull(request);

    // ... implementation
}
```

**Return values**:
```csharp
// Return empty collections, not null
public IReadOnlyList<NewsItem> GetNews(string symbol)
{
    return newsItems ?? Array.Empty<NewsItem>();
}
```

### Configuration Constants

**Centralize in Constants file**:

```csharp
// YFinance.NET.Implementation/Constants/YahooFinanceConstants.cs
internal static class YahooFinanceConstants
{
    public const string BaseUrl = "https://query2.finance.yahoo.com";
    public const string ChartEndpoint = "/v8/finance/chart/";
    public const string QuoteSummaryEndpoint = "/v10/finance/quoteSummary/";

    public const int DefaultTimeoutSeconds = 30;
    public const int MaxRetries = 3;
    public const int RetryDelayMilliseconds = 1000;
}
```

### Formatting

**Use `dotnet format`**:
```bash
# Check formatting
dotnet format --verify-no-changes

# Auto-format
dotnet format
```

**Rules**:
- 4 spaces indentation (no tabs)
- Opening braces on new line (Allman style)
- Max line length: ~120 characters (soft limit)
- One statement per line

---

## Dependency Injection Patterns

### Service Lifetimes

Defined in `ServiceCollectionExtensions.cs`:

```csharp
// Singleton: Shared instance, stateless, thread-safe
services.AddSingleton<ICookieService, CookieService>();
services.AddSingleton<ICacheService, CacheService>();
services.AddSingleton<IRateLimitService, RateLimitService>();
services.AddSingleton<ITickerService, TickerService>();
services.AddSingleton<IHistoryScraper, HistoryScraper>();

// Transient: New instance every time, lightweight
services.AddTransient<IYahooFinanceClient, YahooFinanceClient>();
services.AddTransient<IDataParser, DataParser>();
services.AddTransient<ITimezoneHelper, TimezoneHelper>();

// Scoped: NOT USED (no HTTP request context in library)
```

### Registration Pattern

**All services registered in one place**:

```csharp
// YFinance.NET.Implementation/DependencyInjection/ServiceCollectionExtensions.cs
public static IServiceCollection AddYFinance(this IServiceCollection services)
{
    services.AddHttpClient();
    services.AddMemoryCache();

    // Register all services...

    return services;
}
```

**Usage**:
```csharp
// Console app
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddYFinance();
    })
    .Build();

// ASP.NET Core
builder.Services.AddYFinance();
```

### Constructor Injection

**Always use constructor injection**:

```csharp
public class TickerService : ITickerService
{
    private readonly IHistoryScraper _historyScraper;
    private readonly IQuoteScraper _quoteScraper;

    public TickerService(
        IHistoryScraper historyScraper,
        IQuoteScraper quoteScraper)
    {
        _historyScraper = historyScraper ?? throw new ArgumentNullException(nameof(historyScraper));
        _quoteScraper = quoteScraper ?? throw new ArgumentNullException(nameof(quoteScraper));
    }
}
```

**Never**:
- Use service locator pattern
- Use property injection
- Use method injection

---

## Error Handling

### Exception Hierarchy

```
Exception
└── YahooFinanceException (base for all library exceptions)
    ├── InvalidTickerException (symbol not found)
    ├── RateLimitException (too many requests)
    └── DataParsingException (JSON parsing failed)
```

### Retry Logic

**HTTP client includes automatic retry**:

```csharp
// YahooFinanceClient.cs
private const int MaxRetries = 3;
private const int RetryDelayMs = 1000;

for (int attempt = 0; attempt <= MaxRetries; attempt++)
{
    try
    {
        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    catch (HttpRequestException) when (attempt < MaxRetries)
    {
        await Task.Delay(RetryDelayMs * (attempt + 1), cancellationToken);
    }
}
```

### Rate Limiting

**Handled by RateLimitService**:

```csharp
// Check before making request
await _rateLimitService.WaitIfNeededAsync(cancellationToken);

// Make request
var response = await _client.GetAsync(url, cancellationToken);
```

### Validation

**Request validation**:

```csharp
public async Task<HistoricalData> GetHistoryAsync(
    string symbol,
    HistoryRequest request,
    CancellationToken cancellationToken = default)
{
    ArgumentNullException.ThrowIfNull(symbol);
    ArgumentNullException.ThrowIfNull(request);

    if (string.IsNullOrWhiteSpace(symbol))
        throw new ArgumentException("Symbol cannot be empty", nameof(symbol));

    if (request.Start.HasValue && request.End.HasValue && request.Start > request.End)
        throw new ArgumentException("Start date must be before end date");

    // ... implementation
}
```

---

## CI/CD Pipeline

### GitHub Actions Workflows

Located in `.github/workflows/`:

#### 1. **ci.yml** - Main Build & Test Pipeline

**Triggers**:
- Push to `main` or `develop`
- Pull requests to `main` or `develop`
- Manual workflow dispatch

**Jobs**:

1. **build-and-test**:
   - Setup .NET 10.0
   - Restore dependencies
   - Build in Release mode
   - Run unit tests (filter: `FullyQualifiedName~Unit`)
   - Run integration tests (filter: `FullyQualifiedName~Integration`)
   - Upload test results (TRX format, 30-day retention)
   - Generate test report

2. **code-quality**:
   - Restore and build with `/warnaserror` (warnings as errors)
   - Check formatting with `dotnet format --verify-no-changes`

3. **coverage**:
   - Run tests with XPlat Code Coverage
   - Upload to Codecov

#### 2. **claude.yml** - Claude Code Integration

**Triggers**:
- Issue comments with `@claude`
- PR comments with `@claude`
- PR reviews
- New issues

**Purpose**: Allows Claude to assist with issues and PRs

#### 3. **claude-code-review.yml** - Automated Code Review

**Triggers**:
- PR opened
- PR synchronized (new commits)

**Purpose**: Claude reviews PRs for code quality, bugs, performance, security

### Pre-commit Checklist

Before committing, ensure:

- [ ] All tests pass: `dotnet test`
- [ ] Code formatted: `dotnet format`
- [ ] No build warnings: `dotnet build /warnaserror`
- [ ] XML documentation on public members
- [ ] Unit tests for new code
- [ ] Integration tests for API changes (if applicable)

### Pull Request Guidelines

1. **Branch naming**: `feature/description` or `fix/description`
2. **Commit messages**: Clear, descriptive, present tense
3. **PR title**: Concise summary of changes
4. **PR description**:
   - What changed
   - Why it changed
   - How to test
5. **Link issues**: Reference related issues with `#123`
6. **Tests**: Include test coverage for changes
7. **CI**: Ensure all checks pass before requesting review

---

## Key Files Reference

### Essential Files to Know

| File | Purpose | When to Modify |
|------|---------|----------------|
| `YFinance.NET.Interfaces/ITickerService.cs` | Main service contract | Adding new API methods |
| `YFinance.NET.Implementation/TickerService.cs` | Main service implementation | Implementing new API methods |
| `YFinance.NET.Implementation/DependencyInjection/ServiceCollectionExtensions.cs` | DI registration | Adding new services/scrapers |
| `YFinance.NET.Implementation/Constants/YahooFinanceConstants.cs` | URLs, endpoints, headers | Adding new endpoints |
| `YFinance.NET.Models/Enums/Period.cs` | Time period enum | Adding new periods |
| `YFinance.NET.Models/Enums/Interval.cs` | Data interval enum | Adding new intervals |
| `YFinance.NET.Models/Exceptions/YahooFinanceException.cs` | Base exception | Adding exception types |
| `.github/workflows/ci.yml` | CI/CD pipeline | Changing build/test process |
| `README.md` | User documentation | Adding features, examples |
| `CLAUDE.md` | This file | Updating conventions, guidelines |

### Configuration Files

| File | Purpose |
|------|---------|
| `global.json` | .NET SDK version (10.0.101) |
| `YFinance.NET.sln` | Visual Studio solution |
| `*.csproj` | Project files with dependencies |
| `.gitignore` | Git ignore rules |

---

## Common Pitfalls & Solutions

### Pitfall 1: Blocking Calls in Async Code

**DON'T**:
```csharp
var result = GetHistoryAsync("AAPL", request).Result; // BLOCKS!
```

**DO**:
```csharp
var result = await GetHistoryAsync("AAPL", request);
```

### Pitfall 2: Not Using ConfigureAwait(false)

**DON'T**:
```csharp
var json = await _client.GetJsonAsync(url, cancellationToken);
```

**DO** (in library code):
```csharp
var json = await _client.GetJsonAsync(url, cancellationToken)
    .ConfigureAwait(false);
```

### Pitfall 3: Returning Null Instead of Empty Collections

**DON'T**:
```csharp
public IReadOnlyList<NewsItem>? GetNews() => null;
```

**DO**:
```csharp
public IReadOnlyList<NewsItem> GetNews() => Array.Empty<NewsItem>();
```

### Pitfall 4: Not Validating Parameters

**DON'T**:
```csharp
public Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request)
{
    // No validation!
}
```

**DO**:
```csharp
public Task<HistoricalData> GetHistoryAsync(string symbol, HistoryRequest request)
{
    ArgumentNullException.ThrowIfNull(symbol);
    ArgumentNullException.ThrowIfNull(request);

    if (string.IsNullOrWhiteSpace(symbol))
        throw new ArgumentException("Symbol cannot be empty", nameof(symbol));
}
```

### Pitfall 5: Tight Coupling to Concrete Types

**DON'T**:
```csharp
public class TickerService
{
    private readonly HistoryScraper _scraper; // Concrete type!
}
```

**DO**:
```csharp
public class TickerService
{
    private readonly IHistoryScraper _scraper; // Interface!
}
```

### Pitfall 6: Not Using CancellationToken

**DON'T**:
```csharp
public async Task<HistoricalData> GetHistoryAsync(string symbol)
{
    // No cancellation support!
}
```

**DO**:
```csharp
public async Task<HistoricalData> GetHistoryAsync(
    string symbol,
    CancellationToken cancellationToken = default)
{
    var response = await _client.GetAsync(url, cancellationToken);
}
```

### Pitfall 7: Catching and Swallowing Exceptions

**DON'T**:
```csharp
try
{
    return await FetchDataAsync();
}
catch (Exception)
{
    return null; // Lost error context!
}
```

**DO**:
```csharp
try
{
    return await FetchDataAsync();
}
catch (HttpRequestException ex)
{
    throw new YahooFinanceException("Failed to fetch data", ex);
}
```

### Pitfall 8: Mutable DTOs

**DON'T**:
```csharp
public class HistoricalData
{
    public string Symbol { get; set; } // Mutable!
}
```

**DO**:
```csharp
public class HistoricalData
{
    public string Symbol { get; init; } // Immutable!
}
```

### Pitfall 9: Not Following Async Naming Convention

**DON'T**:
```csharp
public Task<HistoricalData> GetHistory() // Missing "Async" suffix
```

**DO**:
```csharp
public Task<HistoricalData> GetHistoryAsync()
```

### Pitfall 10: Dependency Cycle

**DON'T**:
```
YFinance.NET.Models → YFinance.NET.Implementation (CYCLE!)
```

**DO**:
```
YFinance.NET.Models → (nothing)
YFinance.NET.Interfaces → YFinance.NET.Models
YFinance.NET.Implementation → YFinance.NET.Interfaces + YFinance.NET.Models
```

---

## Quick Reference

### Building & Running

```bash
# Restore dependencies
dotnet restore

# Build solution
dotnet build

# Build in Release mode
dotnet build --configuration Release

# Run all tests
dotnet test

# Run unit tests only
dotnet test --filter "FullyQualifiedName~Unit"

# Run with coverage
dotnet test /p:CollectCoverage=true

# Format code
dotnet format

# Check formatting
dotnet format --verify-no-changes
```

### Common Commands

```bash
# Add project reference
dotnet add YFinance.NET.Implementation reference YFinance.NET.Interfaces

# Add NuGet package
dotnet add package Microsoft.Extensions.Http

# List project dependencies
dotnet list package

# Clean build artifacts
dotnet clean

# Watch tests (auto-rerun)
dotnet watch test --filter "FullyQualifiedName~Unit"
```

### Git Workflow

```bash
# Create feature branch
git checkout -b feature/new-api-endpoint

# Stage changes
git add .

# Commit
git commit -m "Add support for new API endpoint"

# Push to remote
git push -u origin feature/new-api-endpoint

# Create PR via GitHub UI or CLI
gh pr create --title "Add new API endpoint" --body "Description..."
```

---

## Summary for AI Assistants

When working on this codebase:

1. **Read before writing**: Always read existing code to understand patterns
2. **Follow architecture**: Maintain clean separation (Models → Interfaces → Implementation)
3. **Use DI**: Constructor injection for all dependencies
4. **Async all the way**: All I/O is async with CancellationToken support
5. **Validate inputs**: Use `ArgumentNullException.ThrowIfNull` and validation
6. **Test thoroughly**: Unit tests (mocked) + integration tests (live API)
7. **Document**: XML comments on all public members
8. **Handle errors**: Use custom exceptions, retry logic, proper error context
9. **Stay immutable**: Use `init` setters, readonly fields, immutable collections
10. **Run tests**: `dotnet test` before committing

### Quick Checklist for New Features

- [ ] Model defined in `YFinance.NET.Models`
- [ ] Request object (if needed) in `YFinance.NET.Models/Requests`
- [ ] Interface defined in `YFinance.NET.Interfaces`
- [ ] Implementation in `YFinance.NET.Implementation`
- [ ] Registered in `ServiceCollectionExtensions.cs`
- [ ] Method added to `ITickerService` and `TickerService`
- [ ] Unit tests in `YFinance.NET.Tests/Unit`
- [ ] Integration tests in `YFinance.NET.Tests/Integration`
- [ ] XML documentation on all public members
- [ ] All tests pass (`dotnet test`)
- [ ] Code formatted (`dotnet format`)
- [ ] Example added to `README.md`

---

**Questions or clarifications?** Check the README.md for user-facing documentation, or examine existing code for implementation patterns.

**Last Updated**: 2025-12-29 by Claude Code
