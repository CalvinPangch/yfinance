using YFinance.NET.Models.Enums;

namespace YFinance.NET.Models.Requests;

/// <summary>
/// Request parameters for corporate actions.
/// </summary>
public class ActionsRequest
{
    public Period? Period { get; set; } = Enums.Period.Max;
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }

    public void Validate()
    {
        if (Period.HasValue && (Start.HasValue || End.HasValue))
            throw new ArgumentException("Cannot specify both Period and Start/End dates.");

        if (!Period.HasValue && (!Start.HasValue || !End.HasValue))
            throw new ArgumentException("Must specify either Period or both Start and End dates.");

        if (Start.HasValue && End.HasValue && Start.Value >= End.Value)
            throw new ArgumentException("Start date must be before End date.");
    }
}
