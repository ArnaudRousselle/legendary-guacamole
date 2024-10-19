using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;

namespace LegendaryGuacamole.WebApi.EditBilling;

public class Query : WorkspaceQuery<Input, bool> { }

public class Input
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
    public bool IsArchived { get; set; }
    [Required]
    public bool IsSaving { get; set; }
}