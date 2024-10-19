using System.Reflection.Emit;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Extensions;
using LegendaryGuacamole.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o =>
    o.AddPolicy("CorsPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
        }));

WorkspaceChannel channel = new();

builder.Services.AddSingleton(channel);
builder.Services.AddHostedService<WorkspaceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicy");
}

app.UseHttpsRedirection();

AppDomain.CurrentDomain
    .GetAssemblies()
    .SelectMany(s => s.GetTypes())
    .Where(t => !t.IsAbstract && t.BaseType != null && t.BaseType.IsAssignableTo(typeof(IWorkspaceQuery)))
    .ToList()
    .ForEach(queryType =>
    {
        var genericArguments = queryType.BaseType!.GetGenericArguments();

        if (genericArguments.Length == 1)
        {
            var outputType = genericArguments[0];

            var mapMethod = typeof(WebApplicationExtensions).GetMethod(nameof(WebApplicationExtensions.MapQuery));
            mapMethod?.MakeGenericMethod([queryType, outputType])
                .Invoke(null, [app, queryType.Name, channel]);
        }
        else
        {
            var inputType = genericArguments[0];
            var outputType = genericArguments[1];

            var mapMethod = typeof(WebApplicationExtensions).GetMethod(nameof(WebApplicationExtensions.MapQueryWithInput));
            mapMethod?.MakeGenericMethod([queryType, inputType, outputType])
                .Invoke(null, [app, queryType.Name, channel]);
        }
    });

app.Run();
