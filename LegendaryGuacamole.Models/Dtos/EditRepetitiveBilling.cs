using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.Models.Dtos;

public class EditRepetitiveBillingInput
{
    public required Guid Id { get; set; }
    public required ShortDate NextValuationDate { get; set; }
    public required string Title { get; set; } = "";
    public required decimal Amount { get; set; }
    public required bool IsSaving { get; set; }
    public required Frequence Frequence { get; set; }
}

public class EditRepetitiveBillingOutput
{
    public required Guid Id { get; set; }
    public required ShortDate NextValuationDate { get; set; }
    public required string Title { get; set; }
    public required decimal Amount { get; set; }
    public required bool IsSaving { get; set; }
    public required Frequence Frequence { get; set; }
}