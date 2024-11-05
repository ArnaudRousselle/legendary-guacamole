using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class GetRepetitiveBilling : WorkspaceQuery<GetRepetitiveBillingInput, GetRepetitiveBillingEvent, GetRepetitiveBillingOutput>
{
    public override GetRepetitiveBillingOutput Map(Workspace workspace, GetRepetitiveBillingEvent evt)
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

public class GetRepetitiveBillingInput
{
    [Required]
    public Guid Id { get; set; }
}

public class GetRepetitiveBillingEvent
{
    public required RepetitiveBilling RepetitiveBilling { get; set; }
}

public class GetRepetitiveBillingOutput
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