

using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class InsertNextBilling : WorkspaceQuery<InsertNextBillingInput, InsertNextBillingEvent, InsertNextBillingOutput>
{
    public override InsertNextBillingOutput Map(Workspace workspace, InsertNextBillingEvent evt)
    => new()
    {
        Billing = new()
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
        },
        RepetitiveBilling = new()
        {
            Id = evt.RepetitiveBilling.Id,
            NextValuationDate = new()
            {
                Year = evt.RepetitiveBilling.NextValuationDate.Year,
                Month = evt.RepetitiveBilling.NextValuationDate.Month,
                Day = evt.RepetitiveBilling.NextValuationDate.Day
            },
            Title = evt.RepetitiveBilling.Title,
            Amount = evt.RepetitiveBilling.Amount,
            IsSaving = evt.RepetitiveBilling.IsSaving,
            Frequence = evt.RepetitiveBilling.Frequence
        }
    };
}

public class InsertNextBillingInput
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public bool IncrementNextValuationDate { get; set; }
}

public class InsertNextBillingEvent
{
    public required Billing Billing { get; set; }
    public required RepetitiveBilling RepetitiveBilling { get; set; }
}

public class InsertNextBillingOutput
{
    [Required]
    public required InsertNextBillingOutputBilling Billing { get; set; }
    [Required]
    public required InsertNextBillingOutputRepetitiveBilling RepetitiveBilling { get; set; }
}

public class InsertNextBillingOutputBilling
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

public class InsertNextBillingOutputRepetitiveBilling
{
    [Required]
    public required Guid Id { get; set; }
    [Required]
    public required ShortDate NextValuationDate { get; set; } = new();
    [Required]
    public required string Title { get; set; }
    [Required]
    public required decimal Amount { get; set; }
    [Required]
    public required bool IsSaving { get; set; }
    [Required]
    public required Frequence Frequence { get; set; }
}