namespace LegendaryGuacamole.WebApi.Model;

public class Workspace
{
    public List<Billing> Billings { get; set; } = [];
    public List<RepetitiveBilling> RepetitiveBillings { get; set; } = [];
}