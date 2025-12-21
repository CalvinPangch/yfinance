namespace YFinance.Models.Exceptions;

/// <summary>
/// Exception thrown when an invalid or delisted ticker symbol is provided.
/// </summary>
public class InvalidTickerException : YahooFinanceException
{
    public string Symbol { get; }

    public InvalidTickerException(string symbol)
        : base($"Invalid or delisted ticker symbol: {symbol}")
    {
        Symbol = symbol;
    }

    public InvalidTickerException(string symbol, string message)
        : base(message)
    {
        Symbol = symbol;
    }
}
