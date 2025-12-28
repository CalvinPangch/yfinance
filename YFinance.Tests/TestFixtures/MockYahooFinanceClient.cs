using Moq;
using YFinance.Interfaces;

namespace YFinance.Tests.TestFixtures;

/// <summary>
/// Helper class for creating mocked IYahooFinanceClient instances for testing.
/// </summary>
public static class MockYahooFinanceClient
{
    /// <summary>
    /// Creates a basic mock IYahooFinanceClient with default setup.
    /// </summary>
    /// <returns>A mock IYahooFinanceClient.</returns>
    public static Mock<IYahooFinanceClient> Create()
    {
        var mock = new Mock<IYahooFinanceClient>();

        // Setup EnsureAuthenticationAsync to complete successfully by default
        mock.Setup(c => c.EnsureAuthenticationAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        return mock;
    }

    /// <summary>
    /// Creates a mock client that returns the specified response for GetAsync calls.
    /// </summary>
    /// <param name="response">The JSON response to return.</param>
    /// <returns>A mock IYahooFinanceClient configured to return the response.</returns>
    public static Mock<IYahooFinanceClient> WithValidResponse(string response)
    {
        var mock = Create();

        mock.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        return mock;
    }

    /// <summary>
    /// Creates a mock client that returns the specified response for a specific endpoint.
    /// </summary>
    /// <param name="endpoint">The endpoint to match.</param>
    /// <param name="response">The JSON response to return.</param>
    /// <returns>A mock IYahooFinanceClient configured for the specific endpoint.</returns>
    public static Mock<IYahooFinanceClient> WithResponseForEndpoint(string endpoint, string response)
    {
        var mock = Create();

        mock.Setup(c => c.GetAsync(
                endpoint,
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        return mock;
    }

    /// <summary>
    /// Creates a mock client that throws the specified exception.
    /// </summary>
    /// <param name="exception">The exception to throw.</param>
    /// <returns>A mock IYahooFinanceClient configured to throw the exception.</returns>
    public static Mock<IYahooFinanceClient> WithException(Exception exception)
    {
        var mock = Create();

        mock.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);

        return mock;
    }

    /// <summary>
    /// Creates a mock client for testing POST requests.
    /// </summary>
    /// <param name="response">The JSON response to return.</param>
    /// <returns>A mock IYahooFinanceClient configured for POST requests.</returns>
    public static Mock<IYahooFinanceClient> WithPostResponse(string response)
    {
        var mock = Create();

        mock.Setup(c => c.PostAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        return mock;
    }

    /// <summary>
    /// Creates a mock client that can be configured with custom behavior.
    /// </summary>
    /// <param name="setupAction">Action to configure the mock.</param>
    /// <returns>A configured mock IYahooFinanceClient.</returns>
    public static Mock<IYahooFinanceClient> WithCustomSetup(Action<Mock<IYahooFinanceClient>> setupAction)
    {
        var mock = Create();
        setupAction(mock);
        return mock;
    }
}
