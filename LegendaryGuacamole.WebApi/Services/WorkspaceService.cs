using System.Collections.Immutable;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Queries;

namespace LegendaryGuacamole.WebApi.Services;

public class WorkspaceService(WorkspaceChannel channel, ILogger<WorkspaceService> logger)
    : BackgroundService
{
    private Models.Workspace workspace = default!;

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const string fileName = "./data.lgc";

        workspace = File.Exists(fileName)
            ? System.Text.Json.JsonSerializer.Deserialize<Models.Workspace>(File.ReadAllText(fileName))!
            : new()
            {
                Billings = [],
                RepetitiveBillings = []
            };

        //todo ARNAUD: à supprimer
        for (var i = 0; i < 100; i++)
            workspace = workspace with
            {
                Billings = workspace.Billings.Add(new()
                {
                    Id = Guid.NewGuid(),
                    Amount = i + 1.45m,
                    Checked = i % 5 == 0,
                    Title = "Mon titre " + i,
                    Comment = i % 3 == 0 ? "mon commentaire " + i : "",
                    ValuationDate = new DateOnly(2024, 10, 12),
                    IsSaving = false
                })
            };

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
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case AddRepetitiveBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case DeleteBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case DeleteRepetitiveBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case EditBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case EditRepetitiveBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case GetBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case GetRepetitiveBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case GetSummary q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case InsertNextBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case ListBillings q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
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

    private AddBillingEvent Handle(AddBilling q)
    {
        var newId = Guid.NewGuid();

        Models.Billing newBilling = new()
        {
            Id = newId,
            Amount = q.Input.Amount,
            Checked = q.Input.Checked,
            Comment = q.Input.Comment,
            IsSaving = q.Input.IsSaving,
            Title = q.Input.Title,
            ValuationDate = q.Input.ValuationDate.ToDateOnly()
        };

        workspace = workspace with
        {
            Billings = workspace.Billings.Add(newBilling)
        };

        return new()
        {
            Billing = newBilling
        };
    }

    private AddRepetitiveBillingEvent Handle(AddRepetitiveBilling q)
    {
        var newId = Guid.NewGuid();

        Models.RepetitiveBilling newRepetitiveBilling = new()
        {
            Id = newId,
            NextValuationDate = q.Input.NextValuationDate.ToDateOnly(),
            Title = q.Input.Title,
            Amount = q.Input.Amount,
            IsSaving = q.Input.IsSaving,
            Frequence = q.Input.Frequence
        };

        workspace = workspace with
        {
            RepetitiveBillings = workspace.RepetitiveBillings.Add(newRepetitiveBilling)
        };

        return new()
        {
            RepetitiveBilling = newRepetitiveBilling
        };
    }

    private DeleteBillingEvent Handle(DeleteBilling q)
    {
        var index = workspace.Billings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("billing not found");

        workspace = workspace with
        {
            Billings = workspace.Billings.RemoveAt(index)
        };

        return new();
    }

    private DeleteRepetitiveBillingEvent Handle(DeleteRepetitiveBilling q)
    {
        var index = workspace.RepetitiveBillings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("repetitive billing not found");

        workspace = workspace with
        {
            RepetitiveBillings = workspace.RepetitiveBillings.RemoveAt(index)
        };

        return new();
    }

    private EditBillingEvent Handle(EditBilling q)
    {
        var index = workspace.Billings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("billing not found");

        var billing = workspace.Billings[index];

        if (q.Input.Amount != null)
            billing = billing with { Amount = q.Input.Amount.Value };

        if (q.Input.Checked != null)
            billing = billing with { Checked = q.Input.Checked.Value };

        if (q.Input.Comment != null)
            billing = billing with { Comment = q.Input.Comment };

        if (q.Input.IsSaving != null)
            billing = billing with { IsSaving = q.Input.IsSaving.Value };

        if (q.Input.Title != null)
            billing = billing with { Title = q.Input.Title };

        if (q.Input.ValuationDate != null)
            billing = billing with { ValuationDate = q.Input.ValuationDate.ToDateOnly() };

        workspace = workspace with
        {
            Billings = workspace.Billings
                .RemoveAt(index)
                .Insert(index, billing)
        };

        return new()
        {
            Billing = billing
        };
    }

    private EditRepetitiveBillingEvent Handle(EditRepetitiveBilling q)
    {
        var index = workspace.RepetitiveBillings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("repetitive billing not found");

        var repetitiveBilling = workspace.RepetitiveBillings[index];

        if (q.Input.NextValuationDate != null)
            repetitiveBilling = repetitiveBilling with { NextValuationDate = q.Input.NextValuationDate.ToDateOnly() };

        if (q.Input.Title != null)
            repetitiveBilling = repetitiveBilling with { Title = q.Input.Title };

        if (q.Input.Amount != null)
            repetitiveBilling = repetitiveBilling with { Amount = q.Input.Amount.Value };

        if (q.Input.IsSaving != null)
            repetitiveBilling = repetitiveBilling with { IsSaving = q.Input.IsSaving.Value };

        if (q.Input.Frequence != null)
            repetitiveBilling = repetitiveBilling with { Frequence = q.Input.Frequence.Value };

        workspace = workspace with
        {
            RepetitiveBillings = workspace.RepetitiveBillings
                .RemoveAt(index)
                .Insert(index, repetitiveBilling)
        };

        return new()
        {
            RepetitiveBilling = repetitiveBilling
        };
    }

    private GetBillingEvent Handle(GetBilling q)
    {
        var index = workspace.Billings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("billing not found");

        return new()
        {
            Billing = workspace.Billings[index]
        };
    }

    private GetRepetitiveBillingEvent Handle(GetRepetitiveBilling q)
    {
        var index = workspace.RepetitiveBillings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("repetitive billing not found");

        return new()
        {
            RepetitiveBilling = workspace.RepetitiveBillings[index]
        };
    }

    private GetSummaryEvent Handle(GetSummary q)
    {
        return new();
    }

    private InsertNextBillingEvent Handle(InsertNextBilling q)
    {
        var index = workspace.RepetitiveBillings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("repetitive billing not found");

        var repetitiveBilling = workspace.RepetitiveBillings[index];

        var newId = Guid.NewGuid();

        Models.Billing newBilling = new()
        {
            Id = newId,
            Amount = repetitiveBilling.Amount,
            Checked = false,
            Comment = "",
            IsSaving = repetitiveBilling.IsSaving,
            Title = repetitiveBilling.Title,
            ValuationDate = repetitiveBilling.NextValuationDate
        };

        var editedRepetitiveBilling = repetitiveBilling with
        {
            NextValuationDate = repetitiveBilling.Frequence switch
            {
                LegendaryGuacamole.Models.Common.Frequence.Monthly => repetitiveBilling.NextValuationDate.AddMonths(1),
                LegendaryGuacamole.Models.Common.Frequence.Bimonthly => repetitiveBilling.NextValuationDate.AddMonths(2),
                LegendaryGuacamole.Models.Common.Frequence.Quaterly => repetitiveBilling.NextValuationDate.AddMonths(3),
                LegendaryGuacamole.Models.Common.Frequence.Annual => repetitiveBilling.NextValuationDate.AddMonths(12),
                _ => throw new NotImplementedException()
            },
        };

        workspace = workspace with
        {
            Billings = workspace.Billings.Add(newBilling),
            RepetitiveBillings = workspace.RepetitiveBillings
                .RemoveAt(index)
                .Insert(index, repetitiveBilling),
        };

        return new InsertNextBillingEvent
        {
            Billing = newBilling,
            RepetitiveBilling = editedRepetitiveBilling
        };
    }

    private ListBillingsEvent Handle(ListBillings _)
    {
        return new();
    }

    private QueryResponse<T> ToQueryResponse<T>(T result)
    => new()
    {
        Workspace = workspace,
        Result = result
    };
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