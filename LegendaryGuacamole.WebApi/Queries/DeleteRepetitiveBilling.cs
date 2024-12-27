

using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class DeleteRepetitiveBilling : WorkspaceQuery<DeleteRepetitiveBillingInput, DeleteRepetitiveBillingEvent, DeleteRepetitiveBillingOutput>
{
    public override DeleteRepetitiveBillingOutput Map(Workspace workspace, DeleteRepetitiveBillingEvent evt)
        => new();
}

public class DeleteRepetitiveBillingEvent
{
}
