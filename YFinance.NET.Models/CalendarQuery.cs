namespace YFinance.NET.Models;

/// <summary>
/// Query builder for Yahoo Finance calendar visualization endpoint.
/// </summary>
public class CalendarQuery
{
    public string Operator { get; }
    public List<object> Operands { get; }

    public CalendarQuery(string @operator, IEnumerable<object> operands)
    {
        Operator = @operator;
        Operands = operands?.ToList() ?? throw new ArgumentNullException(nameof(operands));
    }

    public CalendarQuery(string @operator, params object[] operands)
        : this(@operator, (IEnumerable<object>)operands)
    {
    }

    public bool IsEmpty => Operands.Count == 0;

    public Dictionary<string, object?> ToDictionary()
    {
        return new Dictionary<string, object?>
        {
            ["operator"] = Operator,
            ["operands"] = Operands.Select(operand =>
                operand is CalendarQuery query ? query.ToDictionary() : operand).ToList()
        };
    }
}
