using System.CommandLine;
using System.Net.Http.Json;
using LegendaryGuacamole.ConsoleApp.Extensions;
using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class ImportFile : ConsoleCommand
{
    protected override string Name => "import";

    protected override string Description => "Importe un fichier ofx";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Argument<FileInfo> file = new("file", Parsers.FileInfoParser, false, "le fichier à importer");

        Option<int?> pageSize = new(["--pageSize", "-p"], "nombre d'éléments par page");

        command.AddOption(pageSize);
        command.AddArgument(file);

        command.SetHandler(async (file, pageSize) =>
        {
            var response = await httpClient.PostAsJsonAsync(
                "/importFile",
                new ImportFileInput
                {
                    FilePath = file.FullName,
                });

            await response.ContinueWithAsync<ImportFileOutput>(output =>
            {
                var maxIdLength = output.Tuples
                    .Select(n => n.ImportLine.Id.Length)
                    .Max();

                output.Tuples.ToPage(pageSize ?? 20, items =>
                {
                    Console.WriteLine($"| {"Ident.".FillRight(maxIdLength)} | {"Date".FillRight(10)} | {"Titre".FillRight(30)} | {"Montant".FillRight(9)} | {"Liaison".FillRight(30)} | Nb  |");
                    items.ForEach(l =>
                    {
                        Console.WriteLine($"| {l.ImportLine.Id.FillRight(maxIdLength)} | {l.ImportLine.ValuationDate.ToDateOnly():dd/MM/yyyy} | {l.ImportLine.Title.FillRight(30)} | {l.ImportLine.Amount.ToString("######.00").FillLeft(9)} | {(l.CurrentBilling != null ? l.CurrentBilling.Title.FillRight(30) : " ".FillRight(30))} | {l.MatchingCount.ToString().FillLeft(3)} |");
                        Console.ResetColor();
                    });
                });
            });
        }, file, pageSize);
    }
}