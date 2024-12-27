

using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class InsertNextBilling : WorkspaceQuery<InsertNextBillingInput, InsertNextBillingEvent, InsertNextBillingOutput>
{
    public override InsertNextBillingOutput Map(Workspace workspace, InsertNextBillingEvent evt)
    => new()
    {
        Billing = new()
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
        },
        RepetitiveBilling = new()
        {
            Id = evt.RepetitiveBilling.Id,
            NextValuationDate = new()
            {
                Year = evt.RepetitiveBilling.NextValuationDate.Year,
                Month = evt.RepetitiveBilling.NextValuationDate.Month,
                Day = evt.RepetitiveBilling.NextValuationDate.Day
            },
            Title = evt.RepetitiveBilling.Title,
            Amount = evt.RepetitiveBilling.Amount,
            IsSaving = evt.RepetitiveBilling.IsSaving,
            Frequence = evt.RepetitiveBilling.Frequence
        }
    };
}

public class InsertNextBillingEvent
{
    public required Billing Billing { get; set; }
    public required RepetitiveBilling RepetitiveBilling { get; set; }
}
