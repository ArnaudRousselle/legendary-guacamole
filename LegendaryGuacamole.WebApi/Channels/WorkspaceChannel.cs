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

    public async Task<TOutput> QueryAsync<TInput, TOutput>(WorkspaceQuery<TInput, TOutput> query)
    {
        await channel.Writer.WriteAsync(query);
        return await query.Response;
    }
}

public interface IWorkspaceQuery
{
    public Task OnSuccess(Workspace response);
    public Task OnError();
}

public abstract class WorkspaceQuery<TInput, TOutput> : IWorkspaceQuery
{
    public required TInput Input { get; set; }

    private Channel<Workspace?> channel = Channel.CreateUnbounded<Workspace?>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        }
    );

    public async Task OnSuccess(Workspace response)
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

    public abstract TOutput Map(Workspace output);

    private async Task<TOutput> ReadResponse()
    {
        await channel.Reader.WaitToReadAsync();
        var output = (channel.Reader.TryRead(out Workspace? response) ? response : default)
            ?? throw new Exception("internal error");
        return Map(output);
    }
}


