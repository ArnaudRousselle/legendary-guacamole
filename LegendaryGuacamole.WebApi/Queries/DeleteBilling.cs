using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class DeleteBilling : WorkspaceQuery<DeleteBillingInput, DeleteBillingResult, DeleteBillingOutput>
{
    public override DeleteBillingOutput Map(Workspace workspace, DeleteBillingResult result)
    {
        throw new NotImplementedException();
    }
}

public class DeleteBillingInput
{
    [Required]
    public Guid Id { get; init; }
}

public class DeleteBillingResult
{
}

public class DeleteBillingOutput
{
}