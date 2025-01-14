using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class PrintRepetitiveBilling : ConsoleCommand
{
    protected override string Name => "printRepetitive";

    protected override string Description => "Afficher une ligne de l'échéancier";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Argument<Guid> id = new("id", "Identifiant de la ligne");

        command.AddArgument(id);

        command.SetHandler(async (id) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/getRepetitiveBilling",
                new GetRepetitiveBillingInput
                {
                    Id = id,
                });

            await response.ContinueWithAsync<GetRepetitiveBillingOutput>(output =>
            {
                Console.WriteLine("Titre     : " + output.Title);
                Console.WriteLine("Montant   : " + output.Amount.ToString("#######.00"));
                Console.WriteLine("Economies : " + (output.IsSaving ? "Oui" : "Non"));
                Console.WriteLine("Pro. Date : " + output.NextValuationDate.ToDateOnly().ToString("dd/MM/yyyy"));
                Console.WriteLine("Fréquence : " + (output.Frequence == Frequence.Monthly ? "Mensuel" : output.Frequence == Frequence.Bimonthly ? "Bimestriel" : output.Frequence == Frequence.Quaterly ? "Trimestriel" : output.Frequence == Frequence.Annual ? "Annuel" : "??"));
            });
        }, id);

    }
}