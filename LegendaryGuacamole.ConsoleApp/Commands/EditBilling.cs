using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class EditBilling : ConsoleCommand
{
    protected override string Name => "edit";

    protected override string Description => "Editer une ligne du compte";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Argument<Guid> id = new("id", "Identifiant de la ligne");
        Option<decimal?> amount = new(["--amount", "-a"], "Montant");
        Option<bool?> @checked = new(["--checked", "-c"], "La ligne est pointée");
        Option<string?> comment = new(["--comment"], "Commentaire");
        Option<bool?> saving = new(["--saving", "-s"], "La ligne est une économie");
        Option<string?> title = new(["--title", "-t"], "Titre");
        Option<ShortDate?> date = new(["--date", "-d"], Parsers.NullableShortDateParser, false, "Date");

        command.AddArgument(id);
        command.AddOption(amount);
        command.AddOption(@checked);
        command.AddOption(comment);
        command.AddOption(saving);
        command.AddOption(title);
        command.AddOption(date);

        command.SetHandler(async (id, amount, @checked, comment, saving, title, date) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/editBilling",
                new EditBillingInput
                {
                    Id = id,
                    Amount = amount,
                    Checked = @checked,
                    Comment = comment,
                    IsSaving = saving,
                    Title = title,
                    ValuationDate = date
                });

            await response.ContinueWithAsync<EditBillingOutput>(output =>
            {
                Console.WriteLine("Edité : " + output.Id);
            });
        }, id, amount, @checked, comment, saving, title, date);

    }
}