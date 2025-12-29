using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using YFinance.NET.Implementation.DependencyInjection;
using YFinance.NET.Interfaces;
using YFinance.NET.Models.Enums;
using YFinance.NET.Models.Requests;

namespace YFinance.NET.Tests.Integration;

/// <summary>
/// Integration tests for ITickerService against live Yahoo Finance API.
/// These tests require internet connectivity and may be subject to rate limiting.
/// </summary>
public class TickerServiceIntegrationTests : IDisposable
{
    private readonly IHost _host;
    private readonly ITickerService _tickerService;

    public TickerServiceIntegrationTests()
    {
        // Setup host with dependency injection
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddYFinance();
            })
            .Build();

        // Resolve ITickerService from DI container
        _tickerService = _host.Services.GetRequiredService<ITickerService>();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetHistoryAsync_AppleLast7Days_ReturnsValidData()
    {
        // Arrange
        const string symbol = "AAPL";
        var endDate = DateTime.UtcNow.Date;
        var startDate = endDate.AddDays(-7);

        var request = new HistoryRequest
        {
            Start = startDate,
            End = endDate,
            Interval = Interval.OneDay,
            AutoAdjust = true,
            Repair = false
        };

        // Act
        var result = await _tickerService.GetHistoryAsync(symbol, request);

        // Print results to console
        Console.WriteLine($"\n{new string('=', 80)}");
        Console.WriteLine($"Historical Data for {result.Symbol}");
        Console.WriteLine($"{new string('=', 80)}");
        Console.WriteLine($"TimeZone: {result.TimeZone}");
        Console.WriteLine($"Data Points: {result.Timestamps.Length}");
        Console.WriteLine($"Date Range: {(result.Timestamps.Length > 0 ? $"{result.Timestamps[0]:yyyy-MM-dd} to {result.Timestamps[^1]:yyyy-MM-dd}" : "N/A")}");
        Console.WriteLine($"\n{new string('=', 80)}");
        Console.WriteLine($"{"Date",-12} {"Open",10} {"High",10} {"Low",10} {"Close",10} {"Adj Close",12} {"Volume",15}");
        Console.WriteLine($"{new string('-', 80)}");

        for (int i = 0; i < result.Timestamps.Length; i++)
        {
            Console.WriteLine($"{result.Timestamps[i]:yyyy-MM-dd}  " +
                            $"{result.Open[i],10:F2} " +
                            $"{result.High[i],10:F2} " +
                            $"{result.Low[i],10:F2} " +
                            $"{result.Close[i],10:F2} " +
                            $"{result.AdjustedClose[i],12:F2} " +
                            $"{result.Volume[i],15:N0}");
        }
        Console.WriteLine($"{new string('=', 80)}\n");

        // Assert
        result.Should().NotBeNull();
        result.Symbol.Should().Be(symbol);

        // Verify we got data (may be less than 7 days due to weekends/holidays)
        result.Timestamps.Should().NotBeEmpty();
        result.Timestamps.Length.Should().BeGreaterThan(0).And.BeLessThanOrEqualTo(7);

        // All arrays should have same length
        result.Open.Length.Should().Be(result.Timestamps.Length);
        result.High.Length.Should().Be(result.Timestamps.Length);
        result.Low.Length.Should().Be(result.Timestamps.Length);
        result.Close.Length.Should().Be(result.Timestamps.Length);
        result.AdjustedClose.Length.Should().Be(result.Timestamps.Length);
        result.Volume.Length.Should().Be(result.Timestamps.Length);

        // Validate price data makes sense
        for (int i = 0; i < result.Timestamps.Length; i++)
        {
            result.Open[i].Should().BeGreaterThan(0, $"Open price at index {i} should be positive");
            result.High[i].Should().BeGreaterThan(0, $"High price at index {i} should be positive");
            result.Low[i].Should().BeGreaterThan(0, $"Low price at index {i} should be positive");
            result.Close[i].Should().BeGreaterThan(0, $"Close price at index {i} should be positive");
            result.AdjustedClose[i].Should().BeGreaterThan(0, $"Adjusted close at index {i} should be positive");
            result.Volume[i].Should().BeGreaterThan(0, $"Volume at index {i} should be positive");

            // High should be >= Low
            result.High[i].Should().BeGreaterThanOrEqualTo(result.Low[i],
                $"High price should be >= Low price at index {i}");

            // Open, High, Low, Close should be within reasonable range
            result.Open[i].Should().BeInRange(result.Low[i], result.High[i],
                $"Open price should be between Low and High at index {i}");
            result.Close[i].Should().BeInRange(result.Low[i], result.High[i],
                $"Close price should be between Low and High at index {i}");
        }

        // Verify dates are within requested range
        result.Timestamps.Should().OnlyContain(timestamp =>
            timestamp >= startDate && timestamp <= endDate);

        // Verify timezone is set
        result.TimeZone.Should().NotBeNullOrEmpty();
    }

    [Fact]
    [Trait("Category", "Integration")]
    public async Task GetHistoryAsync_AppleOneWeekPeriod_ReturnsValidData()
    {
        // Arrange
        const string symbol = "AAPL";
        var request = new HistoryRequest
        {
            Period = Period.OneMonth, // Using OneMonth as there's no SevenDays period
            Interval = Interval.OneDay,
            AutoAdjust = true
        };

        // Act
        var result = await _tickerService.GetHistoryAsync(symbol, request);

        // Assert
        result.Should().NotBeNull();
        result.Symbol.Should().Be(symbol);
        result.Timestamps.Should().NotBeEmpty();

        // Should have roughly 20-23 trading days in a month (excluding weekends)
        result.Timestamps.Length.Should().BeGreaterThan(15);

        // Verify all required data is present
        result.Open.Should().NotBeEmpty();
        result.High.Should().NotBeEmpty();
        result.Low.Should().NotBeEmpty();
        result.Close.Should().NotBeEmpty();
        result.AdjustedClose.Should().NotBeEmpty();
        result.Volume.Should().NotBeEmpty();
    }

    public void Dispose()
    {
        _host?.Dispose();
    }
}
