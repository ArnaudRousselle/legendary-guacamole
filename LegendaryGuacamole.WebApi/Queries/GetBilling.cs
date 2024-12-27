using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class GetBilling : WorkspaceQuery<GetBillingInput, GetBillingEvent, GetBillingOutput>
{
    public override GetBillingOutput Map(Workspace workspace, GetBillingEvent evt)
    => new()
    {
        Id = evt.Billing.Id,
        ValuationDate = new()
        {
            Year = evt.Billing.ValuationDate.Year,
            Month = evt.Billing.ValuationDate.Month,
            Day = evt.Billing.ValuationDate.Day
        },
        Title = evt.Billing.Title,
        Amount = evt.Billing.Amount,
        Checked = evt.Billing.Checked,
        Comment = evt.Billing.Comment,
        IsSaving = evt.Billing.IsSaving
    };
}

public class GetBillingEvent
{
    public required Billing Billing { get; set; }
}
