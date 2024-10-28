using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class GetSummary : WorkspaceQuery<GetSummaryInput, GetSummaryResult, GetSummaryOutput>
{
    public override GetSummaryOutput Map(Workspace workspace, GetSummaryResult result)
    {
        throw new NotImplementedException();
    }
}

public class GetSummaryInput
{
}

public class GetSummaryResult
{
}

public class GetSummaryOutput
{
    [Required]
    public required decimal Amount { get; set; }
}

