namespace LegendaryGuacamole.WebApi.Model;

public class Billing
{
    public required Guid Id { get; set; }
    public required DateOnly ValuationDate { get; set; }
    public required string Title { get; set; }
    public required decimal Amount { get; set; }
    public required bool Checked { get; set; }
    public required string? Comment { get; set; }
    public required bool IsArchived { get; set; }
    public required bool IsSaving { get; set; }

    public Dtos.Billing ToDtos()
    {
        return new Dtos.Billing(Id,
            new(ValuationDate.Year, ValuationDate.Month, ValuationDate.Day),
            Title,
            Amount,
            Checked,
            Comment,
            IsArchived,
            IsSaving);
    }
}