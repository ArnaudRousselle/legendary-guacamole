using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class EditBilling : WorkspaceQuery<EditBillingInput, EditBillingResult, EditBillingOutput>
{
    public override EditBillingOutput Map(Workspace workspace, EditBillingResult result)
    => new();
}

public class EditBillingResult
{
}
