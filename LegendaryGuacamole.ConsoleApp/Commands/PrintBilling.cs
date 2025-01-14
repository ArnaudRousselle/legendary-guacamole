using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class PrintBilling : ConsoleCommand
{
    protected override string Name => "print";

    protected override string Description => "Afficher une ligne du compte";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Argument<Guid> id = new("id", "Identifiant de la ligne");

        command.AddArgument(id);

        command.SetHandler(async (id) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/getBilling",
                new GetBillingInput
                {
                    Id = id,
                });

            await response.ContinueWithAsync<GetBillingOutput>(output =>
            {
                Console.WriteLine("Titre       : " + output.Title);
                Console.WriteLine("Date        : " + output.ValuationDate.ToDateOnly().ToString("dd/MM/yyyy"));
                Console.WriteLine("Montant     : " + output.Amount.ToString("#######.00"));
                Console.WriteLine("Pointée     : " + (output.Checked ? "Oui" : "Non"));
                Console.WriteLine("Economies   : " + (output.IsSaving ? "Oui" : "Non"));
                Console.WriteLine("Commentaire : " + output.Comment);
            });
        }, id);

    }
}