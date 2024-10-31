using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class ListBillings : WorkspaceQuery<ListBillingsInput, ListBillingsEvent, ListBillingsOutput[]>
{
    public override ListBillingsOutput[] Map(Workspace workspace, ListBillingsEvent evt)
    => workspace.Billings
        .OrderBy(n => n.ValuationDate)
        .ThenBy(n => n.Id)
        .Select(n => new ListBillingsOutput
        {
            Id = n.Id,
            ValuationDate = new()
            {
                Year = n.ValuationDate.Year,
                Month = n.ValuationDate.Month,
                Day = n.ValuationDate.Day
            },
            Title = n.Title,
            Amount = n.Amount,
            Checked = n.Checked,
            Comment = n.Comment,
            IsArchived = n.IsArchived,
            IsSaving = n.IsSaving
        })
        .ToArray();
}

public class ListBillingsInput
{
    public ShortDate? StartDate { get; set; }
    public ShortDate? EndDate { get; set; }
    public decimal? Amount { get; set; }
    public decimal? DeltaAmount { get; set; }
    public string? Title { get; set; }
    public bool? WithArchived { get; set; }
}

public class ListBillingsEvent
{
}

public class ListBillingsOutput
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