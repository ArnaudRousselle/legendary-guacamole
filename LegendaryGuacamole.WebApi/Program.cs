using LegendaryGuacamole.WebApi.Model;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicy");
}

app.UseHttpsRedirection();

const string fileName = "./data.lgc";

Workspace workspace = File.Exists(fileName)
    ? System.Text.Json.JsonSerializer.Deserialize<Workspace>(File.ReadAllText(fileName))!
    : new();

if (!File.Exists(fileName))

    app.MapGet("/billings", () => workspace.Billings.Select(n => n.ToDtos()))
    .WithName("GetBillings")
    .WithOpenApi();

app.Run();
