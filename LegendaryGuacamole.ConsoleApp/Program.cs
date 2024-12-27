using System.CommandLine;
using LegendaryGuacamole.ConsoleApp.Commands;

HttpClient httpClient = new()
{
    BaseAddress = new("http://localhost:5152/")
};

RootCommand rootCommand = new("Gérer vos finances personnelles");

List<ConsoleCommand> commands = [
    new ListBillings()
];

commands.ForEach(c => rootCommand.AddCommand(c.GetCommand(httpClient)));

var exitCode = await rootCommand.InvokeAsync(args);

return exitCode;