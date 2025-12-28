using System.Text.Json;
using YFinance.Interfaces;
using YFinance.Interfaces.Scrapers;
using YFinance.Interfaces.Utils;
using YFinance.Models;

namespace YFinance.Implementation.Scrapers;

/// <summary>
/// Scraper for financial statements (income statement, balance sheet, cash flow).
/// </summary>
public class FundamentalsScraper : IFundamentalsScraper
{
    private readonly IYahooFinanceClient _client;
    private readonly IDataParser _dataParser;

    public FundamentalsScraper(IYahooFinanceClient client, IDataParser dataParser)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
        _dataParser = dataParser ?? throw new ArgumentNullException(nameof(dataParser));
    }

    public async Task<FinancialStatement> GetFinancialStatementsAsync(string symbol, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or whitespace.", nameof(symbol));

        var queryParams = new Dictionary<string, string>
        {
            { "modules", "incomeStatementHistory,incomeStatementHistoryQuarterly,balanceSheetHistory,balanceSheetHistoryQuarterly,cashflowStatementHistory,cashflowStatementHistoryQuarterly" }
        };

        var endpoint = $"/v10/finance/quoteSummary/{symbol}";
        var jsonResponse = await _client.GetAsync(endpoint, queryParams, cancellationToken).ConfigureAwait(false);
        return ParseFinancialStatements(symbol, jsonResponse);
    }

    private FinancialStatement ParseFinancialStatements(string symbol, string jsonResponse)
    {
        using var document = JsonDocument.Parse(jsonResponse);
        var root = document.RootElement;

        if (!root.TryGetProperty("quoteSummary", out var quoteSummary) ||
            !quoteSummary.TryGetProperty("result", out var results) ||
            results.GetArrayLength() == 0)
        {
            return new FinancialStatement { Symbol = symbol };
        }

        var result = results[0];
        var statement = new FinancialStatement { Symbol = symbol };

        statement.IncomeStatementAnnual = ParseStatement(result, "incomeStatementHistory", "incomeStatementHistory");
        statement.IncomeStatementQuarterly = ParseStatement(result, "incomeStatementHistoryQuarterly", "incomeStatementHistory");
        statement.BalanceSheetAnnual = ParseStatement(result, "balanceSheetHistory", "balanceSheetStatements");
        statement.BalanceSheetQuarterly = ParseStatement(result, "balanceSheetHistoryQuarterly", "balanceSheetStatements");
        statement.CashFlowAnnual = ParseStatement(result, "cashflowStatementHistory", "cashflowStatements");
        statement.CashFlowQuarterly = ParseStatement(result, "cashflowStatementHistoryQuarterly", "cashflowStatements");

        return statement;
    }

    private Dictionary<string, decimal?>? ParseStatement(JsonElement result, string moduleName, string arrayName)
    {
        if (!result.TryGetProperty(moduleName, out var module))
            return null;

        if (!module.TryGetProperty(arrayName, out var statements) || statements.ValueKind != JsonValueKind.Array || statements.GetArrayLength() == 0)
            return null;

        var latest = statements[0];
        var flat = _dataParser.FlattenResponse(latest);
        var output = new Dictionary<string, decimal?>();

        foreach (var entry in flat)
        {
            if (entry.Value is decimal decimalValue)
                output[entry.Key] = decimalValue;
            else if (entry.Value is long longValue)
                output[entry.Key] = longValue;
        }

        return output.Count > 0 ? output : null;
    }
}
