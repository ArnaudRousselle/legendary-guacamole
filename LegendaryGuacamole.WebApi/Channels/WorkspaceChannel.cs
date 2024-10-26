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

    public async Task<TResult> QueryAsync<TInput, TOutput, TResult>(WorkspaceQuery<TInput, TOutput, TResult> query)
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

public abstract class WorkspaceQuery<TInput, TOutput, TResult> : IWorkspaceQuery
{
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

    public Task<TResult> Response
    {
        get => ReadResponse();
    }

    public abstract TResult Map(TOutput output);

    private async Task<TResult> ReadResponse()
    {
        await channel.Reader.WaitToReadAsync();
        var output = (channel.Reader.TryRead(out TOutput? response) ? response : default)
            ?? throw new Exception("internal error");
        return Map(output);
    }
}

public abstract class WorkspaceQuery<TInput, TResult> : WorkspaceQuery<TInput, TResult, TResult>
{
    public override TResult Map(TResult output) => output;
}

