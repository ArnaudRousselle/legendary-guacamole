using System.ComponentModel.DataAnnotations;

namespace LegendaryGuacamole.WebApi.Dtos;

public class ShortDate
{
    [Required]
    public int Year { get; set; }
    [Required]
    public int Month { get; set; }
    [Required]
    public int Day { get; set; }
    public DateOnly ToDateOnly() => new(Year, Month, Day);
}
