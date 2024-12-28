using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class AddRepetitiveBilling : ConsoleCommand
{
    protected override string Name => "addRepetitive";

    protected override string Description => "Ajouter une ligne à l'échéancier";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Option<decimal> amount = new(["--amount", "-a"], "Montant")
        {
            IsRequired = true
        };

        Option<Frequence> frequence = new(["--frequence", "-f"], Parsers.FrequenceParser, false, "La fréquence");

        Option<bool?> saving = new(["--saving", "-s"], "La ligne est une économie");

        Option<string> title = new(["--title", "-t"], "Titre")
        {
            IsRequired = true
        };

        Option<ShortDate> date = new(["--date", "-d"], Parsers.ShortDateParser, false, "Date")
        {
            IsRequired = true
        };

        command.AddOption(amount);
        command.AddOption(frequence);
        command.AddOption(saving);
        command.AddOption(title);
        command.AddOption(date);

        command.SetHandler(async (amount, frequence, saving, title, date) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/addRepetitiveBilling",
                new AddRepetitiveBillingInput
                {
                    Amount = amount,
                    IsSaving = saving ?? false,
                    Title = title,
                    NextValuationDate = date,
                    Frequence = frequence
                });

            await response.ContinueWithAsync<AddRepetitiveBillingOutput>(output =>
            {
                Console.WriteLine("Ajouté : " + output.Id);
            });
        }, amount, frequence, saving, title, date);

    }
}