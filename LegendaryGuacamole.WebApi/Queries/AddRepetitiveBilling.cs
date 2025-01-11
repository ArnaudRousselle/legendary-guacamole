using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class AddRepetitiveBilling : WorkspaceQuery<AddRepetitiveBillingInput, AddRepetitiveBillingResult, AddRepetitiveBillingOutput>
{
    public override AddRepetitiveBillingOutput Map(Workspace workspace, AddRepetitiveBillingResult result)
    => new()
    {
        Id = workspace.RepetitiveBillings[result.Index].Id,
    };
}

public class AddRepetitiveBillingResult
{
    public required int Index { get; set; }
}
