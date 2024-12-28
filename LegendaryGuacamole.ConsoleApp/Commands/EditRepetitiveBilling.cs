using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class EditRepetitiveBilling : ConsoleCommand
{
    protected override string Name => "editRepetitive";

    protected override string Description => "Editer une ligne de l'échéancier";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Argument<Guid> id = new("id", "Identifiant de la ligne");
        Option<decimal?> amount = new(["--amount", "-a"], "Montant");
        Option<Frequence?> frequence = new(["--frequence", "-f"], Parsers.NullableFrequenceParser, false, "La fréquence");
        Option<bool?> saving = new(["--saving", "-s"], "La ligne est une économie");
        Option<string?> title = new(["--title", "-t"], "Titre");
        Option<ShortDate?> date = new(["--date", "-d"], Parsers.NullableShortDateParser, false, "Date");

        command.AddArgument(id);
        command.AddOption(amount);
        command.AddOption(frequence);
        command.AddOption(saving);
        command.AddOption(title);
        command.AddOption(date);

        command.SetHandler(async (id, amount, frequence, saving, title, date) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/editRepetitiveBilling",
                new EditRepetitiveBillingInput
                {
                    Id = id,
                    Amount = amount,
                    IsSaving = saving,
                    Title = title,
                    NextValuationDate = date,
                    Frequence = frequence
                });

            await response.ContinueWithAsync<EditRepetitiveBillingOutput>(output =>
            {
                Console.WriteLine("Edité");
            });
        }, id, amount, frequence, saving, title, date);

    }
}