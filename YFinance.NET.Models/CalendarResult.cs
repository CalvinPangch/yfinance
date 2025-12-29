namespace YFinance.NET.Models;

/// <summary>
/// Calendar visualization results with normalized records.
/// </summary>
public class CalendarResult
{
    public string CalendarType { get; set; } = string.Empty;
    public List<string> Columns { get; set; } = new();
    public List<Dictionary<string, object?>> Records { get; set; } = new();
}
