namespace LegendaryGuacamole.WebApi.Models;

public record Billing
{
    public required Guid Id { get; init; }
    public required DateOnly ValuationDate { get; init; }
    public required string Title { get; init; }
    public required decimal Amount { get; init; }
    public required bool Checked { get; init; }
    public required string? Comment { get; init; }
    public required bool IsSaving { get; init; }
}