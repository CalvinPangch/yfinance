namespace YFinance.Interfaces.Utils;

/// <summary>
/// Utility interface for detecting and repairing price data errors.
/// Handles 100x errors, zero values, and bad stock splits.
/// </summary>
public interface IPriceRepair
{
    /// <summary>
    /// Detects and repairs 100x errors in price data.
    /// </summary>
    /// <param name="prices">Array of prices</param>
    /// <returns>Repaired prices</returns>
    decimal[] Repair100xErrors(decimal[] prices);

    /// <summary>
    /// Detects and repairs zero values in price data.
    /// </summary>
    /// <param name="prices">Array of prices</param>
    /// <returns>Repaired prices</returns>
    decimal[] RepairZeroValues(decimal[] prices);

    /// <summary>
    /// Detects bad stock splits and adjusts prices accordingly.
    /// </summary>
    /// <param name="prices">Array of prices</param>
    /// <param name="splitDates">Dictionary of split dates and ratios</param>
    /// <returns>Adjusted prices</returns>
    decimal[] RepairBadSplits(decimal[] prices, Dictionary<DateTime, decimal> splitDates);

    /// <summary>
    /// Performs comprehensive price repair including all error types.
    /// </summary>
    /// <param name="prices">Array of prices</param>
    /// <param name="dates">Corresponding dates</param>
    /// <param name="splitDates">Known split dates and ratios</param>
    /// <returns>Fully repaired prices</returns>
    decimal[] RepairPrices(decimal[] prices, DateTime[] dates, Dictionary<DateTime, decimal>? splitDates = null);
}
