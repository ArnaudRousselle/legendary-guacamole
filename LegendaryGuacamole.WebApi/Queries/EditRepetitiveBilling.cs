using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class EditRepetitiveBilling : WorkspaceQuery<EditRepetitiveBillingInput, EditRepetitiveBillingResult, EditRepetitiveBillingOutput>
{
    public override EditRepetitiveBillingOutput Map(Workspace workspace, EditRepetitiveBillingResult result)
    => new();
}

public class EditRepetitiveBillingResult
{
}
