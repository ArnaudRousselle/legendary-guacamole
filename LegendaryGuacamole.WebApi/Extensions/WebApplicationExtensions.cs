using System.Net;
using LegendaryGuacamole.WebApi.Channels;

namespace LegendaryGuacamole.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static void MapQuery<TQuery, TInput, TOutput>(this WebApplication app, string name, WorkspaceChannel channel) where TQuery : WorkspaceQuery<TInput, TOutput>, new() where TInput : new()
    {
        app.MapPost($"/{name[..1].ToLower()}{name[1..]}",
            async (TInput input) =>
            {
                TQuery query = new()
                {
                    Input = input
                };
                //todo ARNAUD: tester si exception (programme se ferme ???)
                return await channel.QueryAsync(query);
            })
            .WithName(name)
            .WithOpenApi();
    }
}