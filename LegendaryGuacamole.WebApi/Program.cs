using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Extensions;
using LegendaryGuacamole.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => (type.Namespace?.Split(".")?.LastOrDefault() ?? "") + type.Name);
});

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
    .Where(t => !t.IsAbstract
        && t.BaseType != null
        && t.BaseType.IsAssignableTo(typeof(IWorkspaceQuery)))
    .ToList()
    .ForEach(queryType =>
    {
        var genericArguments = queryType.BaseType!.GetGenericArguments();

        if (genericArguments.Length == 2)
            genericArguments = queryType.BaseType!.BaseType!.GetGenericArguments();

        var name = (queryType.Namespace?.Split(".")?.LastOrDefault() ?? "") + queryType.Name;

        var inputType = genericArguments[0];
        var outputType = genericArguments[1];
        var resultType = genericArguments[2];

        var mapMethod = typeof(WebApplicationExtensions).GetMethod(nameof(WebApplicationExtensions.MapQuery));
        mapMethod?.MakeGenericMethod([queryType, inputType, outputType, resultType])
            .Invoke(null, [app, name, channel]);
    });

app.Run();
