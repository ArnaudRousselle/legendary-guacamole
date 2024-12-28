using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class DeleteBilling : WorkspaceQuery<DeleteBillingInput, DeleteBillingResult, DeleteBillingOutput>
{
    public override DeleteBillingOutput Map(Workspace workspace, DeleteBillingResult evt)
    => new();
}

public class DeleteBillingResult
{
}
