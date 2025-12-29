using System.Linq;
using System.Text.Json;
using YFinance.NET.Interfaces;
using YFinance.NET.Interfaces.Scrapers;
using YFinance.NET.Interfaces.Utils;
using YFinance.NET.Models;

namespace YFinance.NET.Implementation.Scrapers;

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

        statement.IncomeStatementAnnualHistory = ParseStatementEntries(result, "incomeStatementHistory", "incomeStatementHistory");
        statement.IncomeStatementQuarterlyHistory = ParseStatementEntries(result, "incomeStatementHistoryQuarterly", "incomeStatementHistory");
        statement.BalanceSheetAnnualHistory = ParseStatementEntries(result, "balanceSheetHistory", "balanceSheetStatements");
        statement.BalanceSheetQuarterlyHistory = ParseStatementEntries(result, "balanceSheetHistoryQuarterly", "balanceSheetStatements");
        statement.CashFlowAnnualHistory = ParseStatementEntries(result, "cashflowStatementHistory", "cashflowStatements");
        statement.CashFlowQuarterlyHistory = ParseStatementEntries(result, "cashflowStatementHistoryQuarterly", "cashflowStatements");

        statement.IncomeStatementAnnual = statement.IncomeStatementAnnualHistory?.FirstOrDefault()?.Metrics;
        statement.IncomeStatementQuarterly = statement.IncomeStatementQuarterlyHistory?.FirstOrDefault()?.Metrics;
        statement.BalanceSheetAnnual = statement.BalanceSheetAnnualHistory?.FirstOrDefault()?.Metrics;
        statement.BalanceSheetQuarterly = statement.BalanceSheetQuarterlyHistory?.FirstOrDefault()?.Metrics;
        statement.CashFlowAnnual = statement.CashFlowAnnualHistory?.FirstOrDefault()?.Metrics;
        statement.CashFlowQuarterly = statement.CashFlowQuarterlyHistory?.FirstOrDefault()?.Metrics;

        statement.IncomeStatementTTM = ComputeTtm(statement.IncomeStatementQuarterlyHistory);
        statement.CashFlowTTM = ComputeTtm(statement.CashFlowQuarterlyHistory);

        return statement;
    }

    private List<StatementEntry>? ParseStatementEntries(JsonElement result, string moduleName, string arrayName)
    {
        if (!result.TryGetProperty(moduleName, out var module))
            return null;

        if (!module.TryGetProperty(arrayName, out var statements) || statements.ValueKind != JsonValueKind.Array || statements.GetArrayLength() == 0)
            return null;

        var output = new List<StatementEntry>();

        foreach (var statement in statements.EnumerateArray())
        {
            var flat = _dataParser.FlattenResponse(statement);
            var metrics = new Dictionary<string, decimal?>();
            DateTime? endDate = null;

            foreach (var entry in flat)
            {
                if (entry.Key.Equals("endDate", StringComparison.OrdinalIgnoreCase))
                {
                    if (entry.Value is long endDateRaw)
                        endDate = _dataParser.UnixTimestampToDateTime(endDateRaw);
                    continue;
                }

                if (entry.Value is decimal decimalValue)
                    metrics[entry.Key] = decimalValue;
                else if (entry.Value is long longValue)
                    metrics[entry.Key] = longValue;
            }

            if (metrics.Count > 0)
                output.Add(new StatementEntry { EndDate = endDate, Metrics = metrics });
        }

        return output.Count > 0 ? output : null;
    }

    private static Dictionary<string, decimal?>? ComputeTtm(List<StatementEntry>? quarterlyHistory)
    {
        if (quarterlyHistory == null || quarterlyHistory.Count == 0)
            return null;

        var recent = quarterlyHistory
            .OrderByDescending(entry => entry.EndDate ?? DateTime.MinValue)
            .Take(4)
            .ToList();

        if (recent.Count < 4)
            return null;

        var totals = new Dictionary<string, decimal?>();
        foreach (var entry in recent)
        {
            foreach (var metric in entry.Metrics)
            {
                if (!metric.Value.HasValue)
                    continue;

                if (!totals.TryGetValue(metric.Key, out var current) || !current.HasValue)
                    totals[metric.Key] = metric.Value;
                else
                    totals[metric.Key] = current.Value + metric.Value.Value;
            }
        }

        return totals.Count > 0 ? totals : null;
    }
}
