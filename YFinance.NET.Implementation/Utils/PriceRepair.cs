using YFinance.NET.Interfaces.Utils;

namespace YFinance.NET.Implementation.Utils;

/// <summary>
/// Advanced price repair implementation for Yahoo Finance anomalies.
/// Handles 100x currency errors, missing data, bad splits, and outliers.
/// Based on Python yfinance repair algorithms.
/// </summary>
public class PriceRepair : IPriceRepair
{
    private const decimal HundredXThreshold = 95m; // Rounded to nearest 20, this captures 80-120 range
    private const decimal OutlierThreshold = 5m; // 5x deviation from median
    private const int MedianWindowSize = 5; // Window size for median filter

    /// <summary>
    /// Detects and repairs 100x currency conversion errors in price data.
    /// </summary>
    /// <param name="prices">Array of prices to repair.</param>
    /// <returns>Repaired prices with 100x errors corrected.</returns>
    public decimal[] Repair100xErrors(decimal[] prices)
    {
        if (prices.Length == 0)
            return prices;

        var repaired = (decimal[])prices.Clone();

        // First pass: Fix sporadic 100x errors using median filter approach
        repaired = FixSporadic100xErrors(repaired);

        // Second pass: Fix block 100x errors (currency unit switches)
        repaired = FixBlock100xErrors(repaired);

        return repaired;
    }

    /// <summary>
    /// Detects and fixes random sporadic 100x errors using median filtering.
    /// Mirrors Python yfinance's _fix_unit_random_mixups method.
    /// </summary>
    private static decimal[] FixSporadic100xErrors(decimal[] prices)
    {
        if (prices.Length < MedianWindowSize)
            return prices;

        var repaired = (decimal[])prices.Clone();
        var medianFiltered = CalculateMedianFilter(prices, MedianWindowSize);

        for (int i = 0; i < repaired.Length; i++)
        {
            if (repaired[i] <= 0m || medianFiltered[i] <= 0m)
                continue;

            var ratio = repaired[i] / medianFiltered[i];
            var roundedRatio = RoundToNearest20(ratio);

            // Check if ratio is approximately 100 or 0.01 (1/100)
            if (Math.Abs(roundedRatio - 100m) < 20m)
            {
                repaired[i] = repaired[i] / 100m;
            }
            else if (Math.Abs(roundedRatio - 0.01m) < 0.002m)
            {
                repaired[i] = repaired[i] * 100m;
            }
        }

        return repaired;
    }

    /// <summary>
    /// Detects and fixes block 100x errors (permanent currency unit switches).
    /// Mirrors Python yfinance's _fix_unit_switch method.
    /// </summary>
    private static decimal[] FixBlock100xErrors(decimal[] prices)
    {
        if (prices.Length < 2)
            return prices;

        var repaired = (decimal[])prices.Clone();

        // Find the most common price level (excluding zeros)
        var nonZeroPrices = repaired.Where(p => p > 0m).ToArray();
        if (nonZeroPrices.Length == 0)
            return repaired;

        var medianPrice = CalculateMedian(nonZeroPrices);

        // Check for systematic 100x error by comparing segments
        int midPoint = repaired.Length / 2;
        var firstHalfMedian = CalculateMedian(repaired.Take(midPoint).Where(p => p > 0m).ToArray());
        var secondHalfMedian = CalculateMedian(repaired.Skip(midPoint).Where(p => p > 0m).ToArray());

        if (firstHalfMedian > 0m && secondHalfMedian > 0m)
        {
            var ratio = firstHalfMedian / secondHalfMedian;

            // If first half is ~100x second half, divide first half by 100
            if (ratio >= HundredXThreshold && ratio <= 105m)
            {
                for (int i = 0; i < midPoint; i++)
                {
                    if (repaired[i] > 0m)
                        repaired[i] /= 100m;
                }
            }
            // If second half is ~100x first half, divide second half by 100
            else if (ratio <= 1m / HundredXThreshold && ratio >= 1m / 105m)
            {
                for (int i = midPoint; i < repaired.Length; i++)
                {
                    if (repaired[i] > 0m)
                        repaired[i] /= 100m;
                }
            }
        }

        return repaired;
    }

    /// <summary>
    /// Rounds ratio to nearest 20 for classification (mirrors Python approach).
    /// </summary>
    private static decimal RoundToNearest20(decimal value)
    {
        return Math.Round(value / 20m) * 20m;
    }

    /// <summary>
    /// Calculates median filter output for price series.
    /// </summary>
    private static decimal[] CalculateMedianFilter(decimal[] prices, int windowSize)
    {
        var result = new decimal[prices.Length];
        int halfWindow = windowSize / 2;

        for (int i = 0; i < prices.Length; i++)
        {
            int start = Math.Max(0, i - halfWindow);
            int end = Math.Min(prices.Length - 1, i + halfWindow);
            var window = prices.Skip(start).Take(end - start + 1).Where(p => p > 0m).ToArray();

            result[i] = window.Length > 0 ? CalculateMedian(window) : prices[i];
        }

        return result;
    }

