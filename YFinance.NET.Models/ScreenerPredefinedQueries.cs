namespace YFinance.NET.Models;

/// <summary>
/// Predefined Yahoo Finance screener queries.
/// </summary>
public static class ScreenerPredefinedQueries
{
    public static readonly IReadOnlyDictionary<string, ScreenerPredefinedQuery> All =
        new Dictionary<string, ScreenerPredefinedQuery>(StringComparer.OrdinalIgnoreCase)
        {
            ["aggressive_small_caps"] = new ScreenerPredefinedQuery
            {
                SortField = "eodvolume",
                SortType = "DESC",
                Query = new EquityQuery("AND",
                    new EquityQuery("IS-IN", "exchange", "NMS", "NYQ"),
                    new EquityQuery("LT", "epsgrowth.lasttwelvemonths", 15))
            },
            ["day_gainers"] = new ScreenerPredefinedQuery
            {
                SortField = "percentchange",
                SortType = "DESC",
                Query = new EquityQuery("AND",
                    new EquityQuery("GT", "percentchange", 3),
                    new EquityQuery("EQ", "region", "us"),
                    new EquityQuery("GTE", "intradaymarketcap", 2000000000),
                    new EquityQuery("GTE", "intradayprice", 5),
                    new EquityQuery("GT", "dayvolume", 15000))
            },
            ["day_losers"] = new ScreenerPredefinedQuery
            {
                SortField = "percentchange",
                SortType = "ASC",
                Query = new EquityQuery("AND",
                    new EquityQuery("LT", "percentchange", -2.5m),
                    new EquityQuery("EQ", "region", "us"),
                    new EquityQuery("GTE", "intradaymarketcap", 2000000000),
                    new EquityQuery("GTE", "intradayprice", 5),
                    new EquityQuery("GT", "dayvolume", 20000))
            },
            ["growth_technology_stocks"] = new ScreenerPredefinedQuery
            {
                SortField = "eodvolume",
                SortType = "DESC",
                Query = new EquityQuery("AND",
                    new EquityQuery("GTE", "quarterlyrevenuegrowth.quarterly", 25),
                    new EquityQuery("GTE", "epsgrowth.lasttwelvemonths", 25),
                    new EquityQuery("EQ", "sector", "Technology"),
                    new EquityQuery("IS-IN", "exchange", "NMS", "NYQ"))
            },
            ["most_actives"] = new ScreenerPredefinedQuery
            {
                SortField = "dayvolume",
                SortType = "DESC",
                Query = new EquityQuery("AND",
                    new EquityQuery("EQ", "region", "us"),
                    new EquityQuery("GTE", "intradaymarketcap", 2000000000),
                    new EquityQuery("GT", "dayvolume", 5000000))
            },
            ["most_shorted_stocks"] = new ScreenerPredefinedQuery
            {
                SortField = "short_percentage_of_shares_outstanding.value",
                SortType = "DESC",
                Count = 25,
                Offset = 0,
                Query = new EquityQuery("AND",
                    new EquityQuery("EQ", "region", "us"),
                    new EquityQuery("GT", "intradayprice", 1),
                    new EquityQuery("GT", "avgdailyvol3m", 200000))
            },
            ["small_cap_gainers"] = new ScreenerPredefinedQuery
            {
                SortField = "eodvolume",
                SortType = "DESC",
                Query = new EquityQuery("AND",
                    new EquityQuery("LT", "intradaymarketcap", 2000000000),
                    new EquityQuery("IS-IN", "exchange", "NMS", "NYQ"))
            },
            ["undervalued_growth_stocks"] = new ScreenerPredefinedQuery
            {
                SortField = "eodvolume",
                SortType = "DESC",
                Query = new EquityQuery("AND",
                    new EquityQuery("BTWN", "peratio.lasttwelvemonths", 0, 20),
                    new EquityQuery("LT", "pegratio_5y", 1),
                    new EquityQuery("GTE", "epsgrowth.lasttwelvemonths", 25),
                    new EquityQuery("IS-IN", "exchange", "NMS", "NYQ"))
            },
            ["undervalued_large_caps"] = new ScreenerPredefinedQuery
            {
                SortField = "eodvolume",
                SortType = "DESC",
                Query = new EquityQuery("AND",
                    new EquityQuery("BTWN", "peratio.lasttwelvemonths", 0, 20),
                    new EquityQuery("LT", "pegratio_5y", 1),
                    new EquityQuery("BTWN", "intradaymarketcap", 10000000000, 100000000000),
                    new EquityQuery("IS-IN", "exchange", "NMS", "NYQ"))
            },
            ["conservative_foreign_funds"] = new ScreenerPredefinedQuery
            {
                SortField = "fundnetassets",
                SortType = "DESC",
                Query = new FundQuery("AND",
                    new FundQuery("IS-IN", "categoryname", "Foreign Large Value", "Foreign Large Blend", "Foreign Large Growth", "Foreign Small/Mid Growth", "Foreign Small/Mid Blend", "Foreign Small/Mid Value"),
                    new FundQuery("IS-IN", "performanceratingoverall", 4, 5),
                    new FundQuery("LT", "initialinvestment", 100001),
                    new FundQuery("LT", "annualreturnnavy1categoryrank", 50),
                    new FundQuery("IS-IN", "riskratingoverall", 1, 2, 3),
                    new FundQuery("EQ", "exchange", "NAS"))
            },
            ["high_yield_bond"] = new ScreenerPredefinedQuery
            {
                SortField = "fundnetassets",
                SortType = "DESC",
                Query = new FundQuery("AND",
                    new FundQuery("IS-IN", "performanceratingoverall", 4, 5),
                    new FundQuery("LT", "initialinvestment", 100001),
                    new FundQuery("LT", "annualreturnnavy1categoryrank", 50),
                    new FundQuery("IS-IN", "riskratingoverall", 1, 2, 3),
                    new FundQuery("EQ", "categoryname", "High Yield Bond"),
                    new FundQuery("EQ", "exchange", "NAS"))
            },
            ["portfolio_anchors"] = new ScreenerPredefinedQuery
            {
                SortField = "fundnetassets",
                SortType = "DESC",
                Query = new FundQuery("AND",
                    new FundQuery("EQ", "categoryname", "Large Blend"),
                    new FundQuery("IS-IN", "performanceratingoverall", 4, 5),
                    new FundQuery("LT", "initialinvestment", 100001),
                    new FundQuery("LT", "annualreturnnavy1categoryrank", 50),
                    new FundQuery("EQ", "exchange", "NAS"))
            },
            ["solid_large_growth_funds"] = new ScreenerPredefinedQuery
            {
                SortField = "fundnetassets",
                SortType = "DESC",
                Query = new FundQuery("AND",
                    new FundQuery("EQ", "categoryname", "Large Growth"),
                    new FundQuery("IS-IN", "performanceratingoverall", 4, 5),
                    new FundQuery("LT", "initialinvestment", 100001),
                    new FundQuery("LT", "annualreturnnavy1categoryrank", 50),
                    new FundQuery("EQ", "exchange", "NAS"))
            },
            ["solid_midcap_growth_funds"] = new ScreenerPredefinedQuery
            {
                SortField = "fundnetassets",
                SortType = "DESC",
                Query = new FundQuery("AND",
                    new FundQuery("EQ", "categoryname", "Mid-Cap Growth"),
                    new FundQuery("IS-IN", "performanceratingoverall", 4, 5),
                    new FundQuery("LT", "initialinvestment", 100001),
                    new FundQuery("LT", "annualreturnnavy1categoryrank", 50),
                    new FundQuery("EQ", "exchange", "NAS"))
            },
            ["top_mutual_funds"] = new ScreenerPredefinedQuery
            {
                SortField = "percentchange",
                SortType = "DESC",
                Query = new FundQuery("AND",
                    new FundQuery("GT", "intradayprice", 15),
                    new FundQuery("IS-IN", "performanceratingoverall", 4, 5),
                    new FundQuery("GT", "initialinvestment", 1000),
                    new FundQuery("EQ", "exchange", "NAS"))
            }
        };
}

public class ScreenerPredefinedQuery
{
    public string SortField { get; set; } = string.Empty;
    public string SortType { get; set; } = "DESC";
    public ScreenerQuery Query { get; set; } = new EquityQuery("EQ", "region", "us");
    public int? Count { get; set; }
    public int? Offset { get; set; }
}
