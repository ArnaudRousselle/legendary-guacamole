using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Dtos;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class SetChecked : WorkspaceQuery<SetCheckedInput, SetCheckedResult, SetCheckedOutput>
{
    public override SetCheckedOutput Map(Workspace workspace, SetCheckedResult result)
    {
        throw new NotImplementedException();
    }
}

public class SetCheckedInput
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public bool Checked { get; set; }
}

public class SetCheckedResult
{
    [Required]
    public required Guid Id { get; set; }
}

public class SetCheckedOutput
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public bool Checked { get; set; }
}