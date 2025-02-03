using System.CommandLine;
using LegendaryGuacamole.ConsoleApp.Commands;

RootCommand rootCommand = new("Gestion des finances personnelles");

List<ConsoleCommand> commands = [
    new PrintBilling(),
    new PrintRepetitiveBilling(),
    new PrintBalance(),
    new AddBilling(),
    new AddRepetitiveBilling(),
    new InsertNextBilling(),
    new MultipleInsertNextBilling(),
    new EditBilling(),
    new EditRepetitiveBilling(),
    new DeleteBilling(),
    new DeleteRepetitiveBilling(),
    new ListBillings(),
    new ListRepetitiveBillings(),
    new ShowProjection(),
    new ImportFile(),
    new PrintImport(),
    new CommitImport(),
    new MatchBilling(),
    new PrintImportLine(),
    new InstallService()
];

commands.ForEach(c => rootCommand.AddCommand(c.GetCommand()));

var exitCode = await rootCommand.InvokeAsync(args);

return exitCode;