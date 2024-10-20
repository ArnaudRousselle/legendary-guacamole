using System.Threading.Channels;

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
    public Task OnSuccess(object response);
    public Task OnError();
}

public abstract class WorkspaceQuery<TInput, TOutput> : IWorkspaceQuery
{
    public interface IHandler
    {
        public TOutput Handle(TInput input);
    }

    public required TInput Input { get; set; }

    private Channel<TOutput?> channel = Channel.CreateUnbounded<TOutput?>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        }
    );

    public async Task OnSuccess(object response)
    {
        await channel.Writer.WriteAsync((TOutput)response);
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

    private async Task<TOutput> ReadResponse()
    {
        await channel.Reader.WaitToReadAsync();
        return (channel.Reader.TryRead(out TOutput? response) ? response : default)
            ?? throw new Exception("error while reading response");
    }
}

