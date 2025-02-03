using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class CommitImport : ConsoleCommand
{
    protected override string Name => "commit";

    protected override string Description => "Valide l'import en cours";

    protected override void InitializeCommand(Command command)
    {
        command.SetHandler(async () =>
        {
            using var httpClient = GetHttpClient();

            var response = await httpClient.PostAsJsonAsync(
                "/commitImport",
                new CommitImportInput());

            await response.ContinueWithAsync<CommitImportOutput>(output =>
            {
                var count = output.BillingsIds.Length;
                var plural = count >= 2 ? "s" : "";
                Console.WriteLine($"{count} ligne{plural} crée{plural}");
            });
        });
    }
}