using System.Collections.Immutable;

namespace LegendaryGuacamole.WebApi.Models;

public record Workspace
{
    public required ImmutableArray<Billing> Billings { get; init; }
    public required ImmutableArray<RepetitiveBilling> RepetitiveBillings { get; init; }
}