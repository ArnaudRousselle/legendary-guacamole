using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class ShowProjection : ConsoleCommand
{
    protected override string Name => "projection";

    protected override string Description => "Affiche les valeurs futures";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Option<int?> pageSize = new(["--pageSize", "-p"], "nombre d'éléments par page");
        command.AddOption(pageSize);

        command.SetHandler(async (pageSize) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/showProjection",
                new ShowProjectionInput());

            await response.ContinueWithAsync<ShowProjectionOutput>(output =>
            {
                output.Items.ToPage(pageSize ?? 20, items =>
                {
                    Console.WriteLine($"| {"Date".FillRight(10)} | {"Montant".FillRight(10)} |");
                    items.ForEach(l =>
                    {
                        Console.WriteLine($"| {l.ValuationDate.ToDateOnly():dd/MM/yyyy} | {l.Amount.ToString("#######.00").FillLeft(10)} ||");
                        Console.ResetColor();
                    });
                });
            });
        }, pageSize);
    }
}