namespace YFinance.NET.Interfaces.Utils;

/// <summary>
/// Validates ticker symbols to prevent URL injection and ensure proper formatting.
/// </summary>
public interface ISymbolValidator
{
    /// <summary>
    /// Validates that a symbol is safe to use in URLs and API requests.
    /// </summary>
    /// <param name="symbol">The ticker symbol to validate.</param>
    /// <returns>True if the symbol is valid; otherwise, false.</returns>
    bool IsValid(string symbol);

    /// <summary>
    /// Validates a symbol and throws an exception if invalid.
    /// </summary>
    /// <param name="symbol">The ticker symbol to validate.</param>
    /// <param name="parameterName">The name of the parameter (for exception messages).</param>
    /// <exception cref="ArgumentException">Thrown when the symbol is invalid.</exception>
    void ValidateAndThrow(string symbol, string parameterName = "symbol");

    /// <summary>
    /// Sanitizes a symbol by removing or escaping invalid characters.
    /// </summary>
    /// <param name="symbol">The ticker symbol to sanitize.</param>
    /// <returns>A sanitized version of the symbol safe for use in URLs.</returns>
    string Sanitize(string symbol);
}
