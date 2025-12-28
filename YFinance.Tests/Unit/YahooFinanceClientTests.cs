using FluentAssertions;
using Moq;
using Xunit;
using YFinance.Implementation;
using YFinance.Interfaces.Services;

namespace YFinance.Tests.Unit;

/// <summary>
/// Unit tests for YahooFinanceClient focusing on testable logic.
/// HTTP retry logic and error handling are tested in integration tests.
/// </summary>
public class YahooFinanceClientTests
{
    private readonly Mock<ICookieService> _mockCookieService;
    private readonly Mock<IRateLimitService> _mockRateLimitService;
    private readonly Mock<ICacheService> _mockCacheService;

    public YahooFinanceClientTests()
    {
        _mockCookieService = new Mock<ICookieService>();
        _mockRateLimitService = new Mock<IRateLimitService>();
        _mockCacheService = new Mock<ICacheService>();

        _mockCookieService.Setup(s => s.GetCrumb())
            .Returns("test-crumb");
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_NullCookieService_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new YahooFinanceClient(null!, _mockRateLimitService.Object, _mockCacheService.Object));
    }

    [Fact]
    public void Constructor_NullRateLimitService_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() =>
            new YahooFinanceClient(_mockCookieService.Object, null!, _mockCacheService.Object));
    }

    #endregion

    #region Input Validation Tests

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task GetAsync_InvalidEndpoint_ThrowsArgumentException(string? endpoint)
    {
        // Arrange
        var client = new YahooFinanceClient(
            _mockCookieService.Object,
            _mockRateLimitService.Object,
            _mockCacheService.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => client.GetAsync(endpoint!));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public async Task PostAsync_InvalidEndpoint_ThrowsArgumentException(string? endpoint)
    {
        // Arrange
        var client = new YahooFinanceClient(
            _mockCookieService.Object,
            _mockRateLimitService.Object,
            _mockCacheService.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => client.PostAsync(endpoint!, "{}"));
    }

    [Fact]
    public async Task PostAsync_NullJsonBody_ThrowsArgumentNullException()
    {
        // Arrange
        var client = new YahooFinanceClient(
            _mockCookieService.Object,
            _mockRateLimitService.Object,
            _mockCacheService.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => client.PostAsync("/test", null!));
    }

    #endregion

    #region EnsureAuthenticationAsync Tests

    [Fact]
    public async Task EnsureAuthenticationAsync_DelegatesToCookieService()
    {
        // Arrange
        var cookieContainer = new System.Net.CookieContainer();
        _mockCookieService.Setup(s => s.GetCookieContainerAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(cookieContainer);

        var client = new YahooFinanceClient(
            _mockCookieService.Object,
            _mockRateLimitService.Object,
            _mockCacheService.Object);

        // Act
        await client.EnsureAuthenticationAsync();

        // Assert
        _mockCookieService.Verify(s => s.GetCookieContainerAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion
}
