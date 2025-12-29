using YFinance.NET.Models;

namespace YFinance.NET.Interfaces.Scrapers;

/// <summary>
/// Scraper interface for retrieving financial statements.
/// Handles income statements, balance sheets, and cash flow statements.
/// </summary>
public interface IFundamentalsScraper
{
    /// <summary>
    /// Retrieves financial statements for a specific ticker.
    /// </summary>
    /// <param name="symbol">Ticker symbol</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Financial statements including income, balance sheet, and cash flow</returns>
    Task<FinancialStatement> GetFinancialStatementsAsync(string symbol, CancellationToken cancellationToken = default);
}
