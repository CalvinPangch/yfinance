using System.Net;

namespace YFinance.Tests.TestFixtures;

/// <summary>
/// Mock HTTP message handler for testing HTTP requests without making actual network calls.
/// </summary>
public class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;

    /// <summary>
    /// Initializes a new instance of the <see cref="MockHttpMessageHandler"/> class.
    /// </summary>
    /// <param name="handlerFunc">Function that handles HTTP requests and returns responses.</param>
    public MockHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
    {
        _handlerFunc = handlerFunc ?? throw new ArgumentNullException(nameof(handlerFunc));
    }

    /// <summary>
    /// Creates a mock handler that always returns a successful response with the specified content.
    /// </summary>
    /// <param name="content">The response content.</param>
    /// <param name="statusCode">The HTTP status code (default: OK).</param>
    /// <returns>A mock handler that returns the specified response.</returns>
    public static MockHttpMessageHandler CreateWithResponse(string content, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new MockHttpMessageHandler((request, cancellationToken) =>
        {
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(content)
            };
            return Task.FromResult(response);
        });
    }

    /// <summary>
    /// Creates a mock handler that throws an exception.
    /// </summary>
    /// <param name="exception">The exception to throw.</param>
    /// <returns>A mock handler that throws the specified exception.</returns>
    public static MockHttpMessageHandler CreateWithException(Exception exception)
    {
        return new MockHttpMessageHandler((request, cancellationToken) =>
        {
            throw exception;
        });
    }

    /// <inheritdoc />
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return _handlerFunc(request, cancellationToken);
    }
}
