namespace LegendaryGuacamole.Models.Common;

public class ShortDate
{
    public required int Year { get; set; }
    public required int Month { get; set; }
    public required int Day { get; set; }
    public DateOnly ToDateOnly() => new(Year, Month, Day);
}