using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class DeleteBilling : WorkspaceQuery<DeleteBillingInput, DeleteBillingEvent, DeleteBillingOutput>
{
    public override DeleteBillingOutput Map(Workspace workspace, DeleteBillingEvent evt)
    => new();
}

public class DeleteBillingEvent
{
}
