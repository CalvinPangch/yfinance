using System.Text.Json;

namespace YFinance.Interfaces.Utils;

/// <summary>
/// Utility interface for parsing and normalizing Yahoo Finance JSON responses.
/// Handles nested structures, format variants, and data transformations.
/// </summary>
public interface IDataParser
{
    /// <summary>
    /// Parses JSON response to a specified type.
    /// </summary>
    /// <typeparam name="T">Target type</typeparam>
    /// <param name="json">JSON string</param>
    /// <returns>Parsed object</returns>
    T? Parse<T>(string json);

    /// <summary>
    /// Flattens nested Yahoo Finance response structures.
    /// Converts nested dictionaries with "raw" and "fmt" fields to single values.
    /// </summary>
    /// <param name="jsonElement">JSON element to flatten</param>
    /// <returns>Flattened dictionary</returns>
    Dictionary<string, object?> FlattenResponse(JsonElement jsonElement);

    /// <summary>
    /// Extracts numeric value from Yahoo Finance format.
    /// Handles both "raw" values and string formatting.
    /// </summary>
    /// <param name="jsonElement">JSON element containing value</param>
    /// <returns>Numeric value or null</returns>
    decimal? ExtractDecimal(JsonElement jsonElement);

    /// <summary>
    /// Converts Unix timestamp to DateTime.
    /// </summary>
    /// <param name="unixTimestamp">Unix timestamp in seconds</param>
    /// <returns>DateTime in UTC</returns>
    DateTime UnixTimestampToDateTime(long unixTimestamp);
}
