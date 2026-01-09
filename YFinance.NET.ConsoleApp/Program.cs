using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YFinance.NET.Implementation.DependencyInjection;
using YFinance.NET.Interfaces;
using YFinance.NET.Models.Enums;
using YFinance.NET.Models.Requests;

// Create host with dependency injection
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Register YFinance services
        services.AddYFinance();
    })
    .Build();

// Get the ticker service from DI container
var tickerService = host.Services.GetRequiredService<ITickerService>();

try
{
    Console.WriteLine("==============================================");
    Console.WriteLine("  Apple Stock (AAPL) - Last Week Prices");
    Console.WriteLine("==============================================\n");

    // Create request for last 5 days (closest to 1 week) with daily interval
    var request = new HistoryRequest
    {
        Period = Period.FiveDays,
        Interval = Interval.OneHour
    };

    Console.WriteLine("Fetching data...\n");

    // Fetch historical data for Apple (AAPL)
    var history = await tickerService.GetHistoryAsync("AAPL", request);

    if (history == null || history.Timestamps == null || history.Timestamps.Length == 0)
    {
        Console.WriteLine("No data available for AAPL.");
        return;
    }

    Console.WriteLine($"Symbol: {history.Symbol}");
    Console.WriteLine($"Data points retrieved: {history.Timestamps.Length}\n");
    Console.WriteLine("{0,-12} {1,10} {2,10} {3,10} {4,10} {5,15}",
        "Date", "Open", "High", "Low", "Close", "Volume");
    Console.WriteLine(new string('-', 75));

    // Display each day's data
    for (int i = 0; i < history.Timestamps.Length; i++)
    {
        var date = history.Timestamps[i];
        var open = history.Open?[i] ?? 0;
        var high = history.High?[i] ?? 0;
        var low = history.Low?[i] ?? 0;
        var close = history.Close?[i] ?? 0;
        var volume = history.Volume?[i] ?? 0;

        Console.WriteLine("{0,-12} {1,10:C2} {2,10:C2} {3,10:C2} {4,10:C2} {5,15:N0}",
            date.ToString("yyyy-MM-dd HH:mm:ss"),
            open,
            high,
            low,
            close,
            volume);
    }

    Console.WriteLine(new string('-', 75));

    // Display summary
    if (history.Close != null && history.Close.Length > 0)
    {
        var firstClose = history.Close[0];
        var lastClose = history.Close[^1];
        var change = lastClose - firstClose;
        var changePercent = (change / firstClose) * 100;

        Console.WriteLine($"\nSummary:");
        Console.WriteLine($"  Starting Price: {firstClose:C2}");
        Console.WriteLine($"  Ending Price:   {lastClose:C2}");
        Console.WriteLine($"  Change:         {change:C2} ({changePercent:+0.00;-0.00}%)");
    }

    Console.WriteLine("\n==============================================");
    Console.WriteLine("  Data fetched successfully!");
    Console.WriteLine("==============================================");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine($"\n❌ Error: {ex.Message}");
    Console.WriteLine($"Type: {ex.GetType().Name}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner Error: {ex.InnerException.Message}");
    }
    return;
}
