using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class EditBilling : WorkspaceQuery<EditBillingInput, EditBillingEvent, EditBillingOutput>
{
    public override EditBillingOutput Map(Workspace workspace, EditBillingEvent evt)
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

public class EditBillingInput
{
    [Required]
    public Guid Id { get; set; }
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
    public bool IsSaving { get; set; }
}

public class EditBillingEvent
{
    public required Billing Billing { get; set; }
}

public class EditBillingOutput
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