using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class SetChecked : WorkspaceQuery<SetCheckedInput, SetCheckedEvent, SetCheckedOutput>
{
    public override SetCheckedOutput Map(Workspace workspace, SetCheckedEvent evt)
    => new()
    {
        Id = evt.Id,
        Checked = evt.Checked
    };
}

public class SetCheckedInput
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public bool Checked { get; set; }
}

public class SetCheckedEvent
{
    public required Guid Id { get; set; }
    public required bool Checked { get; set; }
}

public class SetCheckedOutput
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public bool Checked { get; set; }
}