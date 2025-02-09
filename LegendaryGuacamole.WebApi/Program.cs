using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Extensions;
using LegendaryGuacamole.WebApi.Services;
using LegendaryGuacamole.Models.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder();

WorkspaceChannel channel = new();

var location = Assembly.GetExecutingAssembly().Location;
var path = Path.Combine(location[0..location.LastIndexOf('\\')], "..\\settings.json");

#if DEBUG
path = "../settings.json";
#endif

var webApiSettings = System.Text.Json.JsonSerializer.Deserialize<WebApiSettings>(File.ReadAllText(path)) ?? throw new Exception("settings error");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(channel);
builder.Services.AddSingleton(webApiSettings);
builder.Services.AddWindowsService();
builder.Services.AddHostedService<WorkspaceService>();

var app = builder.Build();

AppDomain.CurrentDomain
    .GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(t => !t.IsAbstract
        && t.BaseType != null
        && t.BaseType.IsAssignableTo(typeof(IWorkspaceQuery)))
    .ToList()
    .ForEach(queryType =>
    {
        var genericArguments = queryType.BaseType!.GetGenericArguments();

        var inputType = genericArguments[0];
        var resultType = genericArguments[1];
        var outputType = genericArguments[2];

        var mapMethod = typeof(WebApplicationExtensions).GetMethod(nameof(WebApplicationExtensions.MapQuery));
        mapMethod?.MakeGenericMethod([queryType, inputType, resultType, outputType])
            .Invoke(null, [app, queryType.Name, channel]);
    });

app.Run($"http://localhost:{webApiSettings.Port}");
