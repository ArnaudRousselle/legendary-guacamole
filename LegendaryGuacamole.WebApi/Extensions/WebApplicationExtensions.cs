using LegendaryGuacamole.WebApi.Channels;

namespace LegendaryGuacamole.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static void MapQuery<TQuery, TInput, TOutput>(this WebApplication app, string name, WorkspaceChannel channel) where TQuery : WorkspaceQuery<TInput, TOutput>, new() where TInput : new()
    {
        var routeName = $"/{name[..1].ToLower()}{name[1..]}";

        if (typeof(TInput) == typeof(Empty))
        {
            if (typeof(TOutput) == typeof(Empty))
                app.MapPost(routeName,
                 async () =>
                 {
                     TQuery query = new();
                     await channel.QueryAsync(query);
                 })
                 .WithName(name)
                 .WithOpenApi();
            else
                app.MapPost(routeName,
                async () =>
                {
                    TQuery query = new();
                    return await channel.QueryAsync(query);
                })
                .WithName(name)
                .WithOpenApi();
        }
        else
        {
            if (typeof(TOutput) == typeof(Empty))
                app.MapPost(routeName,
                     async (TInput input) =>
                     {
                         TQuery query = new()
                         {
                             Input = input
                         };
                         await channel.QueryAsync(query);
                     })
                     .WithName(name)
                     .WithOpenApi();
            else
                app.MapPost(routeName,
                async (TInput input) =>
                {
                    TQuery query = new()
                    {
                        Input = input
                    };
                    return await channel.QueryAsync(query);
                })
                .WithName(name)
                .WithOpenApi();
        }
    }
}