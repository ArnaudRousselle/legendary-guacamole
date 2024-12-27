using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class EditBilling : WorkspaceQuery<EditBillingInput, EditBillingEvent, EditBillingOutput>
{
    public override EditBillingOutput Map(Workspace workspace, EditBillingEvent evt)
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

public class EditBillingEvent
{
    public required Billing Billing { get; set; }
}
