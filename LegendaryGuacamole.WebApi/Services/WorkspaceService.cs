using System.Collections.Immutable;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Queries;

namespace LegendaryGuacamole.WebApi.Services;

public class WorkspaceService(WorkspaceChannel channel, ILogger<WorkspaceService> logger)
    : BackgroundService
{
    private Models.Workspace workspace = new();

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const string fileName = "./data.lgc";

        workspace = File.Exists(fileName)
            ? System.Text.Json.JsonSerializer.Deserialize<Models.Workspace>(File.ReadAllText(fileName))!
            : new();

        //todo ARNAUD: à supprimer
        for (var i = 0; i < 100; i++)
            workspace.Billings = workspace.Billings.Add(new()
            {
                Id = Guid.NewGuid(),
                Amount = i + 1,
                Checked = true,
                Title = "Mon titre " + i,
                Comment = i % 3 == 0 ? "mon commentaire " + i : null,
                ValuationDate = new DateOnly(2024, 10, 12)
            });

        while (await channel.Reader.WaitToReadAsync(stoppingToken))
        {
            while (channel.Reader.TryRead(out IWorkspaceQuery? message))
            {
                if (message == null)
                    continue;

                try
                {
                    switch (message)
                    {
                        case AddBilling q:
                            await q.OnSuccess(Handle(q));
                            break;
                        case DeleteBilling q:
                            await q.OnSuccess(Handle(q));
                            break;
                        case EditBilling q:
                            await q.OnSuccess(Handle(q));
                            break;
                        case GetBilling q:
                            await q.OnSuccess(Handle(q));
                            break;
                        case GetSummary q:
                            await q.OnSuccess(Handle(q));
                            break;
                        case ListBillings q:
                            await q.OnSuccess(Handle(q));
                            break;
                        case SetChecked q:
                            await q.OnSuccess(Handle(q));
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.ToString());
                    await message.OnError();
                }
            }
        }
    }

    private QueryResponse<AddBillingResult> Handle(AddBilling q)
    {
        var newId = Guid.NewGuid();

        workspace = workspace with
        {
            Billings = workspace.Billings.Add(new()
            {
                Id = newId,
                Amount = q.Input.Amount,
                Checked = q.Input.Checked,
                Comment = q.Input.Comment,
                IsArchived = q.Input.IsArchived,
                IsSaving = q.Input.IsSaving,
                Title = q.Input.Title,
                ValuationDate = q.Input.ValuationDate.ToDateOnly()
            })
        };

        return Success(new AddBillingResult
        {
            Id = newId
        });
    }

    private QueryResponse<DeleteBillingResult> Handle(DeleteBilling q)
    {
        var index = workspace.Billings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("billing not found");

        workspace = workspace with
        {
            Billings = workspace.Billings.RemoveAt(index)
        };

        return Success(new DeleteBillingResult());
    }

    private QueryResponse<EditBillingResult> Handle(EditBilling q)
    {
        var index = workspace.Billings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("billing not found");

        var billing = workspace.Billings[index];

        workspace = workspace with
        {
            Billings = workspace.Billings
                .RemoveAt(index)
                .Insert(index, billing with
                {
                    Amount = q.Input.Amount,
                    Checked = q.Input.Checked,
                    Comment = q.Input.Comment,
                    IsArchived = q.Input.IsArchived,
                    IsSaving = q.Input.IsSaving,
                    Title = q.Input.Title,
                    ValuationDate = q.Input.ValuationDate.ToDateOnly(),
                })
        };

        return Success(new EditBillingResult
        {
            Index = index
        });
    }

    private QueryResponse<GetBillingResult> Handle(GetBilling q)
    {
        throw new NotImplementedException();
    }

    private QueryResponse<GetSummaryResult> Handle(GetSummary q)
    {
        throw new NotImplementedException();
    }

    private QueryResponse<ListBillingsResult> Handle(ListBillings q)
    {
        throw new NotImplementedException();
    }

    private QueryResponse<SetCheckedResult> Handle(SetChecked q)
    {
        throw new NotImplementedException();
    }

    private QueryResponse<T> Success<T>(T result)
    => new()
    {
        Workspace = workspace,
        Result = result
    };

    // public AddBilling.Output Handle(AddBilling.Input input)
    // {
    //     var newId = Guid.NewGuid();

    //     Models.Billing b = new()
    //     {
    //         Id = newId,
    //         Amount = input.Amount,
    //         Checked = input.Checked,
    //         Comment = input.Comment,
    //         IsArchived = input.IsArchived,
    //         IsSaving = input.IsSaving,
    //         Title = input.Title,
    //         ValuationDate = input.ValuationDate.ToDateOnly()
    //     };

    //     billings.Add(b);

    //     return new AddBilling.Output
    //     {
    //         Id = b.Id,
    //         ValuationDate = new()
    //         {
    //             Year = b.ValuationDate.Year,
    //             Month = b.ValuationDate.Month,
    //             Day = b.ValuationDate.Day
    //         },
    //         Title = b.Title,
    //         Amount = b.Amount,
    //         Checked = b.Checked,
    //         Comment = b.Comment,
    //         IsArchived = b.IsArchived,
    //         IsSaving = b.IsSaving
    //     };
    // }

    // public DeleteBilling.Output Handle(DeleteBilling.Input input)
    // {
    //     var billing = billings.SingleOrDefault(b => b.Id == input.Id);

    //     if (billing == null)
    //         throw new Exception("billing not found");

    //     billings.Remove(billing);

    //     return new() { HasBeenDeleted = false };
    // }

    // public EditBilling.Output Handle(EditBilling.Input input)
    // {
    //     var b = billings.SingleOrDefault(b => b.Id == input.Id);

    //     if (b == null)
    //         throw new Exception("billing not found");

    //     b.Amount = input.Amount;
    //     b.Checked = input.Checked;
    //     b.Comment = input.Comment;
    //     b.IsArchived = input.IsArchived;
    //     b.IsSaving = input.IsSaving;
    //     b.Title = input.Title;
    //     b.ValuationDate = input.ValuationDate.ToDateOnly();

    //     return new()
    //     {
    //         Id = b.Id,
    //         ValuationDate = new()
    //         {
    //             Year = b.ValuationDate.Year,
    //             Month = b.ValuationDate.Month,
    //             Day = b.ValuationDate.Day
    //         },
    //         Title = b.Title,
    //         Amount = b.Amount,
    //         Checked = b.Checked,
    //         Comment = b.Comment,
    //         IsArchived = b.IsArchived,
    //         IsSaving = b.IsSaving
    //     };
    // }

    // public GetBilling.Output Handle(GetBilling.Input input)
    // {
    //     var b = billings.Single(b => b.Id == input.Id);
    //     return new()
    //     {
    //         Id = b.Id,
    //         ValuationDate = new()
    //         {
    //             Year = b.ValuationDate.Year,
    //             Month = b.ValuationDate.Month,
    //             Day = b.ValuationDate.Day
    //         },
    //         Title = b.Title,
    //         Amount = b.Amount,
    //         Checked = b.Checked,
    //         Comment = b.Comment,
    //         IsArchived = b.IsArchived,
    //         IsSaving = b.IsSaving
    //     };
    // }

    // public GetSummary.Output[] Handle(GetSummary.Input input)
    //     => billings.Select(b => new GetSummary.Output()
    //     {
    //         ValuationDate = b.ValuationDate,
    //         Amount = b.Amount
    //     }).ToArray();

    // public ListBillings.Output[] Handle(ListBillings.Input input)
    // {
    //     var query = billings.AsQueryable();

    //     if (!string.IsNullOrEmpty(input.Title))
    //         query = query.Where(n => n.Title.ToLower().Contains(input.Title.ToLower()));
    //     if (input.Amount.HasValue)
    //         query = query.Where(n => input.Amount - (input.DeltaAmount ?? 0) <= n.Amount
    //             && n.Amount <= input.Amount + (input.DeltaAmount ?? 0));
    //     if (input.StartDate != null)
    //         query = query.Where(n => input.StartDate.ToDateOnly() <= n.ValuationDate);
    //     if (input.EndDate != null)
    //         query = query.Where(n => n.ValuationDate <= input.EndDate.ToDateOnly());
    //     if (!(input.WithArchived ?? false))
    //         query = query.Where(n => !n.IsArchived);

    //     return billings
    //         .Select(b => new ListBillings.Output
    //         {
    //             Id = b.Id,
    //             ValuationDate = new()
    //             {
    //                 Year = b.ValuationDate.Year,
    //                 Month = b.ValuationDate.Month,
    //                 Day = b.ValuationDate.Day
    //             },
    //             Title = b.Title,
    //             Amount = b.Amount,
    //             Checked = b.Checked,
    //             Comment = b.Comment,
    //             IsArchived = b.IsArchived,
    //             IsSaving = b.IsSaving
    //         })
    //         .ToArray();
    // }

    // private SetChecked.Output Handle(SetChecked.Input input)
    // {
    //     var b = billings.SingleOrDefault(b => b.Id == input.Id);

    //     if (b == null)
    //         throw new Exception("billing not found");

    //     b.Checked = input.Checked;

    //     return new()
    //     {
    //         Id = b.Id,
    //         Checked = b.Checked
    //     };
    // }

}

public static class ImmutableArrayExtensions
{
    public static int Find<T>(this ImmutableArray<T> array, Func<T, bool> predicate)
    {
        for (var i = 0; i < array.Length; i++)
            if (predicate(array[i])) return i;
        return -1;
    }
}