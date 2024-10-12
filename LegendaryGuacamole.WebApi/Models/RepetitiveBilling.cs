namespace LegendaryGuacamole.WebApi.Models;

public class RepetitiveBilling
{
    public Guid Id { get; internal set; }
    public DateOnly NextValuationDate { get; internal set; }
    public string Title { get; internal set; } = "";
    public decimal Amount { get; internal set; }
    public bool IsSaving { get; internal set; }
    public Frequence Frequence { get; internal set; }
}

public enum Frequence
{
    Monthly = 1,
    Bimonthly = 2,
    Quaterly = 3,
    Annual = 4
}