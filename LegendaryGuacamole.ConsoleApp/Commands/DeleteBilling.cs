using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class DeleteBilling : ConsoleCommand
{
    protected override string Name => "delete";

    protected override string Description => "Supprime une ligne du compte";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Argument<Guid> id = new("id", "Identifiant de la ligne");

        command.AddArgument(id);

        command.SetHandler(async (id) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/deleteBilling",
                new DeleteBillingInput
                {
                    Id = id,
                });

            await response.ContinueWithAsync<DeleteBillingOutput>(output =>
            {
                Console.WriteLine("Supprimé");
            });
        }, id);
    }
}