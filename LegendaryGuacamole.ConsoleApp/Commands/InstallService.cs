
using System.CommandLine;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class InstallService : ConsoleCommand
{
    protected override string Name => "install";

    protected override string Description => "Installe la Web API";

    protected override void InitializeCommand(Command command, HttpClient httpClient)
    {
        Option<int> port = new(["--port", "-p"], "Port")
        {
            IsRequired = true
        };

        Option<string> name = new(["--name", "-n"], "Name")
        {
            IsRequired = true
        };

        command.AddOption(port);
        command.AddOption(name);

        command.SetHandler(async (port, name) =>
        {
            //todo ARNAUD: à continuer
        }, port, name);
    }
}