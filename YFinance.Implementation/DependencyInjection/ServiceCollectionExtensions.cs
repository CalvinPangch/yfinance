using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Interfaces.Services;
using YFinance.Interfaces.Utils;
using YFinance.Implementation;
using YFinance.Implementation.Scrapers;
using YFinance.Implementation.Services;
using YFinance.Implementation.Utils;

namespace YFinance.Implementation.DependencyInjection;

/// <summary>
/// Extension methods for configuring YFinance services in an IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds YFinance services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection so that additional calls can be chained.</returns>
    public static IServiceCollection AddYFinance(this IServiceCollection services)
    {
        // Register HttpClient factory
        services.AddHttpClient();

        // Register cookie service as Singleton (manages shared authentication state)
        services.AddSingleton<ICookieService, CookieService>();

        // Register cache service as Singleton
        services.AddSingleton<ICacheService, CacheService>();

        // Register rate limit service as Singleton (stateless)
        services.AddSingleton<IRateLimitService, RateLimitService>();

        // Register YahooFinanceClient as Transient (stateless, uses shared cookie service)
        services.AddTransient<IYahooFinanceClient, YahooFinanceClient>();

        // Register scrapers as Singleton (stateless)
        services.AddSingleton<IHistoryScraper, HistoryScraper>();
        services.AddSingleton<IQuoteScraper, QuoteScraper>();
        services.AddSingleton<IAnalysisScraper, AnalysisScraper>();
        services.AddSingleton<IHoldersScraper, HoldersScraper>();
        services.AddSingleton<IFundamentalsScraper, FundamentalsScraper>();
        services.AddSingleton<IFundsScraper, FundsScraper>();
        services.AddSingleton<ISearchScraper, SearchScraper>();
        services.AddSingleton<ILookupScraper, LookupScraper>();
        services.AddSingleton<IScreenerScraper, ScreenerScraper>();
        services.AddSingleton<INewsScraper, NewsScraper>();
        services.AddSingleton<IEarningsScraper, EarningsScraper>();
        services.AddSingleton<IOptionsScraper, OptionsScraper>();
        services.AddSingleton<IEsgScraper, EsgScraper>();

        // Register utilities as Transient (lightweight)
        services.AddTransient<IDataParser, DataParser>();
        services.AddTransient<ITimezoneHelper, TimezoneHelper>();
        services.AddTransient<IPriceRepair, PriceRepair>();

        // Register main service with constructor injection
        services.AddSingleton<ITickerService, TickerService>();
        services.AddSingleton<IMultiTickerService, MultiTickerService>();

        // Add memory cache if not already registered
        services.AddMemoryCache();

        return services;
    }
}
