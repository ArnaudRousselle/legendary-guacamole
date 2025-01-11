using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class InsertNextBilling : WorkspaceQuery<InsertNextBillingInput, InsertNextBillingResult, InsertNextBillingOutput>
{
    public override InsertNextBillingOutput Map(Workspace workspace, InsertNextBillingResult result)
    => new()
    {
        BillingId = workspace.Billings[result.Index].Id
    };
}

public class InsertNextBillingResult
{
    public required int Index { get; set; }
}
