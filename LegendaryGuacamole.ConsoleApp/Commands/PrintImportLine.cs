using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class PrintImportLine : ConsoleCommand
{
    protected override string Name => "printImportLine";

    protected override string Description => "Affiche le détail d'une ligne d'import";

    protected override void InitializeCommand(Command command)
    {
        Argument<string> id = new("id", "Identifiant de la ligne d'import");

        command.SetHandler(async (id) =>
        {
            using var httpClient = GetHttpClient();

            var response = await httpClient.PostAsJsonAsync(
                "/showImportLineDetail",
                new ShowImportLineDetailInput
                {
                    ImportLineId = id
                });

            await response.ContinueWithAsync<ShowImportLineDetailOutput>(output =>
            {
                Console.WriteLine("Titre   : " + output.Title);
                Console.WriteLine("Date    : " + output.Date.ToDateOnly().ToString("dd/MM/yyyy"));
                Console.WriteLine("Montant : " + output.Amount.ToString("#######.00"));

                if (output.Candidates.Length == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pas de candidat");
                }
                else
                {
                    for (var i = 0; i < output.Candidates.Length; i++)
                    {
                        var billing = output.Candidates[i];
                        Console.WriteLine();
                        Console.WriteLine($"[{(i == output.SelectedIndex ? "*" : " ")}] Candidat {i + 1} --------------");
                        Console.WriteLine("N°      : " + billing.Id);
                        Console.WriteLine("Titre   : " + billing.Title);
                        Console.WriteLine("Date    : " + billing.ValuationDate.ToDateOnly().ToString("dd/MM/yyyy"));
                    }
                }
            });
        }, id);
    }
}