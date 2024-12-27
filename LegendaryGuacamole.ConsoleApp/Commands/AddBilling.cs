using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class AddBilling : ConsoleCommand
{
    protected override string Name => "add";

    protected override string Description => "Ajouter une ligne au compte";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Option<decimal> amount = new(["--amount", "-a"], "Montant")
        {
            IsRequired = true
        };

        Option<bool?> @checked = new(["--checked", "-c"], "La ligne est pointée");

        Option<string?> comment = new(["--comment"], "Commentaire");

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
        command.AddOption(@checked);
        command.AddOption(comment);
        command.AddOption(saving);
        command.AddOption(title);
        command.AddOption(date);

        command.SetHandler(async (amount, @checked, comment, saving, title, date) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/addBilling",
                new AddBillingInput
                {
                    Amount = amount,
                    Checked = @checked ?? false,
                    Comment = comment ?? "",
                    IsSaving = saving ?? false,
                    Title = title,
                    ValuationDate = date
                });

            await response.ContinueWithAsync<AddBillingOutput>(output =>
            {
                Console.WriteLine("Ajouté : " + output.Id);
            });
        }, amount, @checked, comment, saving, title, date);

    }
}