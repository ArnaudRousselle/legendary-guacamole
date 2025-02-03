using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class InsertNextBilling : ConsoleCommand
{
    protected override string Name => "next";

    protected override string Description => "Insérer une ligne de compte à partir de l'échéancier";

    protected override void InitializeCommand(Command command)
    {
        Argument<Guid> id = new("id", "Identifiant de la ligne");

        command.AddArgument(id);

        command.SetHandler(async (id) =>
        {
            using var httpClient = GetHttpClient();

            var response = await httpClient.PostAsJsonAsync(
                "/insertNextBilling",
                new InsertNextBillingInput
                {
                    RepetitiveBillingId = id
                });

            await response.ContinueWithAsync<InsertNextBillingOutput>(output =>
            {
                Console.WriteLine("Ligne insérée : " + output.BillingId);
            });
        }, id);

    }
}