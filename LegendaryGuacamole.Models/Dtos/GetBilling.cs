using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.Models.Dtos;

public class GetBillingInput
{
    public required Guid Id { get; set; }
}

public class GetBillingOutput
{
    public required Guid Id { get; set; }
    public required ShortDate ValuationDate { get; set; }
    public required string Title { get; set; } = "";
    public required decimal Amount { get; set; }
    public required bool Checked { get; set; }
    public required string Comment { get; set; }
    public required bool IsSaving { get; set; }
}