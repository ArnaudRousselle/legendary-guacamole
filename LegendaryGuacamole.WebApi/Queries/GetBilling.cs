using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class GetBilling : WorkspaceQuery<GetBillingInput, GetBillingResult, GetBillingOutput>
{
    public override GetBillingOutput Map(Workspace workspace, GetBillingResult result)
    {
        var billing = workspace.Billings[result.Index];
        return new()
        {
            Id = billing.Id,
            ValuationDate = new()
            {
                Year = billing.ValuationDate.Year,
                Month = billing.ValuationDate.Month,
                Day = billing.ValuationDate.Day
            },
            Title = billing.Title,
            Amount = billing.Amount,
            Checked = billing.Checked,
            Comment = billing.Comment,
            IsSaving = billing.IsSaving
        };
    }
}

public class GetBillingResult
{
    public required int Index { get; set; }
}
