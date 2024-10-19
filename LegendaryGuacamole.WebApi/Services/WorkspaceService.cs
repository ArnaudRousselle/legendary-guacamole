using LegendaryGuacamole.WebApi.Channels;

namespace LegendaryGuacamole.WebApi.Services;

public class WorkspaceService(WorkspaceChannel channel, ILogger<WorkspaceService> logger) : BackgroundService
{
    private readonly List<Models.Billing> billings = [];
    private readonly List<Models.RepetitiveBilling> repetitiveBillings = [];

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const string fileName = "./data.lgc";

        Models.Workspace workspace = File.Exists(fileName)
            ? System.Text.Json.JsonSerializer.Deserialize<Models.Workspace>(File.ReadAllText(fileName))!
            : new();

        billings.AddRange(workspace.Billings);
        repetitiveBillings.AddRange(workspace.RepetitiveBillings);

        //todo ARNAUD: à supprimer
        billings.Add(new()
        {
            Id = Guid.NewGuid(),
            Amount = 123,
            Checked = true,
            Title = "Mon titre",
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
                        case AddBilling.Query m:
                            await HandleAsync(m);
                            break;
                        case DeleteBilling.Query m:
                            await HandleAsync(m);
                            break;
                        case EditBilling.Query m:
                            await HandleAsync(m);
                            break;
                        case GetBilling.Query m:
                            await HandleAsync(m);
                            break;
                        case ListBillings.Query m:
                            await HandleAsync(m);
                            break;
                        default:
                            logger.LogWarning($"{message.GetType().Name} not implemented");
                            await message.OnError();
                            break;
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

    private async Task HandleAsync(AddBilling.Query m)
    {
        var newId = Guid.NewGuid();
        billings.Add(new()
        {
            Id = newId,
            Amount = m.Input.Amount,
            Checked = m.Input.Checked,
            Comment = m.Input.Comment,
            IsArchived = m.Input.IsArchived,
            IsSaving = m.Input.IsSaving,
            Title = m.Input.Title,
            ValuationDate = m.Input.ValuationDate.ToDateOnly()
        });

        await m.OnSuccess(newId);
    }

    private async Task HandleAsync(DeleteBilling.Query m)
    {
        var billing = billings.SingleOrDefault(b => b.Id == m.Input);

        if (billing == null)
        {
            await m.OnSuccess(false);
            return;
        }

        billings.Remove(billing);

        await m.OnSuccess(true);
    }

    private async Task HandleAsync(EditBilling.Query m)
    {
        var billing = billings.SingleOrDefault(b => b.Id == m.Input.Id);

        if (billing == null)
        {
            await m.OnSuccess(false);
            return;
        }

        billing.Amount = m.Input.Amount;
        billing.Checked = m.Input.Checked;
        billing.Comment = m.Input.Comment;
        billing.IsArchived = m.Input.IsArchived;
        billing.IsSaving = m.Input.IsSaving;
        billing.Title = m.Input.Title;
        billing.ValuationDate = m.Input.ValuationDate.ToDateOnly();

        await m.OnSuccess(true);
    }

    private async Task HandleAsync(GetBilling.Query m)
    {
        var b = billings.Single(b => b.Id == m.Input);
        await m.OnSuccess(new()
        {
            Id = b.Id,
            ValuationDate = new()
            {
                Year = b.ValuationDate.Year,
                Month = b.ValuationDate.Month,
                Day = b.ValuationDate.Day
            },
            Title = b.Title,
            Amount = b.Amount,
            Checked = b.Checked,
            Comment = b.Comment,
            IsArchived = b.IsArchived,
            IsSaving = b.IsSaving
        });
    }

    private async Task HandleAsync(ListBillings.Query m)
    {
        await m.OnSuccess(billings
            .Select(b => new ListBillings.Output
            {
                Id = b.Id,
                ValuationDate = new()
                {
                    Year = b.ValuationDate.Year,
                    Month = b.ValuationDate.Month,
                    Day = b.ValuationDate.Day
                },
                Title = b.Title,
                Amount = b.Amount,
                Checked = b.Checked,
                Comment = b.Comment,
                IsArchived = b.IsArchived,
                IsSaving = b.IsSaving
            })
            .ToArray());
    }
}