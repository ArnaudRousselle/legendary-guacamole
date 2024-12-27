using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.Models.Dtos;

public class InsertNextBillingInput
{
    public Guid Id { get; set; }
    public bool IncrementNextValuationDate { get; set; }
}

public class InsertNextBillingOutput
{
    public required InsertNextBillingOutputBilling Billing { get; set; }
    public required InsertNextBillingOutputRepetitiveBilling RepetitiveBilling { get; set; }
}

public class InsertNextBillingOutputBilling
{
    public required Guid Id { get; set; }
    public required ShortDate ValuationDate { get; set; }
    public required string Title { get; set; } = "";
    public required decimal Amount { get; set; }
    public required bool Checked { get; set; }
    public required string? Comment { get; set; }
    public required bool IsSaving { get; set; }
}

public class InsertNextBillingOutputRepetitiveBilling
{
    public required Guid Id { get; set; }
    public required ShortDate NextValuationDate { get; set; }
    public required string Title { get; set; }
    public required decimal Amount { get; set; }
    public required bool IsSaving { get; set; }
    public required Frequence Frequence { get; set; }
}