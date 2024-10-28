using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class GetBilling : WorkspaceQuery<GetBillingInput, GetBillingResult, GetBillingOutput>
{
    public override GetBillingOutput Map(Workspace workspace, GetBillingResult result)
    {
        throw new NotImplementedException();
    }
}

public class GetBillingInput
{
    [Required]
    public Guid Id { get; init; }
}

public class GetBillingResult
{
}

public class GetBillingOutput
{
    [Required]
    public required Guid Id { get; set; }
    [Required]
    public required ShortDate ValuationDate { get; set; } = new();
    [Required]
    public required string Title { get; set; } = "";
    [Required]
    public required decimal Amount { get; set; }
    [Required]
    public required bool Checked { get; set; }
    public required string? Comment { get; set; }
    [Required]
    public required bool IsArchived { get; set; }
    [Required]
    public required bool IsSaving { get; set; }
}