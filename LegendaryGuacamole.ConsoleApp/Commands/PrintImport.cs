using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class PrintImport : ConsoleCommand
{
    protected override string Name => "printImport";

    protected override string Description => "Affiche l'import en cours";

    protected override void InitializeCommand(Command command)
    {
        Option<int?> pageSize = new(["--pageSize", "-p"], "nombre d'éléments par page");

        command.AddOption(pageSize);

        command.SetHandler(async (pageSize) =>
        {
            using var httpClient = GetHttpClient();

            var response = await httpClient.PostAsJsonAsync(
                "/showImport",
                new ShowImportInput());

            await response.ContinueWithAsync<ShowImportOutput>(output =>
            {
                var maxIdLength = output.Tuples
                    .Select(n => n.ImportLine.Id.Length)
                    .Max();

                output.Tuples.ToPage(pageSize ?? 20, items =>
                {
                    Console.WriteLine($"| {"Ident.".FillRight(maxIdLength)} | {"Date".FillRight(10)} | {"Titre".FillRight(30)} | {"Montant".FillRight(10)} | {"Liaison".FillRight(30)} | Nb  |");
                    items.ForEach(l =>
                    {
                        Console.WriteLine($"| {l.ImportLine.Id.FillRight(maxIdLength)} | {l.ImportLine.ValuationDate.ToDateOnly():dd/MM/yyyy} | {l.ImportLine.Title.FillRight(30)} | {l.ImportLine.Amount.ToString("#######.00").FillLeft(10)} | {(l.CurrentBilling != null ? l.CurrentBilling.Title.FillRight(30) : " ".FillRight(30))} | {l.MatchingCount.ToString().FillLeft(3)} |");
                        Console.ResetColor();
                    });
                });
            });
        }, pageSize);
    }
}