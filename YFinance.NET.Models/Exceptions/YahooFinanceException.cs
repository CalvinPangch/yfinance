namespace YFinance.NET.Models.Exceptions;

/// <summary>
/// Base exception for all Yahoo Finance related errors.
/// </summary>
public class YahooFinanceException : Exception
{
    public YahooFinanceException() : base() { }

    public YahooFinanceException(string message) : base(message) { }

    public YahooFinanceException(string message, Exception innerException)
        : base(message, innerException) { }
}
