using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class DeleteRepetitiveBilling : ConsoleCommand
{
    protected override string Name => "deleteRepetitive";

    protected override string Description => "Supprime une ligne de l'échéancier";

    protected override void InitializeCommand(Command command)
    {
        Argument<Guid> id = new("id", "Identifiant de la ligne");

        command.AddArgument(id);

        command.SetHandler(async (id) =>
        {
            using var httpClient = GetHttpClient();

            var response = await httpClient.PostAsJsonAsync(
                "/deleteRepetitiveBilling",
                new DeleteRepetitiveBillingInput
                {
                    Id = id,
                });

            await response.ContinueWithAsync<DeleteRepetitiveBillingOutput>(output =>
            {
                Console.WriteLine("Supprimé");
            });
        }, id);
    }
}