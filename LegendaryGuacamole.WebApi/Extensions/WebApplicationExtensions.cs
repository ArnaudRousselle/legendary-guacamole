using System.Net;
using LegendaryGuacamole.WebApi.Channels;

namespace LegendaryGuacamole.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static void MapQuery<TQuery, TOutput>(this WebApplication app, string name, WorkspaceChannel channel) where TQuery : WorkspaceQuery<TOutput>, new()
    {
        var routeName = $"/{name[..1].ToLower()}{name[1..]}";

        app.MapPost(routeName,
            async () =>
            {
                TQuery query = new();
                var response = await channel.QueryAsync(query);
                return !response.HasError ? Results.Ok(response.Result) : Results.Problem();
            })
            .Produces<TOutput>()
            .WithName(name)
            .WithOpenApi();
    }

    public static void MapQueryWithInput<TQuery, TInput, TOutput>(this WebApplication app, string name, WorkspaceChannel channel) where TQuery : WorkspaceQuery<TInput, TOutput>, new() where TInput : new()
    {
        var routeName = $"/{name[..1].ToLower()}{name[1..]}";

        app.MapPost(routeName,
            async (TInput input) =>
            {
                TQuery query = new()
                {
                    Input = input
                };
                var response = await channel.QueryAsync(query);
                return !response.HasError ? Results.Ok(response.Result) : Results.Problem();
            })
            .Produces<TOutput>()
            .WithName(name)
            .WithOpenApi();
    }
}