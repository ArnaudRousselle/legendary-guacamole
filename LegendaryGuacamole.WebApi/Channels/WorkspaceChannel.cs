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

    public async Task QueryAsync<T>(IWorkspaceQuery<T> query)
    {
        await channel.Writer.WriteAsync(query);
    }
}

public interface IWorkspaceQuery
{
}

public interface IWorkspaceQuery<T> : IWorkspaceQuery
{
    public Task OnCompleted(T result);
}

public abstract class WorkspaceQuery : IWorkspaceQuery<bool>
{
    private Channel<bool> channel = Channel.CreateUnbounded<bool>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        }
    );

    public async Task OnCompleted(bool _)
    {
        await channel.Writer.WriteAsync(true);
        channel.Writer.Complete();
    }

    public Task Result
    {
        get => ReadResult();
    }

    private async Task ReadResult()
    {
        await channel.Reader.WaitToReadAsync();
        channel.Reader.TryRead(out _);
    }
}

public abstract class WorkspaceQuery<T> : IWorkspaceQuery<T>
{
    private Channel<T> channel = Channel.CreateUnbounded<T>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        }
    );

    public async Task OnCompleted(T result)
    {
        await channel.Writer.WriteAsync(result);
        channel.Writer.Complete();
    }

    public Task<T?> Result
    {
        get => ReadResult();
    }

    private async Task<T?> ReadResult()
    {
        await channel.Reader.WaitToReadAsync();
        return channel.Reader.TryRead(out T? result) ? result : default;
    }
}