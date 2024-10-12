namespace LegendaryGuacamole.WebApi.Model;

public class RepetitiveBilling
{
    public required Guid Id { get; set; }
    public required DateOnly NextValuationDate { get; set; }
    public required string Title { get; set; }
    public required decimal Amount { get; set; }
    public required bool IsSaving { get; set; }
    public required Frequence Frequence { get; set; }
}

public enum Frequence
{
    Monthly = 1,
    Bimonthly = 2,
    Quaterly = 3,
    Annual = 4
}