using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class EditRepetitiveBilling : WorkspaceQuery<EditRepetitiveBillingInput, EditRepetitiveBillingEvent, EditRepetitiveBillingOutput>
{
    public override EditRepetitiveBillingOutput Map(Workspace workspace, EditRepetitiveBillingEvent evt)
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

public class EditRepetitiveBillingEvent
{
    public required RepetitiveBilling RepetitiveBilling { get; set; }
}
