using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.Models.Dtos;

public class MultipleInsertNextBillingInput
{
    public required ShortDate MaxDate { get; set; }
}

public class MultipleInsertNextBillingOutput
{
    public required Guid[] BillingIds { get; set; }
}
