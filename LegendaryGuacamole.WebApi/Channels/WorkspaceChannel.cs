using System.Threading.Channels;

namespace LegendaryGuacamole.WebApi.Channels;

public class WorkspaceChannel
{
    private readonly Channel<object> channel = Channel.CreateUnbounded<object>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        }
    );

    public ChannelReader<object> Reader { get => channel.Reader; }

    public async Task<TOutput> QueryAsync<TInput, TOutput>(IWorkspaceQuery<TInput, TOutput> query)
    {
        await channel.Writer.WriteAsync(query);
        return await query.TaskResult;
    }
}

public interface IWorkspaceQuery { }

public interface IWorkspaceQuery<TInput, TOutput> : IWorkspaceQuery
{
    public TInput Input { get; set; }
    public Task OnCompleted(TOutput result);
    public Task<TOutput> TaskResult { get; }
}

public abstract class WorkspaceQuery<TInput, TOutput> : IWorkspaceQuery<TInput, TOutput> where TInput : new()
{
    public TInput Input { get; set; } = new();

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

    public Task<TOutput> TaskResult
    {
        get => ReadResult();
    }

    private async Task<TOutput> ReadResult()
    {
        await channel.Reader.WaitToReadAsync();
        var result = channel.Reader.TryRead(out TOutput? output) ? output : default;
        if (result == null)
            throw new Exception("error while reading response");
        return result;
    }
}
