using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class AddRepetitiveBilling : WorkspaceQuery<AddRepetitiveBillingInput, AddRepetitiveBillingEvent, AddRepetitiveBillingOutput>
{
    public override AddRepetitiveBillingOutput Map(Workspace workspace, AddRepetitiveBillingEvent evt)
    => new()
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
    };
}

public class AddRepetitiveBillingEvent
{
    public required RepetitiveBilling RepetitiveBilling { get; set; }
}
