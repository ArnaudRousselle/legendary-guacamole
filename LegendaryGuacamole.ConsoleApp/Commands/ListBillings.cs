
using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class ListBillings : ConsoleCommand
{
    protected override string Name => "list";
    protected override string Description => "Affiche l'ensemble des lignes du compte";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Option<int?> pageSize = new(["--pageSize", "-p"], "nombre d'éléments par page");
        Option<decimal?> amount = new(["--amount", "-a"], "recherche sur le montant");
        Option<decimal?> deltaAmount = new(["--delta", "-d"], "applique un delta sur le montant");
        Option<ShortDate?> endDate = new(["--end", "-e"], Parsers.NullableShortDateParser, false, "recherche sur la date (borne max)");
        Option<ShortDate?> startDate = new(["--start", "-s"], Parsers.NullableShortDateParser, false, "recherche sur la date (borne min)");
        Option<string?> title = new(["--title", "-t"], "recherche sur le titre");
        Option<bool?> withChecked = new(["--withChecked", "-c"], "inclut les lignes pointées");

        command.AddOption(pageSize);
        command.AddOption(amount);
        command.AddOption(deltaAmount);
        command.AddOption(endDate);
        command.AddOption(startDate);
        command.AddOption(title);
        command.AddOption(withChecked);

        command.SetHandler(async (pageSize, amount, deltaAmount, endDate, startDate, title, withChecked) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/listBillings",
                new ListBillingsInput
                {
                    Amount = amount,
                    DeltaAmount = deltaAmount,
                    EndDate = endDate,
                    StartDate = startDate,
                    Title = title,
                    WithChecked = withChecked ?? false
                });

            await response.ContinueWithAsync<ListBillingsOutput>(output =>
            {
                output.Items.ToPage(pageSize ?? 20, items =>
                {
                    Console.WriteLine($"| {"N°".FillRight(36)} | {"Date".FillRight(10)} | {"Titre".FillRight(30)} | {"Montant".FillRight(10)} | Eco | Com |");
                    items.ForEach(l =>
                    {
                        if (l.Checked)
                            Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"| {l.Id} | {l.ValuationDate.ToDateOnly():dd/MM/yyyy} | {l.Title.FillRight(30)} | {l.Amount.ToString("#######.00").FillLeft(10)} |  {(l.IsSaving ? "x" : " ")}  |  {(!string.IsNullOrWhiteSpace(l.Comment) ? "x" : " ")}  |");
                        Console.ResetColor();
                    });
                });
            });
        }, pageSize, amount, deltaAmount, endDate, startDate, title, withChecked);
    }
}