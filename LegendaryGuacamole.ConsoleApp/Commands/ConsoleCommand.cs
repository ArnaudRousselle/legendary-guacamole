using System.CommandLine;
using LegendaryGuacamole.Models.Settings;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public abstract class ConsoleCommand
{
    protected abstract string Name { get; }
    protected abstract string Description { get; }

    protected abstract void InitializeCommand(Command command);

    protected HttpClient GetHttpClient()
    {
        var webApiSettings = System.Text.Json.JsonSerializer.Deserialize<WebApiSettings>(File.ReadAllText("../settings.json")) ?? throw new Exception("settings error");
        return new()
        {
            BaseAddress = new($"http://localhost:{webApiSettings.Port}/")
        };
    }

    public Command GetCommand()
    {
        Command command = new(Name, Description);
        InitializeCommand(command);
        return command;
    }
}