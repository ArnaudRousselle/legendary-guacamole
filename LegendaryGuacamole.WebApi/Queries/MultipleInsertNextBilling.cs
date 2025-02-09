using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class MultipleInsertNextBilling : WorkspaceQuery<MultipleInsertNextBillingInput, MultipleInsertNextBillingResult, MultipleInsertNextBillingOutput>
{
    public override MultipleInsertNextBillingOutput Map(Workspace workspace, MultipleInsertNextBillingResult result)
    => new()
    {
        BillingIds = workspace.Billings
            .Where((_, i) => result.Indexes.Contains(i))
            .Select(b => b.Id)
            .ToArray()
    };
}

public class MultipleInsertNextBillingResult
{
    public required int[] Indexes { get; set; }
}
