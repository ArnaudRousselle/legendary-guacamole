using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class DeleteRepetitiveBilling : WorkspaceQuery<DeleteRepetitiveBillingInput, DeleteRepetitiveBillingResult, DeleteRepetitiveBillingOutput>
{
    public override DeleteRepetitiveBillingOutput Map(Workspace workspace, DeleteRepetitiveBillingResult result)
    => new();
}

public class DeleteRepetitiveBillingResult
{
}
