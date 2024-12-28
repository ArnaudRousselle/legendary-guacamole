using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Extensions;
using LegendaryGuacamole.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WorkspaceChannel channel = new();

builder.Services.AddSingleton(channel);
builder.Services.AddHostedService<WorkspaceService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

app.Run();
