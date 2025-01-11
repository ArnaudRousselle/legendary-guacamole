using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class MatchBilling : WorkspaceQuery<MatchBillingInput, MatchBillingResult, MatchBillingOutput>
{
    public override MatchBillingOutput Map(Workspace workspace, MatchBillingResult result)
    => new();
}

public class MatchBillingResult
{

}