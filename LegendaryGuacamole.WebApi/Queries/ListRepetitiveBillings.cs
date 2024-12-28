using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class ListRepetitiveBillings : WorkspaceQuery<ListRepetitiveBillingsInput, ListRepetitiveBillingsResult, ListRepetitiveBillingsOutput>
{
    public override ListRepetitiveBillingsOutput Map(Workspace workspace, ListRepetitiveBillingsResult evt)
    => new()
    {
        Items = workspace.RepetitiveBillings
        .OrderBy(n => n.NextValuationDate)
        .ThenBy(n => n.Id)
        .Select(n => new ListRepetitiveBillingsOutput.Item
        {
            Id = n.Id,
            NextValuationDate = new()
            {
                Year = n.NextValuationDate.Year,
                Month = n.NextValuationDate.Month,
                Day = n.NextValuationDate.Day
            },
            Title = n.Title,
            Amount = n.Amount,
            IsSaving = n.IsSaving,
            Frequence = n.Frequence
        })
        .ToArray()
    };
}

public class ListRepetitiveBillingsResult
{
}
