using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;

namespace LegendaryGuacamole.WebApi.DeleteBilling;

public class Query : WorkspaceQuery<Input, Output> { }

public class Input
{
    [Required]
    public Guid Id { get; init; }
}

public class Output
{
    [Required]
    public required bool HasBeenDeleted { get; init; }
}