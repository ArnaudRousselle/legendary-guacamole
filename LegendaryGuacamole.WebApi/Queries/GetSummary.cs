using System.ComponentModel.DataAnnotations;
using LegendaryGuacamole.WebApi.Channels;

namespace LegendaryGuacamole.WebApi.GetSummary;

public class Query : WorkspaceQuery<Input, Output[], Result>
{
    public override Result Map(Output[] output)
        => new()
        {
            Amount = output
                .OrderBy(n => n.ValuationDate)
                .Select(n => n.Amount)
                .Sum()
        };
}

public class Input
{
}

public class Output
{
    [Required]
    public required DateOnly ValuationDate { get; set; }
    [Required]
    public required decimal Amount { get; set; }
}

public class Result
{
    [Required]
    public required decimal Amount { get; set; }
}