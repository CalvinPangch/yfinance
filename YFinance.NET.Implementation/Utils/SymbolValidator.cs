using System.Text.RegularExpressions;
using YFinance.NET.Interfaces.Utils;

namespace YFinance.NET.Implementation.Utils;

/// <summary>
/// Validates ticker symbols to prevent URL injection and ensure proper formatting.
/// </summary>
internal sealed class SymbolValidator : ISymbolValidator
{
    // Valid Yahoo Finance symbols contain: alphanumeric, period, hyphen, caret, equals, underscore
    // Examples: AAPL, BRK.B, ^GSPC, BTC-USD, ES=F, SYMBOL_NAME
    private static readonly Regex ValidSymbolRegex = new(
        @"^[A-Za-z0-9\.\-\^=_]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant,
        TimeSpan.FromMilliseconds(100)); // Timeout to prevent ReDoS

    private const int MaxSymbolLength = 50; // Reasonable upper limit for ticker symbols
    private const int MinSymbolLength = 1;

    /// <summary>
    /// Characters that could indicate path traversal or URL manipulation attempts.
    /// </summary>
    private static readonly char[] DangerousCharacters =
    {
        '/', '\\', '%', '?', '#', '&', ':', ';', '@', '<', '>',
        '"', '\'', '(', ')', '[', ']', '{', '}', '|', '~', '`',
        ' ', '\t', '\r', '\n'
    };

    /// <inheritdoc />
    public bool IsValid(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            return false;

        // Check length constraints
        if (symbol.Length < MinSymbolLength || symbol.Length > MaxSymbolLength)
            return false;

        // Check for dangerous characters that could indicate injection attempts
        if (symbol.IndexOfAny(DangerousCharacters) >= 0)
            return false;

        // Check for path traversal sequences
        if (ContainsPathTraversal(symbol))
            return false;

        // Check for URL-encoded characters (potential injection)
        if (ContainsUrlEncoding(symbol))
            return false;

        // Validate against allowed character pattern
        try
        {
            if (!ValidSymbolRegex.IsMatch(symbol))
                return false;
        }
        catch (RegexMatchTimeoutException)
        {
            // If regex times out, reject the symbol as potentially malicious
            return false;
        }

        // Additional check: Symbol shouldn't start with dangerous characters
        char firstChar = symbol[0];
        if (firstChar == '.' || firstChar == '-')
            return false;

        return true;
    }

    /// <inheritdoc />
    public void ValidateAndThrow(string symbol, string parameterName = "symbol")
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or whitespace.", parameterName);

        if (symbol.Length < MinSymbolLength || symbol.Length > MaxSymbolLength)
            throw new ArgumentException(
                $"Symbol length must be between {MinSymbolLength} and {MaxSymbolLength} characters.",
                parameterName);

        // Check for path traversal BEFORE checking for dangerous characters
        // to provide more specific error message
        if (ContainsPathTraversal(symbol))
            throw new ArgumentException(
                "Symbol contains path traversal sequences which are not allowed.",
                parameterName);

        // Check for URL encoding BEFORE checking for dangerous characters
        // to provide more specific error message
        if (ContainsUrlEncoding(symbol))
            throw new ArgumentException(
                "Symbol contains URL-encoded characters which are not allowed.",
                parameterName);

        if (symbol.IndexOfAny(DangerousCharacters) >= 0)
            throw new ArgumentException(
                "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed.",
                parameterName);

        try
        {
            if (!ValidSymbolRegex.IsMatch(symbol))
                throw new ArgumentException(
                    "Symbol contains invalid characters. Only alphanumeric characters and .-^=_ are allowed.",
                    parameterName);
        }
        catch (RegexMatchTimeoutException)
        {
            throw new ArgumentException(
                "Symbol validation failed due to timeout. The symbol may be malformed.",
                parameterName);
        }

        char firstChar = symbol[0];
        if (firstChar == '.' || firstChar == '-')
            throw new ArgumentException(
                "Symbol cannot start with '.' or '-' characters.",
                parameterName);
    }

    /// <inheritdoc />
    public string Sanitize(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            return string.Empty;

        // Trim whitespace
        symbol = symbol.Trim();

        // Remove any dangerous characters
        var sanitized = new System.Text.StringBuilder(symbol.Length);
        bool hasAlphanumeric = false;
        foreach (char c in symbol)
        {
            // Only keep allowed characters
            if (char.IsLetterOrDigit(c) || c == '.' || c == '-' || c == '^' || c == '=' || c == '_')
            {
                sanitized.Append(c);
                if (char.IsLetterOrDigit(c))
                    hasAlphanumeric = true;
            }
        }

        // If there are no alphanumeric characters, return empty
        // (symbols must contain at least one letter or digit)
        if (!hasAlphanumeric)
            return string.Empty;

        var result = sanitized.ToString();

        // Truncate to max length if needed
        if (result.Length > MaxSymbolLength)
            result = result.Substring(0, MaxSymbolLength);

        // Remove leading periods or hyphens
        result = result.TrimStart('.', '-');

        return result;
    }

    /// <summary>
    /// Checks if the symbol contains path traversal sequences.
    /// </summary>
    private static bool ContainsPathTraversal(string symbol)
    {
        // Check for common path traversal patterns
        return symbol.Contains("..") ||
               symbol.Contains("./") ||
               symbol.Contains(".\\") ||
               symbol.Contains("~/");
    }

    /// <summary>
    /// Checks if the symbol contains URL-encoded characters.
    /// </summary>
    private static bool ContainsUrlEncoding(string symbol)
    {
        // Check for percent-encoded sequences (e.g., %2F, %2E, %5C)
        // Simple check: if it contains % followed by hex digits
        for (int i = 0; i < symbol.Length - 2; i++)
        {
            if (symbol[i] == '%')
            {
                char next1 = symbol[i + 1];
                char next2 = symbol[i + 2];
                if (IsHexDigit(next1) && IsHexDigit(next2))
                    return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if a character is a hexadecimal digit.
    /// </summary>
    private static bool IsHexDigit(char c)
    {
        return (c >= '0' && c <= '9') ||
               (c >= 'a' && c <= 'f') ||
               (c >= 'A' && c <= 'F');
    }
}
