using FluentAssertions;
using Moq;
using Xunit;
using YFinance.Implementation.Scrapers;
using YFinance.Implementation.Utils;
using YFinance.Interfaces;
using YFinance.Models;

namespace YFinance.Tests.Unit.Scrapers;

public class FundamentalsScraperTests
{
    [Fact]
    public async Task GetFinancialStatementsAsync_ParsesHistoryAndTtm()
    {
        // Arrange
        var symbol = "AAPL";
        var response = """
        {
            "quoteSummary": {
                "result": [{
                    "incomeStatementHistoryQuarterly": {
                        "incomeStatementHistory": [
                            { "endDate": { "raw": 1711843200 }, "totalRevenue": { "raw": 100 }, "netIncome": { "raw": 10 } },
                            { "endDate": { "raw": 1704067200 }, "totalRevenue": { "raw": 110 }, "netIncome": { "raw": 11 } },
                            { "endDate": { "raw": 1696118400 }, "totalRevenue": { "raw": 120 }, "netIncome": { "raw": 12 } },
                            { "endDate": { "raw": 1688169600 }, "totalRevenue": { "raw": 130 }, "netIncome": { "raw": 13 } }
                        ]
                    },
                    "incomeStatementHistory": {
                        "incomeStatementHistory": [
                            { "endDate": { "raw": 1704067200 }, "totalRevenue": { "raw": 400 }, "netIncome": { "raw": 40 } }
                        ]
                    }
                }]
            }
        }
        """;

        var client = new Mock<IYahooFinanceClient>();
        client.Setup(c => c.GetAsync(
                It.IsAny<string>(),
                It.IsAny<Dictionary<string, string>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var scraper = new FundamentalsScraper(client.Object, new DataParser());

        // Act
        var result = await scraper.GetFinancialStatementsAsync(symbol);

        // Assert
        result.Symbol.Should().Be(symbol);
        result.IncomeStatementQuarterlyHistory.Should().NotBeNull();
        result.IncomeStatementQuarterlyHistory!.Should().HaveCount(4);
        result.IncomeStatementAnnual.Should().NotBeNull();
        result.IncomeStatementAnnual!["totalRevenue"].Should().Be(400);
        result.IncomeStatementTTM.Should().NotBeNull();
        result.IncomeStatementTTM!["totalRevenue"].Should().Be(460);
        result.IncomeStatementTTM["netIncome"].Should().Be(46);
    }
}
