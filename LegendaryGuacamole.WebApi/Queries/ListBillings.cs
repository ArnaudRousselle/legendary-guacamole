using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class ListBillings : WorkspaceQuery<ListBillingsInput, ListBillingsEvent, ListBillingsOutput[]>
{
    public override ListBillingsOutput[] Map(Workspace workspace, ListBillingsEvent evt)
    => workspace.Billings
        .Where(b =>
        {
            if (!(Input.WithChecked ?? false) && b.Checked)
                return false;
            if (Input.StartDate != null && b.ValuationDate < new DateOnly(Input.StartDate.Year, Input.StartDate.Month, Input.StartDate.Day))
                return false;
            if (Input.EndDate != null && b.ValuationDate > new DateOnly(Input.EndDate.Year, Input.EndDate.Month, Input.EndDate.Day))
                return false;
            if (Input.Amount.HasValue && (Math.Abs(b.Amount - Input.Amount.Value) >= (Input.DeltaAmount ?? 0) + 0.001m))
                return false;
            if (!string.IsNullOrEmpty(Input.Title) && b.Title.IndexOf(Input.Title, StringComparison.CurrentCultureIgnoreCase) < 0)
                return false;
            return true;
        })
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
    public bool? WithChecked { get; set; }
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
    public required bool IsSaving { get; set; }
}