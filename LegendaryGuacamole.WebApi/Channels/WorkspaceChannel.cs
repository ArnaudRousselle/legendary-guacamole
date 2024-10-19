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

    public async Task<QueryResponce<TOutput>> QueryAsync<TInput, TOutput>(WorkspaceQuery<TInput, TOutput> query)
    {
        await channel.Writer.WriteAsync(query);
        return await query.Response;
    }

    public async Task<QueryResponce<TOutput>> QueryAsync<TOutput>(WorkspaceQuery<TOutput> query)
    {
        await channel.Writer.WriteAsync(query);
        return await query.Response;
    }
}

public interface IWorkspaceQuery
{
    public Task OnError();
}

public abstract class WorkspaceQuery<TOutput> : IWorkspaceQuery
{
    private Channel<QueryResponce<TOutput>> channel = Channel.CreateUnbounded<QueryResponce<TOutput>>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            SingleWriter = false
        }
    );

    public async Task OnSuccess(TOutput response)
    {
        await channel.Writer.WriteAsync(QueryResponce<TOutput>.Success(response));
        channel.Writer.Complete();
    }

    public async Task OnError()
    {
        await channel.Writer.WriteAsync(QueryResponce<TOutput>.Error());
        channel.Writer.Complete();
    }

    public Task<QueryResponce<TOutput>> Response
    {
        get => ReadResponse();
    }

    private async Task<QueryResponce<TOutput>> ReadResponse()
    {
        await channel.Reader.WaitToReadAsync();
        var result = channel.Reader.TryRead(out QueryResponce<TOutput>? response) ? response : default;
        if (result == null)
            throw new Exception("error while reading response");
        return result;
    }
}

public abstract class WorkspaceQuery<TInput, TOutput> : WorkspaceQuery<TOutput>
{
    public required TInput Input { get; set; }
}

public class QueryResponce<T>
{
    public T? Result { get; init; }
    public bool HasError { get; init; }

    private QueryResponce(T result)
    {
        Result = result;
    }

    private QueryResponce()
    {
        HasError = true;
    }

    public static QueryResponce<T> Success(T result) => new(result);
    public static QueryResponce<T> Error() => new();
}
