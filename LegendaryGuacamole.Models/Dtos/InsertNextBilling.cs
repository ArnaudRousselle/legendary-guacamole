namespace LegendaryGuacamole.Models.Dtos;

public class InsertNextBillingInput
{
    public required Guid RepetitiveBillingId { get; set; }
}

public class InsertNextBillingOutput
{
    public required Guid BillingId { get; set; }
}
