using FluentAssertions;
using Xunit;
using YFinance.NET.Implementation.Services;
using YFinance.NET.Models.Exceptions;

namespace YFinance.NET.Tests.Unit.Services;

public class RateLimitServiceTests
{
    private readonly RateLimitService _service;

    public RateLimitServiceTests()
    {
        _service = new RateLimitService();
    }

    #region IsRateLimited Tests

    [Fact]
    public void IsRateLimited_StatusCode429_ReturnsTrue()
    {
        // Arrange
        var statusCode = 429;
        var responseBody = "Some response";

        // Act
        var result = _service.IsRateLimited(statusCode, responseBody);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsRateLimited_ResponseContainsTooManyRequests_ReturnsTrue()
    {
        // Arrange
        var statusCode = 200;
        var responseBody = "Error: Too Many Requests";

        // Act
        var result = _service.IsRateLimited(statusCode, responseBody);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsRateLimited_ResponseContainsRateLimit_ReturnsTrue()
    {
        // Arrange
        var statusCode = 200;
        var responseBody = "You have exceeded the rate limit";

        // Act
        var result = _service.IsRateLimited(statusCode, responseBody);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsRateLimited_CaseInsensitive_ReturnsTrue()
    {
        // Arrange
        var statusCode = 200;
        var responseBody = "error: too many requests";

        // Act
        var result = _service.IsRateLimited(statusCode, responseBody);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsRateLimited_RateLimitMixedCase_ReturnsTrue()
    {
        // Arrange
        var statusCode = 200;
        var responseBody = "RATE LIMIT exceeded";

        // Act
        var result = _service.IsRateLimited(statusCode, responseBody);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsRateLimited_NormalResponse_ReturnsFalse()
    {
        // Arrange
        var statusCode = 200;
        var responseBody = "{\"chart\":{\"result\":[{\"meta\":{}}]}}";

        // Act
        var result = _service.IsRateLimited(statusCode, responseBody);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsRateLimited_EmptyResponseBody_ReturnsFalse()
    {
        // Arrange
        var statusCode = 200;
        var responseBody = "";

        // Act
        var result = _service.IsRateLimited(statusCode, responseBody);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsRateLimited_NullResponseBody_ReturnsFalse()
    {
        // Arrange
        var statusCode = 200;
        string? responseBody = null;

        // Act
        var result = _service.IsRateLimited(statusCode, responseBody!);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsRateLimited_Status404_ReturnsFalse()
    {
        // Arrange
        var statusCode = 404;
        var responseBody = "Not found";

        // Act
        var result = _service.IsRateLimited(statusCode, responseBody);

        // Assert
        result.Should().BeFalse();
    }

    #endregion

    #region HandleRateLimitAsync Tests

    [Fact]
    public async Task HandleRateLimitAsync_FirstRetry_Delays1Second()
    {
        // Arrange
        var retryCount = 0;
        var startTime = DateTime.UtcNow;

        // Act
        await _service.HandleRateLimitAsync(retryCount);
        var elapsed = DateTime.UtcNow - startTime;

        // Assert - Allow 10ms tolerance for timer resolution
        elapsed.TotalMilliseconds.Should().BeGreaterOrEqualTo(990);
        elapsed.TotalMilliseconds.Should().BeLessThan(1500);
    }

    [Fact]
    public async Task HandleRateLimitAsync_SecondRetry_Delays2Seconds()
    {
        // Arrange
        var retryCount = 1;
        var startTime = DateTime.UtcNow;

        // Act
        await _service.HandleRateLimitAsync(retryCount);
        var elapsed = DateTime.UtcNow - startTime;

        // Assert - Allow 10ms tolerance for timer resolution
        elapsed.TotalMilliseconds.Should().BeGreaterOrEqualTo(1990);
        elapsed.TotalMilliseconds.Should().BeLessThan(2500);
    }

    [Fact]
    public async Task HandleRateLimitAsync_ThirdRetry_Delays4Seconds()
    {
        // Arrange
        var retryCount = 2;
        var startTime = DateTime.UtcNow;

        // Act
        await _service.HandleRateLimitAsync(retryCount);
        var elapsed = DateTime.UtcNow - startTime;

        // Assert - Allow 10ms tolerance for timer resolution
        elapsed.TotalMilliseconds.Should().BeGreaterOrEqualTo(3990);
        elapsed.TotalMilliseconds.Should().BeLessThan(4500);
    }

    [Fact]
    public async Task HandleRateLimitAsync_FourthRetry_Delays8Seconds()
    {
        // Arrange
        var retryCount = 3;
        var startTime = DateTime.UtcNow;

        // Act
        await _service.HandleRateLimitAsync(retryCount);
        var elapsed = DateTime.UtcNow - startTime;

        // Assert - Allow 10ms tolerance for timer resolution
        elapsed.TotalMilliseconds.Should().BeGreaterOrEqualTo(7990);
        elapsed.TotalMilliseconds.Should().BeLessThan(8500);
    }

    [Fact]
    public async Task HandleRateLimitAsync_FifthRetry_ThrowsRateLimitException()
    {
        // Arrange
        var retryCount = 5;

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RateLimitException>(
            () => _service.HandleRateLimitAsync(retryCount));

        exception.RetryAfterSeconds.Should().Be(60);
    }

    [Fact]
    public async Task HandleRateLimitAsync_MaxRetries_ThrowsRateLimitException()
    {
        // Arrange
        var retryCount = 10; // Way over max

        // Act & Assert
        var exception = await Assert.ThrowsAsync<RateLimitException>(
            () => _service.HandleRateLimitAsync(retryCount));

        exception.RetryAfterSeconds.Should().Be(60);
    }

    [Fact]
    public async Task HandleRateLimitAsync_CancellationRequested_ThrowsTaskCanceledException()
    {
        // Arrange
        var retryCount = 0;
        var cts = new CancellationTokenSource();
        cts.Cancel();

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(
            () => _service.HandleRateLimitAsync(retryCount, cts.Token));
    }

    [Fact]
    public async Task HandleRateLimitAsync_CancellationDuringDelay_ThrowsTaskCanceledException()
    {
        // Arrange
        var retryCount = 2; // 4 second delay
        var cts = new CancellationTokenSource();

        // Cancel after 500ms
        cts.CancelAfter(500);

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(
            () => _service.HandleRateLimitAsync(retryCount, cts.Token));
    }

    #endregion
}
