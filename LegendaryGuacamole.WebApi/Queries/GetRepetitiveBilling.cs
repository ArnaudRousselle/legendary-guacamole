using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class GetRepetitiveBilling : WorkspaceQuery<GetRepetitiveBillingInput, GetRepetitiveBillingResult, GetRepetitiveBillingOutput>
{
    public override GetRepetitiveBillingOutput Map(Workspace workspace, GetRepetitiveBillingResult evt)
    {
        var repetitiveBilling = workspace.RepetitiveBillings[evt.Index];
        return new()
        {
            Id = repetitiveBilling.Id,
            NextValuationDate = new()
            {
                Year = repetitiveBilling.NextValuationDate.Year,
                Month = repetitiveBilling.NextValuationDate.Month,
                Day = repetitiveBilling.NextValuationDate.Day
            },
            Title = repetitiveBilling.Title,
            Amount = repetitiveBilling.Amount,
            IsSaving = repetitiveBilling.IsSaving,
            Frequence = repetitiveBilling.Frequence
        };
    }
}

public class GetRepetitiveBillingResult
{
    public required int Index { get; set; }
}
