using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class MultipleInsertNextBilling : ConsoleCommand
{
    protected override string Name => "nextAuto";

    protected override string Description => "Insérer une ligne de compte à partir de l'échéancier";

    protected override void InitializeCommand(Command command)
    {
        Argument<ShortDate> maxDate = new("maxDate", Parsers.ShortDateParser, false, "Date");

        command.AddArgument(maxDate);

        command.SetHandler(async (maxDate) =>
        {
            using var httpClient = GetHttpClient();

            var response = await httpClient.PostAsJsonAsync(
                "/multipleInsertNextBilling",
                new MultipleInsertNextBillingInput
                {
                    MaxDate = maxDate
                });

            await response.ContinueWithAsync<MultipleInsertNextBillingOutput>(output =>
            {
                if (output.BillingIds.Length == 0)
                    Console.WriteLine("Aucune ligne insérée");
                else
                {
                    var plural = output.BillingIds.Length >= 2 ? "s" : "";
                    Console.WriteLine(output.BillingIds.Length + $" ligne{plural} insérée{plural} : ");
                    foreach (var id in output.BillingIds)
                        Console.WriteLine(id);
                }
            });
        }, maxDate);

    }
}