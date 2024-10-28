using System.Collections.Immutable;

namespace LegendaryGuacamole.WebApi.Models;

public record Workspace
{
    public ImmutableArray<Billing> Billings { get; set; } = [];
    public ImmutableArray<RepetitiveBilling> RepetitiveBillings { get; set; } = [];
}