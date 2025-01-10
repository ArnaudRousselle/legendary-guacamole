using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class GetSummary : WorkspaceQuery<GetSummaryInput, GetSummaryResult, GetSummaryOutput>
{
    public override GetSummaryOutput Map(Workspace workspace, GetSummaryResult evt)
    => new()
    {
        Amount = workspace.Billings
            .Where(b => b.Checked)
            .Select(b => b.Amount)
            .Sum()
    };
}

public class GetSummaryResult
{
}
