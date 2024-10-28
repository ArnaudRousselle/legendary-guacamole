namespace LegendaryGuacamole.WebApi.Models;

public record Billing
{
    public Guid Id { get; internal set; }
    public DateOnly ValuationDate { get; internal set; }
    public string Title { get; internal set; } = "";
    public decimal Amount { get; internal set; }
    public bool Checked { get; internal set; }
    public string? Comment { get; internal set; }
    public bool IsArchived { get; internal set; }
    public bool IsSaving { get; internal set; }
}