
using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class ListRepetitiveBillings : ConsoleCommand
{
    protected override string Name => "listRepetitive";
    protected override string Description => "Affiche l'ensemble des lignes de l'échéancier";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Option<int?> pageSize = new(["--pageSize", "-p"], "nombre d'éléments par page");
        command.AddOption(pageSize);

        command.SetHandler(async (pageSize) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/listRepetitiveBillings",
                new ListRepetitiveBillingsInput());

            await response.ContinueWithAsync<ListRepetitiveBillingsOutput>(output =>
            {
                output.Items.ToPage(pageSize ?? 20, items =>
                {
                    Console.WriteLine($"| {"N°".FillRight(36)} | {"Date".FillRight(10)} | {"Titre".FillRight(30)} | {"Montant".FillRight(9)} | Eco | Freq |");
                    items.ForEach(l =>
                    {
                        Console.WriteLine($"| {l.Id} | {l.NextValuationDate.ToDateOnly():dd/MM/yyyy} | {l.Title.FillRight(30)} | {l.Amount.ToString("######.00").FillLeft(9)} |  {(l.IsSaving ? "x" : " ")}  |  {(l.Frequence == Frequence.Monthly ? "1m" : l.Frequence == Frequence.Bimonthly ? "2m" : l.Frequence == Frequence.Quaterly ? "3m" : l.Frequence == Frequence.Annual ? "1y" : "??")}  |");
                        Console.ResetColor();
                    });
                });
            });
        }, pageSize);
    }
}