    /// <summary>
    /// Repairs zero or negative prices by forward and backward filling with the last good value.
    /// </summary>
    /// <param name="prices">Array of prices to repair.</param>
    /// <returns>Repaired prices with zeros filled.</returns>
    public decimal[] RepairZeroValues(decimal[] prices)
    {
        if (prices.Length == 0)
            return prices;

        var repaired = (decimal[])prices.Clone();

        // Forward fill: Use last known good value
        decimal lastGood = 0m;
        for (int i = 0; i < repaired.Length; i++)
        {
            if (repaired[i] > 0m)
            {
                lastGood = repaired[i];
            }
            else if (repaired[i] <= 0m && lastGood > 0m)
            {
                repaired[i] = lastGood;
            }
        }

        // Backward fill: For any remaining zeros at the start
        decimal firstGood = 0m;
        for (int i = repaired.Length - 1; i >= 0; i--)
        {
            if (repaired[i] > 0m)
            {
                firstGood = repaired[i];
            }
            else if (repaired[i] <= 0m && firstGood > 0m)
            {
                repaired[i] = firstGood;
            }
        }

        return repaired;
    }

    /// <summary>
    /// Detects and repairs errors related to stock splits.
    /// </summary>
    /// <param name="prices">Array of prices to repair.</param>
    /// <param name="splitDates">Dictionary mapping split dates to split ratios.</param>
    /// <returns>Repaired prices with split errors corrected.</returns>
    public decimal[] RepairBadSplits(decimal[] prices, Dictionary<DateTime, decimal> splitDates)
    {
        if (prices.Length == 0 || splitDates.Count == 0)
            return prices;

        var repaired = (decimal[])prices.Clone();
        var ratios = splitDates.Values.Where(r => r > 0m).Distinct().ToArray();

        if (ratios.Length == 0)
            return repaired;

        for (int i = 1; i < repaired.Length; i++)
        {
            var prev = repaired[i - 1];
            var current = repaired[i];

            if (prev <= 0m || current <= 0m)
                continue;

            var ratio = prev / current;
            foreach (var splitRatio in ratios)
            {
                if (IsClose(ratio, splitRatio) || IsClose(ratio, 1m / splitRatio))
                {
                    var adjustment = IsClose(ratio, splitRatio) ? splitRatio : 1m / splitRatio;
                    for (int j = 0; j < i; j++)
                    {
                        repaired[j] /= adjustment;
                    }
                    break;
                }
            }
        }

        return repaired;
    }

    /// <summary>
    /// Applies comprehensive price repairs including zero values, 100x errors, outliers, and splits.
    /// </summary>
    /// <param name="prices">Array of prices to repair.</param>
    /// <param name="dates">Array of dates corresponding to prices.</param>
    /// <param name="splitDates">Optional dictionary mapping split dates to split ratios.</param>
    /// <returns>Fully repaired prices.</returns>
    public decimal[] RepairPrices(decimal[] prices, DateTime[] dates, Dictionary<DateTime, decimal>? splitDates = null)
    {
        var repaired = RepairZeroValues(prices);
        repaired = Repair100xErrors(repaired);
        repaired = RepairOutliers(repaired);

        if (splitDates != null && splitDates.Count > 0)
        {
            repaired = AdjustForKnownSplits(repaired, dates, splitDates);
            repaired = RepairBadSplits(repaired, splitDates);
        }

        return repaired;
    }

    /// <summary>
    /// Detects and repairs outliers using median filtering.
    /// Uses a rolling window approach similar to Python yfinance.
    /// </summary>
    private static decimal[] RepairOutliers(decimal[] prices)
    {
        if (prices.Length < MedianWindowSize)
            return prices;

        var repaired = (decimal[])prices.Clone();
        var medianFiltered = CalculateMedianFilter(prices, MedianWindowSize);

        for (int i = 0; i < repaired.Length; i++)
        {
            if (repaired[i] <= 0m || medianFiltered[i] <= 0m)
                continue;

            var ratio = repaired[i] / medianFiltered[i];

            // If price deviates more than OutlierThreshold (5x) from local median, replace with median
            if (ratio > OutlierThreshold || ratio < 1m / OutlierThreshold)
            {
                repaired[i] = medianFiltered[i];
            }
        }

        return repaired;
    }

    /// <summary>
    /// Calculates the median of an array of decimals.
    /// </summary>
    private static decimal CalculateMedian(decimal[] values)
    {
        if (values.Length == 0)
            return 0m;

        var sorted = (decimal[])values.Clone();
        Array.Sort(sorted);

        int mid = sorted.Length / 2;
        if (sorted.Length % 2 == 0)
            return (sorted[mid - 1] + sorted[mid]) / 2m;
        else
            return sorted[mid];
    }

    private static decimal[] AdjustForKnownSplits(decimal[] prices, DateTime[] dates, Dictionary<DateTime, decimal> splitDates)
    {
        if (prices.Length == 0 || dates.Length == 0 || splitDates.Count == 0)
            return prices;

        var repaired = (decimal[])prices.Clone();

        foreach (var split in splitDates.OrderBy(kvp => kvp.Key))
        {
            var splitDate = split.Key.Date;
            var splitRatio = split.Value;
            if (splitRatio <= 0m)
                continue;

            var index = Array.FindLastIndex(dates, date => date.Date <= splitDate);
            if (index <= 0 || index >= repaired.Length)
                continue;

            var prev = repaired[index - 1];
            var current = repaired[index];
            if (prev <= 0m || current <= 0m)
                continue;

            var ratio = prev / current;
            if (!IsClose(ratio, splitRatio) && !IsClose(ratio, 1m / splitRatio))
                continue;

            var adjustment = IsClose(ratio, splitRatio) ? splitRatio : 1m / splitRatio;
            for (int i = 0; i < index; i++)
            {
                repaired[i] /= adjustment;
            }
        }

        return repaired;
    }

    private static bool IsClose(decimal left, decimal right)
    {
        var diff = Math.Abs(left - right);
        return diff <= right * 0.05m;
    }
}
