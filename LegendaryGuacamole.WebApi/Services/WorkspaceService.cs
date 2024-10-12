using LegendaryGuacamole.WebApi.Channels;

namespace LegendaryGuacamole.WebApi.Services;

public class WorkspaceService(WorkspaceChannel channel) : BackgroundService
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

                switch (message)
                {
                    case AddBilling m:
                        await HandleAsync(m);
                        break;
                    case DeleteBilling m:
                        await HandleAsync(m);
                        break;
                    case EditBilling m:
                        await HandleAsync(m);
                        break;
                    case ListBillings m:
                        await HandleAsync(m);
                        break;
                }
            }
        }
    }

    private async Task HandleAsync(AddBilling m)
    {
        billings.Add(new()
        {
            Id = Guid.NewGuid(),
            Amount = m.Billing.Amount,
            Checked = m.Billing.Checked,
            Comment = m.Billing.Comment,
            IsArchived = m.Billing.IsArchived,
            IsSaving = m.Billing.IsSaving,
            Title = m.Billing.Title,
            ValuationDate = m.Billing.ValuationDate.ToDateOnly()
        });

        await m.OnCompleted(true);
    }

    private async Task HandleAsync(DeleteBilling m)
    {
        var billing = billings.SingleOrDefault(b => b.Id == m.BillingId);

        if (billing == null)
            return;

        billings.Remove(billing);

        await m.OnCompleted(true);
    }

    private async Task HandleAsync(EditBilling m)
    {
        var billing = billings.SingleOrDefault(b => b.Id == m.Billing.Id);

        if (billing == null)
            return;

        billing.Amount = m.Billing.Amount;
        billing.Checked = m.Billing.Checked;
        billing.Comment = m.Billing.Comment;
        billing.IsArchived = m.Billing.IsArchived;
        billing.IsSaving = m.Billing.IsSaving;
        billing.Title = m.Billing.Title;
        billing.ValuationDate = m.Billing.ValuationDate.ToDateOnly();

        await m.OnCompleted(true);
    }

    private async Task HandleAsync(ListBillings m)
    {
        await m.OnCompleted(billings
            .Select(b => new Dtos.Billing(
                b.Id,
                new(b.ValuationDate.Year, b.ValuationDate.Month, b.ValuationDate.Day),
                b.Title,
                b.Amount,
                b.Checked,
                b.Comment,
                b.IsArchived,
                b.IsSaving))
            .ToArray());
    }
}