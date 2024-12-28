using LegendaryGuacamole.WebApi.Channels;

namespace LegendaryGuacamole.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static void MapQuery<TQuery, TInput, TResult, TOutput>(this WebApplication app, string name, WorkspaceChannel channel) where TQuery : WorkspaceQuery<TInput, TResult, TOutput>, new() where TInput : new()
    {
        app.MapPost($"/{name[..1].ToLower()}{name[1..]}",
            async (TInput input) =>
            {
                TQuery query = new()
                {
                    Input = input
                };
                return await channel.QueryAsync(query);
            })
            .WithName(name)
            .WithTags("Workspace")
            .WithOpenApi(a =>
            {
                a.RequestBody.Required = true;
                return a;
            });
    }
}