using LegendaryGuacamole.WebApi.Channels;

namespace LegendaryGuacamole.WebApi.Services;

public class WorkspaceService(WorkspaceChannel channel, ILogger<WorkspaceService> logger)
    : BackgroundService
{
    private readonly List<Models.Billing> billings = [];
    private readonly List<Models.RepetitiveBilling> repetitiveBillings = [];
    //todo ARNAUD: utiliser un state de type record et le passer aux queries pour qu'elles le déserialisent

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const string fileName = "./data.lgc";

        Models.Workspace workspace = File.Exists(fileName)
            ? System.Text.Json.JsonSerializer.Deserialize<Models.Workspace>(File.ReadAllText(fileName))!
            : new();

        billings.AddRange(workspace.Billings);
        repetitiveBillings.AddRange(workspace.RepetitiveBillings);

        //todo ARNAUD: à supprimer
        for (var i = 0; i < 100; i++)
            billings.Add(new()
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
                    object output = message switch
                    {
                        AddBilling.Query q => Handle(q.Input),
                        DeleteBilling.Query q => Handle(q.Input),
                        EditBilling.Query q => Handle(q.Input),
                        GetBilling.Query q => Handle(q.Input),
                        GetSummary.Query q => Handle(q.Input),
                        ListBillings.Query q => Handle(q.Input),
                        SetChecked.Query q => Handle(q.Input),
                        _ => throw new NotImplementedException()
                    };
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

        Models.Billing b = new()
        {
            Id = newId,
            Amount = input.Amount,
            Checked = input.Checked,
            Comment = input.Comment,
            IsArchived = input.IsArchived,
            IsSaving = input.IsSaving,
            Title = input.Title,
            ValuationDate = input.ValuationDate.ToDateOnly()
        };

        billings.Add(b);

        return new AddBilling.Output
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

    public DeleteBilling.Output Handle(DeleteBilling.Input input)
    {
        var billing = billings.SingleOrDefault(b => b.Id == input.Id);

        if (billing == null)
            throw new Exception("billing not found");

        billings.Remove(billing);

        return new() { HasBeenDeleted = false };
    }

    public EditBilling.Output Handle(EditBilling.Input input)
    {
        var b = billings.SingleOrDefault(b => b.Id == input.Id);

        if (b == null)
            throw new Exception("billing not found");

        b.Amount = input.Amount;
        b.Checked = input.Checked;
        b.Comment = input.Comment;
        b.IsArchived = input.IsArchived;
        b.IsSaving = input.IsSaving;
        b.Title = input.Title;
        b.ValuationDate = input.ValuationDate.ToDateOnly();

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

    public GetSummary.Output[] Handle(GetSummary.Input input)
        => billings.Select(b => new GetSummary.Output()
        {
            ValuationDate = b.ValuationDate,
            Amount = b.Amount
        }).ToArray();

    public ListBillings.Output[] Handle(ListBillings.Input input)
    {
        var query = billings.AsQueryable();

        if (!string.IsNullOrEmpty(input.Title))
            query = query.Where(n => n.Title.ToLower().Contains(input.Title.ToLower()));
        if (input.Amount.HasValue)
            query = query.Where(n => input.Amount - (input.DeltaAmount ?? 0) <= n.Amount
                && n.Amount <= input.Amount + (input.DeltaAmount ?? 0));
        if (input.StartDate != null)
            query = query.Where(n => input.StartDate.ToDateOnly() <= n.ValuationDate);
        if (input.EndDate != null)
            query = query.Where(n => n.ValuationDate <= input.EndDate.ToDateOnly());
        if (!(input.WithArchived ?? false))
            query = query.Where(n => !n.IsArchived);

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

    private SetChecked.Output Handle(SetChecked.Input input)
    {
        var b = billings.SingleOrDefault(b => b.Id == input.Id);

        if (b == null)
            throw new Exception("billing not found");

        b.Checked = input.Checked;

        return new()
        {
            Id = b.Id,
            Checked = b.Checked
        };
    }

}