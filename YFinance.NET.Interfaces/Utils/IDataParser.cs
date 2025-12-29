using System.Text.Json;

namespace YFinance.NET.Interfaces.Utils;

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

    /// <summary>
    /// Parses an array of values from a JSON element with null handling.
    /// </summary>
    /// <typeparam name="T">The target type for array elements.</typeparam>
    /// <param name="element">JSON element containing the array property.</param>
    /// <param name="propertyName">Name of the property containing the array.</param>
    /// <param name="parser">Function to parse individual elements.</param>
    /// <param name="defaultValue">Default value for null elements.</param>
    /// <returns>Parsed array with null values replaced by default.</returns>
    T[] ParseArray<T>(JsonElement element, string propertyName, Func<JsonElement, T> parser, T defaultValue = default!);

    /// <summary>
    /// Parses an array of decimal values from a JSON element.
    /// Null values are replaced with 0.
    /// </summary>
    /// <param name="element">JSON element containing the array property.</param>
    /// <param name="propertyName">Name of the property containing the decimal array.</param>
    /// <returns>Array of decimal values.</returns>
    decimal[] ParseDecimalArray(JsonElement element, string propertyName);

    /// <summary>
    /// Parses an array of long values from a JSON element.
    /// Null values are replaced with 0.
    /// </summary>
    /// <param name="element">JSON element containing the array property.</param>
    /// <param name="propertyName">Name of the property containing the long array.</param>
    /// <returns>Array of long values.</returns>
    long[] ParseLongArray(JsonElement element, string propertyName);
}
