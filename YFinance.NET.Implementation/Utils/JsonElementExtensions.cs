using System.Text.Json;

namespace YFinance.NET.Implementation.Utils;

internal static class JsonElementExtensions
{
    public static string? GetStringOrNull(this JsonElement element, params string[] propertyNames)
    {
        foreach (var name in propertyNames)
        {
            if (element.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String)
                return value.GetString();
        }

        return null;
    }

    public static decimal? GetDecimalOrNull(this JsonElement element, params string[] propertyNames)
    {
        foreach (var name in propertyNames)
        {
            if (!element.TryGetProperty(name, out var value))
                continue;

            if (value.ValueKind == JsonValueKind.Number)
                return value.GetDecimal();

            if (value.ValueKind == JsonValueKind.String &&
                decimal.TryParse(value.GetString(), out var parsed))
                return parsed;
        }

        return null;
    }

    public static long? GetLongOrNull(this JsonElement element, params string[] propertyNames)
    {
        foreach (var name in propertyNames)
        {
            if (!element.TryGetProperty(name, out var value))
                continue;

            if (value.ValueKind == JsonValueKind.Number && value.TryGetInt64(out var parsed))
                return parsed;

            if (value.ValueKind == JsonValueKind.String &&
                long.TryParse(value.GetString(), out var parsedString))
                return parsedString;
        }

        return null;
    }
}
