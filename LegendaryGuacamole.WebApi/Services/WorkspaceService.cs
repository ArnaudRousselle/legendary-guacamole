using LegendaryGuacamole.WebApi.Channels;

namespace LegendaryGuacamole.WebApi.Services;

public class WorkspaceService(WorkspaceChannel channel, ILogger<WorkspaceService> logger)
    : BackgroundService,
    AddBilling.Query.IHandler,
    DeleteBilling.Query.IHandler,
    EditBilling.Query.IHandler,
    GetBilling.Query.IHandler,
    ListBillings.Query.IHandler
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
                    object output = ((dynamic)this).Handle(((dynamic)message).Input);
                    await message.OnSuccess(output);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.ToString());
                    await message.OnError();
                }
            }
        }
    }

    public AddBilling.Output Handle(AddBilling.Input input)
    {
        var newId = Guid.NewGuid();
        billings.Add(new()
        {
            Id = newId,
            Amount = input.Amount,
            Checked = input.Checked,
            Comment = input.Comment,
            IsArchived = input.IsArchived,
            IsSaving = input.IsSaving,
            Title = input.Title,
            ValuationDate = input.ValuationDate.ToDateOnly()
        });

        return new()
        {
            NewId = newId
        };
    }

    public DeleteBilling.Output Handle(DeleteBilling.Input input)
    {
        var billing = billings.SingleOrDefault(b => b.Id == input.Id);

        if (billing == null)
            return new() { HasBeenDeleted = false };

        billings.Remove(billing);

        return new() { HasBeenDeleted = false };
    }

    public EditBilling.Output Handle(EditBilling.Input input)
    {
        var billing = billings.SingleOrDefault(b => b.Id == input.Id);

        if (billing == null)
            return new() { HasBeenEdited = false };

        billing.Amount = input.Amount;
        billing.Checked = input.Checked;
        billing.Comment = input.Comment;
        billing.IsArchived = input.IsArchived;
        billing.IsSaving = input.IsSaving;
        billing.Title = input.Title;
        billing.ValuationDate = input.ValuationDate.ToDateOnly();

        return new() { HasBeenEdited = true };
    }

    public GetBilling.Output Handle(GetBilling.Input input)
    {
        var b = billings.Single(b => b.Id == input.Id);
        return new()
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
        };
    }

    public ListBillings.Output[] Handle(ListBillings.Input input)
    {
        return billings
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
            .ToArray();
    }


}