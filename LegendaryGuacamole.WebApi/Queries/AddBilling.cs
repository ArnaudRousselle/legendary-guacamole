using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;

namespace LegendaryGuacamole.WebApi.AddBilling;

public class Query : WorkspaceQuery<Input, Output> { }

public class Input
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

public class Output
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