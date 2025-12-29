namespace YFinance.Models;

/// <summary>
/// Represents a corporate action (dividend or stock split).
/// </summary>
public sealed class ActionEntry
{
    public DateTime Date { get; init; }
    public ActionType Type { get; init; }
    public decimal Value { get; init; }
}

/// <summary>
/// Type of corporate action.
/// </summary>
public enum ActionType
{
    /// <summary>
    /// Dividend distribution
    /// </summary>
    Dividend,

    /// <summary>
    /// Stock split
    /// </summary>
    Split
}

/// <summary>
/// Collection of corporate actions (dividends and splits) in chronological order.
/// </summary>
public sealed class ActionData
{
    public string Symbol { get; init; } = string.Empty;
    public IReadOnlyList<ActionEntry> Actions { get; init; } = Array.Empty<ActionEntry>();
}
