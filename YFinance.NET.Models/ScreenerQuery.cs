namespace YFinance.NET.Models;

/// <summary>
/// Base class for Yahoo Finance screener queries.
/// </summary>
public class ScreenerQuery
{
    public string Operator { get; }
    public IReadOnlyList<object> Operands { get; }

    public ScreenerQuery(string @operator, IEnumerable<object> operands)
    {
        if (string.IsNullOrWhiteSpace(@operator))
            throw new ArgumentException("Operator cannot be null or empty.", nameof(@operator));

        var operandList = operands?.ToList() ?? throw new ArgumentNullException(nameof(operands));
        if (operandList.Count == 0)
            throw new ArgumentException("Operands cannot be empty.", nameof(operands));

        Operator = @operator.ToUpperInvariant();
        Operands = operandList;
        ValidateOperands();
    }

    public ScreenerQuery(string @operator, params object[] operands)
        : this(@operator, (IEnumerable<object>)operands)
    {
    }

    public Dictionary<string, object?> ToDictionary()
    {
        if (Operator == "IS-IN")
        {
            var field = Operands[0];
            var expanded = Operands.Skip(1)
                .Select(value => new ScreenerQuery("EQ", new[] { field, value }))
                .Cast<object>()
                .ToList();

            return new ScreenerQuery("OR", expanded).ToDictionary();
        }

        var operands = Operands
            .Select(op => op is ScreenerQuery query ? query.ToDictionary() : op)
            .ToList();

        return new Dictionary<string, object?>
        {
            ["operator"] = Operator,
            ["operands"] = operands
        };
    }

    private void ValidateOperands()
    {
        switch (Operator)
        {
            case "OR":
            case "AND":
                if (Operands.Count <= 1)
                    throw new ArgumentException("OR/AND requires two or more operands.");
                break;
            case "EQ":
            case "GT":
            case "LT":
            case "GTE":
            case "LTE":
                if (Operands.Count != 2)
                    throw new ArgumentException($"{Operator} requires exactly two operands.");
                break;
            case "BTWN":
                if (Operands.Count != 3)
                    throw new ArgumentException("BTWN requires exactly three operands.");
                break;
            case "IS-IN":
                if (Operands.Count < 2)
                    throw new ArgumentException("IS-IN requires at least two operands.");
                break;
            default:
                throw new ArgumentException($"Unsupported operator '{Operator}'.");
        }
    }
}

/// <summary>
/// Screener query for equity filters.
/// </summary>
public class EquityQuery : ScreenerQuery
{
    public EquityQuery(string @operator, IEnumerable<object> operands)
        : base(@operator, operands)
    {
    }

    public EquityQuery(string @operator, params object[] operands)
        : base(@operator, operands)
    {
    }
}

/// <summary>
/// Screener query for fund filters.
/// </summary>
public class FundQuery : ScreenerQuery
{
    public FundQuery(string @operator, IEnumerable<object> operands)
        : base(@operator, operands)
    {
    }

    public FundQuery(string @operator, params object[] operands)
        : base(@operator, operands)
    {
    }
}
