using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class PrintBalance : ConsoleCommand
{
    protected override string Name => "balance";

    protected override string Description => "Afficher le solde total pointé du compte";

    protected override void InitializeCommand(Command command)
    {
        command.SetHandler(async (id) =>
        {
            using var httpClient = GetHttpClient();

            var response = await httpClient.PostAsJsonAsync(
                "/getSummary",
                new GetSummaryInput());

            await response.ContinueWithAsync<GetSummaryOutput>(output =>
            {
                Console.WriteLine("Total pointé : " + output.Amount.ToString("############.00"));
            });
        });

    }
}