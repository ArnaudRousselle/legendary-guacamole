using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class AddBilling : WorkspaceQuery<AddBillingInput, AddBillingEvent, AddBillingOutput>
{
    public override AddBillingOutput Map(Workspace workspace, AddBillingEvent evt)
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
        IsArchived = evt.Billing.IsArchived,
        IsSaving = evt.Billing.IsSaving
    };
}

public class AddBillingInput
{
    [Required]
    public ShortDate ValuationDate { get; set; } = new();
    [Required]
    public string Title { get; set; } = "";
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public bool Checked { get; set; }
    public string? Comment { get; set; }
    [Required]
    public bool IsArchived { get; set; }
    [Required]
    public bool IsSaving { get; set; }
}

public class AddBillingEvent
{
    public required Billing Billing { get; set; }
}

public class AddBillingOutput
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