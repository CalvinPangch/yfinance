namespace YFinance.Models;

/// <summary>
/// Request parameters for Yahoo Finance calendar visualization endpoint.
/// </summary>
public class CalendarRequest
{
    public string CalendarType { get; set; } = "sp_earnings";
    public CalendarQuery Query { get; set; } = new CalendarQuery("and", Array.Empty<object>());
    public int Limit { get; set; } = 12;
    public int Offset { get; set; } = 0;
}
