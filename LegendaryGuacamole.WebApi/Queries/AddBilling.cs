using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class AddBilling : WorkspaceQuery<AddBillingInput, AddBillingResult, AddBillingOutput>
{
    public override AddBillingOutput Map(Workspace workspace, AddBillingResult evt)
    => new()
    {
        Id = workspace.Billings[evt.Index].Id,
    };
}

public class AddBillingResult
{
    public required int Index { get; set; }
}
