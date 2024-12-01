using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class GetBilling : WorkspaceQuery<GetBillingInput, GetBillingEvent, GetBillingOutput>
{
    public override GetBillingOutput Map(Workspace workspace, GetBillingEvent evt)
    => new()
    {
        Id = evt.Billing.Id,
        ValuationDate = new()
        {
            Year = evt.Billing.ValuationDate.Year,
            Month = evt.Billing.ValuationDate.Month,
            Day = evt.Billing.ValuationDate.Day
        },
        Title = evt.Billing.Title,
        Amount = evt.Billing.Amount,
        Checked = evt.Billing.Checked,
        Comment = evt.Billing.Comment,
        IsSaving = evt.Billing.IsSaving
    };
}

public class GetBillingInput
{
    [Required]
    public Guid Id { get; set; }
}

public class GetBillingEvent
{
    public required Billing Billing { get; set; }
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
    public required bool IsSaving { get; set; }
}