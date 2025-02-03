
using System.CommandLine;
using System.Diagnostics;
using System.Text.Json;
using LegendaryGuacamole.Models.Settings;

namespace LegendaryGuacamole.ConsoleApp.Commands;

public class InstallService : ConsoleCommand
{
    protected override string Name => "install";

    protected override string Description => "Installe la Web API";

    protected override void InitializeCommand(Command command)
    {
        Option<int> port = new(["--port", "-p"], "Port")
        {
            IsRequired = true
        };

        Option<string> name = new(["--name", "-n"], "Name")
        {
            IsRequired = true
        };

        Option<string> file = new(["--file", "-f"], "File")
        {
            IsRequired = true
        };

        command.AddOption(port);
        command.AddOption(name);
        command.AddOption(file);

        command.SetHandler(async (port, name, file) =>
        {
            WebApiSettings settings = new()
            {
                FilePath = file,
                Port = port
            };

            File.WriteAllText("settings.json", JsonSerializer.Serialize(settings));

            var path = Path.GetFullPath("./webapi/LegendaryGuacamole.WebApi.exe");

            using var process = Process.Start("sc.exe", $"create {name} binPath= \"{path}\" start= delayed-auto");
            process.WaitForExit();
        }, port, name, file);
    }
}