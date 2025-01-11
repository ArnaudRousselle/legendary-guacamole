using System.Collections.Immutable;

namespace LegendaryGuacamole.WebApi.Models;

public record Import
{
    public required ImmutableArray<Line> Lines { get; init; }
}

public record Line
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required decimal Amount { get; init; }
    public required DateOnly Date { get; init; }
    public required ImmutableArray<Guid> Matchings { get; init; }
    public required int SelectedIndex { get; init; }
}