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

    public async Task QueryAsync<TInput, TOutput>(IWorkspaceQuery<TInput, TOutput> query)
    {
        await channel.Writer.WriteAsync(query);
    }
}

public interface IWorkspaceQuery
{
}

public interface IWorkspaceQuery<TInput, TOutput> : IWorkspaceQuery
{
    public TInput Input { get; set; }
    public Task OnCompleted(TOutput result);
}

public abstract class WorkspaceQuery<TInput, TOutput> : IWorkspaceQuery<TInput, TOutput>
{
    public required TInput Input { get; set; }

    private Channel<TOutput> channel = Channel.CreateUnbounded<TOutput>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        }
    );

    public async Task OnCompleted(TOutput result)
    {
        await channel.Writer.WriteAsync(result);
        channel.Writer.Complete();
    }

    public Task<TOutput?> Result
    {
        get => ReadResult();
    }

    private async Task<TOutput?> ReadResult()
    {
        await channel.Reader.WaitToReadAsync();
        return channel.Reader.TryRead(out TOutput? result) ? result : default;
    }
}
