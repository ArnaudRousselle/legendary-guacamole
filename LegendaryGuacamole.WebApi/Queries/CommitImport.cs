using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class CommitImport : WorkspaceQuery<CommitImportInput, CommitImportResult, CommitImportOutput>
{
    public override CommitImportOutput Map(Workspace workspace, CommitImportResult result)
    => new()
    {
        BillingsIds = result.Indexes
            .Select(i => workspace.Billings[i].Id)
            .ToArray()
    };
}

public class CommitImportResult
{
    public required int[] Indexes { get; set; }
}