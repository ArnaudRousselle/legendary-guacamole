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

    public async Task<TOutput> QueryAsync<TInput, TEvent, TOutput>(WorkspaceQuery<TInput, TEvent, TOutput> query)
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

public abstract class WorkspaceQuery<TInput, TEvent, TOutput> : IWorkspaceQuery
{
    public required TInput Input { get; set; }

    private Channel<QueryResponse<TEvent>?> channel = Channel.CreateUnbounded<QueryResponse<TEvent>?>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        }
    );

    public async Task OnSuccess(QueryResponse<TEvent> response)
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

    public abstract TOutput Map(Workspace workspace, TEvent evt);

    private async Task<TOutput> ReadResponse()
    {
        await channel.Reader.WaitToReadAsync();
        var output = (channel.Reader.TryRead(out QueryResponse<TEvent>? response) ? response : default)
            ?? throw new Exception("internal error");
        return Map(output.Workspace, output.Result);
    }
}
