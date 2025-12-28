namespace YFinance.Models;

/// <summary>
/// Institutional and insider holdings data.
/// </summary>
public class HolderData
{
    public string Symbol { get; set; } = string.Empty;

    // Major holders
    public decimal? InsidersPercentHeld { get; set; }
    public decimal? InstitutionsPercentHeld { get; set; }
    public decimal? InstitutionsFloatPercentHeld { get; set; }
    public int? InstitutionsCount { get; set; }

    // Institutional holders
    public List<InstitutionalHolder>? InstitutionalHolders { get; set; }

    // Insider transactions
    public List<InsiderTransaction>? InsiderTransactions { get; set; }

    // Insider holders
    public List<InsiderHolder>? InsiderHolders { get; set; }

    // Fund ownership
    public List<FundHolder>? FundHolders { get; set; }
}

public class InstitutionalHolder
{
    public string Holder { get; set; } = string.Empty;
    public long Shares { get; set; }
    public DateTime DateReported { get; set; }
    public decimal PercentOut { get; set; }
    public decimal Value { get; set; }
}

public class InsiderTransaction
{
    public string Insider { get; set; } = string.Empty;
    public string Relation { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public int Shares { get; set; }
    public decimal? Value { get; set; }
}

public class InsiderHolder
{
    public string Name { get; set; } = string.Empty;
    public string? Relation { get; set; }
    public string? Url { get; set; }
    public string? TransactionDescription { get; set; }
    public DateTime? LatestTransDate { get; set; }
    public decimal? PositionDirect { get; set; }
    public DateTime? PositionDirectDate { get; set; }
    public decimal? PositionIndirect { get; set; }
    public DateTime? PositionIndirectDate { get; set; }
}

public class FundHolder
{
    public string Holder { get; set; } = string.Empty;
    public long Shares { get; set; }
    public DateTime DateReported { get; set; }
    public decimal PercentOut { get; set; }
    public decimal Value { get; set; }
}
