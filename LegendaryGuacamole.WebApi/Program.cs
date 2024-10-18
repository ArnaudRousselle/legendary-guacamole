using System.Reflection.Emit;
using LegendaryGuacamole.WebApi.Channels;
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
    .Where(t => t.BaseType != null && t.BaseType.IsAssignableTo(typeof(IWorkspaceQuery)))
    .ToList()
    .ForEach(t =>
    {
        var genericArguments = t.BaseType!.GetGenericArguments();
        var inputType = genericArguments[0];
        var outputType = genericArguments[1];

        DynamicMethod method = new DynamicMethod(
            t.Name,
            outputType,
            [inputType],
            typeof(Program).Module);

        app.MapPost($"/{t.Name[..1].ToLower()}{t.Name[1..]}", async () =>
        {
            await Task.CompletedTask;
        })
        .WithName(t.Name)
        .WithOpenApi();
    });

app.Run();
