using System.CommandLine;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public abstract class ConsoleCommand
{
    protected abstract string Name { get; }
    protected abstract string Description { get; }

    protected abstract void InitializeCommand(Command command, HttpClient httpClient);

    public Command GetCommand(HttpClient httpClient)
    {
        Command command = new(Name, Description);
        InitializeCommand(command, httpClient);
        return command;
    }
}