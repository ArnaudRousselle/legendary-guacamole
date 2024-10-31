using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class GetSummary : WorkspaceQuery<GetSummaryInput, GetSummaryEvent, GetSummaryOutput>
{
    public override GetSummaryOutput Map(Workspace workspace, GetSummaryEvent evt)
    => new()
    {
        Amount = workspace.Billings
            .Where(b => b.Checked)
            .Select(b => b.Amount)
            .Sum()
    };
}

public class GetSummaryInput
{
}

public class GetSummaryEvent
{
}

public class GetSummaryOutput
{
    [Required]
    public required decimal Amount { get; set; }
}

