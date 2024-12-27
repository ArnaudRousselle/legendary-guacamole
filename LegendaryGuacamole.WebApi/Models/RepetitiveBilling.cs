using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.WebApi.Models;

public record RepetitiveBilling
{
    public required Guid Id { get; init; }
    public required DateOnly NextValuationDate { get; init; }
    public required string Title { get; init; }
    public required decimal Amount { get; init; }
    public required bool IsSaving { get; init; }
    public required Frequence Frequence { get; init; }
}
