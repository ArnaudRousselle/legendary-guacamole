using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;

namespace LegendaryGuacamole.WebApi.SetChecked;

public class Query : WorkspaceQuery<Input, Output> { }

public class Input
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public bool Checked { get; set; }
}

public class Output
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public bool Checked { get; set; }
}