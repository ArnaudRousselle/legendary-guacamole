namespace LegendaryGuacamole.WebApi.Models;

public class Import
{
    public required Line[] Lines { get; init; }
}

public class Line
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required decimal Amount { get; init; }
    public required DateOnly Date { get; init; }
    public required Guid[] Matchings { get; init; }
    public int SelectedIndex { get; set; } = -1;
}