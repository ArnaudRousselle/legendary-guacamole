using LegendaryGuacamole.Models.Common;
using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class ShowProjection : WorkspaceQuery<ShowProjectionInput, ShowProjectionResult, ShowProjectionOutput>
{
    class Item
    {
        public required decimal Amount { get; set; }
        public required DateOnly Date { get; set; }
    }

    public override ShowProjectionOutput Map(Workspace workspace, ShowProjectionResult result)
    {
        var now = DateOnly.FromDateTime(DateTime.Now);
        var maxDate = now.AddMonths(2);

        var billings = workspace.Billings
            .Select(n => new Item
            {
                Amount = n.Amount,
                Date = n.ValuationDate
            })
            .ToList();

        foreach (var repetitiveBilling in workspace.RepetitiveBillings)
        {
            var nextValuationDate = repetitiveBilling.NextValuationDate;

            while (nextValuationDate <= maxDate)
            {
                billings.Add(new Item
                {
                    Amount = repetitiveBilling.Amount,
                    Date = nextValuationDate
                });

                nextValuationDate = repetitiveBilling.Frequence switch
                {
                    Frequence.Monthly => nextValuationDate.AddMonths(1),
                    Frequence.Bimonthly => nextValuationDate.AddMonths(2),
                    Frequence.Quaterly => nextValuationDate.AddMonths(3),
                    Frequence.Annual => nextValuationDate.AddMonths(12),
                    _ => throw new Exception("Invalid frequence")
                };
            }
        }

        var total = billings
            .Where(n => n.Date < now)
            .Select(n => n.Amount)
            .Sum();

        List<ShowProjectionOutput.Item> res = [];

        foreach (var date in billings
            .Where(n => n.Date >= now)
            .Select(n => n.Date)
            .Distinct()
            .OrderBy(n => n))
        {
            total += billings
                .Where(n => n.Date == date)
                .Select(n => n.Amount)
                .Sum();

            res.Add(new()
            {
                Amount = total,
                ValuationDate = new()
                {
                    Day = date.Day,
                    Month = date.Month,
                    Year = date.Year
                }
            });
        }

        return new()
        {
            Items = [.. res]
        };
    }
}

public class ShowProjectionResult
{
}