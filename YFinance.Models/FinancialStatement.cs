using YFinance.Models.Enums;

namespace YFinance.Models;

/// <summary>
/// Financial statement data including income statement, balance sheet, and cash flow.
/// </summary>
public class FinancialStatement
{
    public string Symbol { get; set; } = string.Empty;

    // Income Statement
    public Dictionary<string, decimal?>? IncomeStatementAnnual { get; set; }
    public Dictionary<string, decimal?>? IncomeStatementQuarterly { get; set; }
    public List<StatementEntry>? IncomeStatementAnnualHistory { get; set; }
    public List<StatementEntry>? IncomeStatementQuarterlyHistory { get; set; }

    // Balance Sheet
    public Dictionary<string, decimal?>? BalanceSheetAnnual { get; set; }
    public Dictionary<string, decimal?>? BalanceSheetQuarterly { get; set; }
    public List<StatementEntry>? BalanceSheetAnnualHistory { get; set; }
    public List<StatementEntry>? BalanceSheetQuarterlyHistory { get; set; }

    // Cash Flow
    public Dictionary<string, decimal?>? CashFlowAnnual { get; set; }
    public Dictionary<string, decimal?>? CashFlowQuarterly { get; set; }
    public List<StatementEntry>? CashFlowAnnualHistory { get; set; }
    public List<StatementEntry>? CashFlowQuarterlyHistory { get; set; }

    // Trailing Twelve Months (TTM)
    public Dictionary<string, decimal?>? IncomeStatementTTM { get; set; }
    public Dictionary<string, decimal?>? CashFlowTTM { get; set; }
}

public class StatementEntry
{
    public DateTime? EndDate { get; set; }
    public Dictionary<string, decimal?> Metrics { get; set; } = new();
}
