using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class DeleteBilling : WorkspaceQuery<DeleteBillingInput, DeleteBillingEvent, DeleteBillingOutput>
{
    public override DeleteBillingOutput Map(Workspace workspace, DeleteBillingEvent evt)
    => new();
}

public class DeleteBillingInput
{
    [Required]
    public Guid Id { get; set; }
}

public class DeleteBillingEvent
{
}

public class DeleteBillingOutput
{
}