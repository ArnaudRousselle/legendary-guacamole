using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class InsertNextBilling : WorkspaceQuery<InsertNextBillingInput, InsertNextBillingResult, InsertNextBillingOutput>
{
    public override InsertNextBillingOutput Map(Workspace workspace, InsertNextBillingResult evt)
    => new()
    {
        BillingId = workspace.Billings[evt.Index].Id
    };
}

public class InsertNextBillingResult
{
    public required int Index { get; set; }
}
