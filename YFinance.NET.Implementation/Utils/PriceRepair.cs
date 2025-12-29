using YFinance.NET.Interfaces.Utils;

namespace YFinance.NET.Implementation.Utils;

/// <summary>
/// Basic price repair implementation for common Yahoo Finance anomalies.
/// </summary>
public class PriceRepair : IPriceRepair
{
    public decimal[] Repair100xErrors(decimal[] prices)
    {
        if (prices.Length == 0)
            return prices;

        var repaired = (decimal[])prices.Clone();

        for (int i = 1; i < repaired.Length; i++)
        {
            var prev = repaired[i - 1];
            var current = repaired[i];

            if (prev <= 0 || current <= 0)
                continue;

            var ratio = current / prev;
            if (ratio >= 100m)
                repaired[i] = current / 100m;
            else if (ratio <= 0.01m)
                repaired[i] = current * 100m;
        }

        return repaired;
    }

    public decimal[] RepairZeroValues(decimal[] prices)
    {
        if (prices.Length == 0)
            return prices;

        var repaired = (decimal[])prices.Clone();
        decimal last = 0m;

        for (int i = 0; i < repaired.Length; i++)
        {
            if (repaired[i] == 0m && last > 0m)
                repaired[i] = last;
            else if (repaired[i] > 0m)
                last = repaired[i];
        }

        return repaired;
    }

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

    private static decimal[] RepairOutliers(decimal[] prices)
    {
        if (prices.Length < 5)
            return prices;

        var repaired = (decimal[])prices.Clone();
        for (int i = 2; i < repaired.Length; i++)
        {
            var windowStart = Math.Max(0, i - 4);
            var window = repaired.Skip(windowStart).Take(i - windowStart).Where(p => p > 0m).ToArray();
            if (window.Length == 0)
                continue;

            Array.Sort(window);
            var median = window[window.Length / 2];
            if (median <= 0m || repaired[i] <= 0m)
                continue;

            if (repaired[i] > median * 5m || repaired[i] < median / 5m)
                repaired[i] = median;
        }

        return repaired;
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
