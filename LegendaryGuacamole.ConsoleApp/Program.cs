using System.CommandLine;
using LegendaryGuacamole.ConsoleApp.Commands;

HttpClient httpClient = new()
{
    BaseAddress = new("http://localhost:5152/")
};

RootCommand rootCommand = new("Gestion des finances personnelles");

List<ConsoleCommand> commands = [
    new AddBilling(),
    new EditBilling(),
    new ListBillings(),
];

commands.ForEach(c => rootCommand.AddCommand(c.GetCommand(httpClient)));

var exitCode = await rootCommand.InvokeAsync(args);

return exitCode;