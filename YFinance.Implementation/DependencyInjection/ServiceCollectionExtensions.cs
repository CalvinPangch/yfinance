using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Implementation.Scrapers;

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
        // Register HttpClient with factory
        services.AddHttpClient<IYahooFinanceClient, YahooFinanceClient>();

        // Register scrapers as Singleton (stateless)
        services.AddSingleton<IHistoryScraper, HistoryScraper>();
        // TODO: Uncomment when other scrapers are implemented
        // services.AddSingleton<IQuoteScraper, QuoteScraper>();
        // services.AddSingleton<IAnalysisScraper, AnalysisScraper>();
        // services.AddSingleton<IHoldersScraper, HoldersScraper>();
        // services.AddSingleton<IFundamentalsScraper, FundamentalsScraper>();

        // Register services as Singleton
        // TODO: Uncomment when services are implemented
        // services.AddSingleton<ICookieService, CookieService>();
        // services.AddSingleton<ICacheService, CacheService>();
        // services.AddSingleton<IRateLimitService, RateLimitService>();

        // Register utilities as Transient (lightweight)
        // TODO: Uncomment when utilities are implemented
        // services.AddTransient<ITimezoneHelper, TimezoneHelper>();
        // services.AddTransient<IDataParser, DataParser>();
        // services.AddTransient<IPriceRepair, PriceRepair>();

        // Register main service with constructor injection
        services.AddSingleton<ITickerService, TickerService>();

        // Add memory cache if not already registered
        services.AddMemoryCache();

        return services;
    }
}
