namespace YFinance.Models;

/// <summary>
/// Raw info payload flattened into a dictionary for flexible access.
/// </summary>
public class InfoData
{
    public string Symbol { get; set; } = string.Empty;
    public Dictionary<string, object?> Values { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}
