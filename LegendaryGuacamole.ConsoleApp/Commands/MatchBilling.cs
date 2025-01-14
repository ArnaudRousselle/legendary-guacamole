using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class MatchBilling : ConsoleCommand
{
    protected override string Name => "match";

    protected override string Description => "Lie une ligne d'import à une ligne du compte";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Argument<string> id = new("id", "Identifiant de la ligne d'import");
        Option<Guid?> billingId = new(["--billing", "-b"], "Identifiant de la ligne du compte");

        command.SetHandler(async (id, billingId) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/matchBilling",
                new MatchBillingInput
                {
                    BillingId = billingId,
                    ImportLineId = id
                });

            await response.ContinueWithAsync<MatchBillingOutput>(output =>
            {
                Console.WriteLine($"liaison effectuée");
            });
        }, id, billingId);
    }
}