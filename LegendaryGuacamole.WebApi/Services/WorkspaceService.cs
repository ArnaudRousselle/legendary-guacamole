using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using LegendaryGuacamole.Models.Settings;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Commons;
using LegendaryGuacamole.WebApi.Extensions;
using LegendaryGuacamole.WebApi.Queries;

namespace LegendaryGuacamole.WebApi.Services;

public class WorkspaceService(WorkspaceChannel channel,
    WebApiSettings settings,
    ILogger<WorkspaceService> logger) : BackgroundService
{
    private Models.Workspace workspace = new()
    {
        Billings = [],
        RepetitiveBillings = []
    };

    private Models.Import? import;

    private DateTime _lastFileAccess = DateTime.MinValue;

    private void Save()
    {
        var json = JsonSerializer.Serialize(workspace, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(settings.FilePath, json);
        _lastFileAccess = DateTime.Now;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!File.Exists(settings.FilePath))
            Save();

        while (await channel.Reader.WaitToReadAsync(stoppingToken))
        {
            while (channel.Reader.TryRead(out IWorkspaceQuery? message))
            {
                if (message == null)
                    continue;

                if (DateTime.Now - _lastFileAccess >= TimeSpan.FromMinutes(5))
                {
                    workspace = JsonSerializer.Deserialize<Models.Workspace>(File.ReadAllText(settings.FilePath))!;
                    _lastFileAccess = DateTime.Now;
                }

                try
                {
                    switch (message)
                    {
                        case AddBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            Save();
                            break;
                        case AddRepetitiveBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            Save();
                            break;
                        case CommitImport q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            Save();
                            break;
                        case DeleteBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            Save();
                            break;
                        case DeleteRepetitiveBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            Save();
                            break;
                        case EditBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            Save();
                            break;
                        case EditRepetitiveBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            Save();
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
                            Save();
                            break;
                        case ImportFile q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case ListBillings q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case ListRepetitiveBillings q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case MatchBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case MultipleInsertNextBilling q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            Save();
                            break;
                        case ShowImport q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case ShowImportLineDetail q:
                            await q.OnSuccess(ToQueryResponse(Handle(q)));
                            break;
                        case ShowProjection q:
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

    private AddBillingResult Handle(AddBilling q)
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
            Index = workspace.Billings.Length - 1
        };
    }

    private AddRepetitiveBillingResult Handle(AddRepetitiveBilling q)
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
            Index = workspace.RepetitiveBillings.Length - 1
        };
    }

    private CommitImportResult Handle(CommitImport q)
    {
        if (import == null)
            throw new Exception("no import");

        var newWorkspace = workspace;
        List<int> indexes = [];

        foreach (var line in import.Lines)
        {
            if (line.SelectedIndex < 0)
            {
                newWorkspace = newWorkspace with
                {
                    Billings = newWorkspace.Billings.Add(new()
                    {
                        Id = Guid.NewGuid(),
                        Amount = line.Amount,
                        Checked = true,
                        Comment = "",
                        IsSaving = false,
                        Title = line.Name,
                        ValuationDate = line.Date
                    })
                };
                indexes.Add(newWorkspace.Billings.Length - 1);
            }
            else
            {
                var billingId = line.Matchings[line.SelectedIndex];
                var billingIndex = newWorkspace.Billings.FindIndexWithPredicate(b => b.Id == billingId);

                if (billingIndex < 0)
                    throw new Exception("billing not found: " + billingId);

                var billing = newWorkspace.Billings[billingIndex];

                newWorkspace = newWorkspace with
                {
                    Billings = newWorkspace.Billings
                            .RemoveAt(billingIndex)
                            .Insert(billingIndex, billing with
                            {
                                ValuationDate = line.Date,
                                Checked = true,
                                Amount = line.Amount
                            })
                };
            }
        }

        import = null;
        workspace = newWorkspace;

        return new()
        {
            Indexes = [.. indexes]
        };
    }

    private DeleteBillingResult Handle(DeleteBilling q)
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

    private DeleteRepetitiveBillingResult Handle(DeleteRepetitiveBilling q)
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

    private EditBillingResult Handle(EditBilling q)
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

        return new();
    }

    private EditRepetitiveBillingResult Handle(EditRepetitiveBilling q)
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

        return new();
    }

    private GetBillingResult Handle(GetBilling q)
    {
        var index = workspace.Billings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("billing not found");

        return new()
        {
            Index = index
        };
    }

    private GetRepetitiveBillingResult Handle(GetRepetitiveBilling q)
    {
        var index = workspace.RepetitiveBillings.Find(b => b.Id == q.Input.Id);

        if (index < 0)
            throw new Exception("repetitive billing not found");

        return new()
        {
            Index = index
        };
    }

    private GetSummaryResult Handle(GetSummary q)
    {
        return new();
    }

    private InsertNextBillingResult Handle(InsertNextBilling q)
    {
        var index = workspace.RepetitiveBillings.Find(b => b.Id == q.Input.RepetitiveBillingId);

        if (index < 0)
            throw new Exception("repetitive billing not found");

        var repetitiveBilling = workspace.RepetitiveBillings[index];

        var newId = Guid.NewGuid();

        workspace = workspace with
        {
            Billings = workspace.Billings.Add(new()
            {
                Id = newId,
                Amount = repetitiveBilling.Amount,
                Checked = false,
                Comment = "",
                IsSaving = repetitiveBilling.IsSaving,
                Title = repetitiveBilling.Title,
                ValuationDate = repetitiveBilling.NextValuationDate
            }),
            RepetitiveBillings = workspace.RepetitiveBillings
                .RemoveAt(index)
                .Insert(index, repetitiveBilling with
                {
                    NextValuationDate = repetitiveBilling.Frequence switch
                    {
                        LegendaryGuacamole.Models.Common.Frequence.Monthly => repetitiveBilling.NextValuationDate.AddMonths(1),
                        LegendaryGuacamole.Models.Common.Frequence.Bimonthly => repetitiveBilling.NextValuationDate.AddMonths(2),
                        LegendaryGuacamole.Models.Common.Frequence.Quaterly => repetitiveBilling.NextValuationDate.AddMonths(3),
                        LegendaryGuacamole.Models.Common.Frequence.Annual => repetitiveBilling.NextValuationDate.AddMonths(12),
                        _ => throw new NotImplementedException()
                    },
                }),
        };

        return new InsertNextBillingResult
        {
            Index = workspace.Billings.Length - 1
        };
    }

    private ImportFileResult Handle(ImportFile q)
    {
        var data = CreditAgricoleReader.ReadFile(q.Input.FilePath);

        import = new()
        {
            Lines = data.Lines.Select(l =>
            {
                var amount = l.TrnAmt ?? throw new Exception("Amount is missing");
                var date = l.DtPosted ?? throw new Exception("Date is missing");

                var matchings = workspace.Billings
                    .Where(n => Math.Abs(n.Amount - amount) < 0.0001m
                        && n.ValuationDate >= date.AddDays(-5)
                        && n.ValuationDate <= date.AddDays(5))
                    .OrderByDescending(n => n.ValuationDate == date)
                    .Select(n => n.Id)
                    .ToImmutableArray();

                return new Models.Line()
                {
                    Id = l.FitId ?? throw new Exception("ID is missing"),
                    Amount = amount,
                    Date = date,
                    Name = l.Name ?? "???",
                    Matchings = matchings,
                    SelectedIndex = matchings.Length > 0 ? 0 : -1
                };
            }).ToImmutableArray()
        };

        return new()
        {
            Import = import
        };
    }

    private ListBillingsResult Handle(ListBillings _)
    {
        return new();
    }

    private ListRepetitiveBillingsResult Handle(ListRepetitiveBillings _)
    {
        return new();
    }

    private MatchBillingResult Handle(MatchBilling q)
    {
        if (import == null)
            throw new Exception("no import");

        var index = import.Lines.FindIndexWithPredicate(l => l.Id == q.Input.ImportLineId);

        if (index < 0)
            throw new Exception("line not found");

        var importLine = import.Lines[index];

        if (q.Input.BillingId.HasValue)
        {
            var billingIndex = workspace.Billings.FindIndexWithPredicate(b => b.Id == q.Input.BillingId);

            if (billingIndex < 0)
                throw new Exception("billing not found");

            var newSelectedIndex = importLine.Matchings.IndexOf(q.Input.BillingId.Value);
            if (newSelectedIndex < 0)
                importLine = importLine with
                {
                    Matchings = importLine.Matchings.Add(q.Input.BillingId.Value),
                    SelectedIndex = importLine.Matchings.Length
                };
            else
                importLine = importLine with
                {
                    SelectedIndex = newSelectedIndex
                };
        }
        else
            importLine = importLine with
            {
                SelectedIndex = -1
            };

        import = import with
        {
            Lines = import.Lines
                .RemoveAt(index)
                .Insert(index, importLine)
        };

        return new();
    }

    private MultipleInsertNextBillingResult Handle(MultipleInsertNextBilling q)
    {
        var maxDate = q.Input.MaxDate.ToDateOnly();

        var indexes = workspace.RepetitiveBillings
            .Where(b => b.NextValuationDate <= maxDate)
            .Select((_, i) => i)
            .ToArray();

        var newWorkspace = workspace;

        foreach (var index in indexes)
        {
            var repetitiveBilling = workspace.RepetitiveBillings[index];

            var newId = Guid.NewGuid();

            newWorkspace = newWorkspace with
            {
                Billings = newWorkspace.Billings.Add(new()
                {
                    Id = newId,
                    Amount = repetitiveBilling.Amount,
                    Checked = false,
                    Comment = "",
                    IsSaving = repetitiveBilling.IsSaving,
                    Title = repetitiveBilling.Title,
                    ValuationDate = repetitiveBilling.NextValuationDate
                }),
                RepetitiveBillings = newWorkspace.RepetitiveBillings
                    .RemoveAt(index)
                    .Insert(index, repetitiveBilling with
                    {
                        NextValuationDate = repetitiveBilling.Frequence switch
                        {
                            LegendaryGuacamole.Models.Common.Frequence.Monthly => repetitiveBilling.NextValuationDate.AddMonths(1),
                            LegendaryGuacamole.Models.Common.Frequence.Bimonthly => repetitiveBilling.NextValuationDate.AddMonths(2),
                            LegendaryGuacamole.Models.Common.Frequence.Quaterly => repetitiveBilling.NextValuationDate.AddMonths(3),
                            LegendaryGuacamole.Models.Common.Frequence.Annual => repetitiveBilling.NextValuationDate.AddMonths(12),
                            _ => throw new NotImplementedException()
                        },
                    }),
            };
        }

        workspace = newWorkspace;

        return new()
        {
            Indexes = indexes
        };
    }

    private ShowImportResult Handle(ShowImport _)
    {
        return new()
        {
            Import = import ?? new()
            {
                Lines = []
            }
        };
    }

    private ShowImportLineDetailResult Handle(ShowImportLineDetail q)
    {
        if (import == null)
            throw new Exception("no import");

        var index = import.Lines.FindIndexWithPredicate(l => l.Id == q.Input.ImportLineId);

        if (index < 0)
            throw new Exception("line not found");

        return new()
        {
            Import = import,
            Index = index
        };
    }

    private ShowProjectionResult Handle(ShowProjection _)
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