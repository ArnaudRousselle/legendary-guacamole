using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class EditRepetitiveBilling : WorkspaceQuery<EditRepetitiveBillingInput, EditRepetitiveBillingEvent, EditRepetitiveBillingOutput>
{
    public override EditRepetitiveBillingOutput Map(Workspace workspace, EditRepetitiveBillingEvent evt)
    => new()
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
    };
}

public class EditRepetitiveBillingInput
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public required ShortDate NextValuationDate { get; set; } = new();
    [Required]
    public string Title { get; set; } = "";
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public bool IsSaving { get; set; }
    [Required]
    public Frequence Frequence { get; set; }
}

public class EditRepetitiveBillingEvent
{
    public required RepetitiveBilling RepetitiveBilling { get; set; }
}

public class EditRepetitiveBillingOutput
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