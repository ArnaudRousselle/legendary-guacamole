namespace LegendaryGuacamole.WebApi.Dtos;

public record ShortDate(int Year, int Month, int Day)
{
    public DateOnly ToDateOnly() => new(Year, Month, Day);
}

public record Billing(
    Guid Id,
    ShortDate ValuationDate,
    string Title,
    decimal Amount,
    bool Checked,
    string? Comment,
    bool IsArchived,
    bool IsSaving
);