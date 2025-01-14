using System.Threading.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Channels;

public class WorkspaceChannel
{
    private readonly Channel<IWorkspaceQuery> channel = Channel.CreateUnbounded<IWorkspaceQuery>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        }
    );

    public ChannelReader<IWorkspaceQuery> Reader { get => channel.Reader; }

    public async Task<TOutput> QueryAsync<TInput, TResult, TOutput>(WorkspaceQuery<TInput, TResult, TOutput> query)
    {
        await channel.Writer.WriteAsync(query);
        return await query.Response;
    }
}

public interface IWorkspaceQuery
{
    public Task OnError();
}

public class QueryResponse<T>
{
    public required Workspace Workspace { get; set; }
    public required T Result { get; set; }
}

public abstract class WorkspaceQuery<TInput, TResult, TOutput> : IWorkspaceQuery
{
    //todo ARNAUD: revoir la façon dont sont faites les queries : déplacer recherche de WorkspaceService vers Map
    public required TInput Input { get; set; }

    private Channel<QueryResponse<TResult>?> channel = Channel.CreateUnbounded<QueryResponse<TResult>?>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        }
    );

    public async Task OnSuccess(QueryResponse<TResult> response)
    {
        await channel.Writer.WriteAsync(response);
        channel.Writer.Complete();
    }

    public async Task OnError()
    {
        await channel.Writer.WriteAsync(default);
        channel.Writer.Complete();
    }

    public Task<TOutput> Response
    {
        get => ReadResponse();
    }

    public abstract TOutput Map(Workspace workspace, TResult result);

    private async Task<TOutput> ReadResponse()
    {
        await channel.Reader.WaitToReadAsync();
        var output = (channel.Reader.TryRead(out QueryResponse<TResult>? response) ? response : default)
            ?? throw new Exception("internal error");
        return Map(output.Workspace, output.Result);
    }
}
