namespace YFinance.NET.Models.Exceptions;

/// <summary>
/// Exception thrown when Yahoo Finance API response data cannot be parsed.
/// This typically indicates malformed JSON, missing required fields, or unexpected data formats.
/// </summary>
public class DataParsingException : YahooFinanceException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataParsingException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the parsing error.</param>
    public DataParsingException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataParsingException"/> class with a specified error message
    /// and a reference to the inner exception that caused this exception.
    /// </summary>
    /// <param name="message">The message that describes the parsing error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public DataParsingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
