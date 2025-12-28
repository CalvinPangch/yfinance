using System.Text.Json;
using YFinance.Interfaces.Utils;

namespace YFinance.Implementation.Utils;

/// <summary>
/// Utility class for parsing and normalizing Yahoo Finance JSON responses.
/// </summary>
public class DataParser : IDataParser
{
    /// <inheritdoc />
    public T? Parse<T>(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return default;

        return JsonSerializer.Deserialize<T>(json);
    }

    /// <inheritdoc />
    public Dictionary<string, object?> FlattenResponse(JsonElement jsonElement)
    {
        var result = new Dictionary<string, object?>();

        if (jsonElement.ValueKind != JsonValueKind.Object)
            return result;

        foreach (var property in jsonElement.EnumerateObject())
        {
            // Handle nested "raw"/"fmt" structures
            if (property.Value.ValueKind == JsonValueKind.Object &&
                property.Value.TryGetProperty("raw", out var rawValue))
            {
                result[property.Name] = ExtractValue(rawValue);
            }
            else
            {
                result[property.Name] = ExtractValue(property.Value);
            }
        }

        return result;
    }

    /// <inheritdoc />
    public decimal? ExtractDecimal(JsonElement jsonElement)
    {
        // Handle "raw"/"fmt" format
        if (jsonElement.ValueKind == JsonValueKind.Object &&
            jsonElement.TryGetProperty("raw", out var rawValue))
        {
            return rawValue.ValueKind == JsonValueKind.Number
                ? rawValue.GetDecimal()
                : null;
        }

        // Direct numeric value
        if (jsonElement.ValueKind == JsonValueKind.Number)
            return jsonElement.GetDecimal();

        // Try parsing string
        if (jsonElement.ValueKind == JsonValueKind.String &&
            decimal.TryParse(jsonElement.GetString(), out var decimalValue))
            return decimalValue;

        return null;
    }

    /// <inheritdoc />
    public DateTime UnixTimestampToDateTime(long unixTimestamp)
    {
        // Yahoo Finance timestamps can be in seconds or milliseconds
        // Values > year 2100 in seconds (4102444800) are likely milliseconds
        if (unixTimestamp > 4102444800)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).UtcDateTime;
        }

        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;
    }

    /// <inheritdoc />
    public T[] ParseArray<T>(JsonElement element, string propertyName, Func<JsonElement, T> parser, T defaultValue = default!)
    {
        if (!element.TryGetProperty(propertyName, out var property))
            return Array.Empty<T>();

        if (property.ValueKind != JsonValueKind.Array)
            return Array.Empty<T>();

        var result = new List<T>();

        foreach (var item in property.EnumerateArray())
        {
            if (item.ValueKind == JsonValueKind.Null)
            {
                result.Add(defaultValue);
            }
            else
            {
                try
                {
                    result.Add(parser(item));
                }
                catch
                {
                    result.Add(defaultValue);
                }
            }
        }

        return result.ToArray();
    }

    /// <inheritdoc />
    public decimal[] ParseDecimalArray(JsonElement element, string propertyName)
    {
        return ParseArray(element, propertyName, e => e.GetDecimal(), 0m);
    }

    /// <inheritdoc />
    public long[] ParseLongArray(JsonElement element, string propertyName)
    {
        return ParseArray(element, propertyName, e => e.GetInt64(), 0L);
    }

    private static object? ExtractValue(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => element.GetString(),
            JsonValueKind.Number => element.TryGetInt64(out var longValue) ? longValue : element.GetDecimal(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => null,
            _ => element.ToString()
        };
    }
}
