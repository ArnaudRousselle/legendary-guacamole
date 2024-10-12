namespace LegendaryGuacamole.WebApi.Models;

public class Workspace
{
    public Billing[] Billings { get; set; } = [];
    public RepetitiveBilling[] RepetitiveBillings { get; set; } = [];
}