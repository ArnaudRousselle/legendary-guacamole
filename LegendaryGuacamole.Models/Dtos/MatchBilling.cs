namespace LegendaryGuacamole.Models.Dtos;

public class MatchBillingInput
{
    public required string ImportLineId { get; set; }
    public required Guid? BillingId { get; set; }
}

public class MatchBillingOutput
{
}