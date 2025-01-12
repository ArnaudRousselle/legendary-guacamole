using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class ShowImportLineDetail : WorkspaceQuery<ShowImportLineDetailInput, ShowImportLineDetailResult, ShowImportLineDetailOutput>
{
    public override ShowImportLineDetailOutput Map(Workspace workspace, ShowImportLineDetailResult result)
    {
        var line = result.Import.Lines[result.Index];
        return new()
        {
            Amount = line.Amount,
            Date = new()
            {
                Day = line.Date.Day,
                Month = line.Date.Month,
                Year = line.Date.Year
            },
            SelectedIndex = line.SelectedIndex,
            Title = line.Name,
            Candidates = line.Matchings
                .Select(billingId =>
                {
                    var billing = workspace.Billings.First(b => b.Id == billingId);
                    return new ShowImportLineDetailOutput.Billing
                    {
                        Id = billing.Id,
                        Title = billing.Title,
                        ValuationDate = new()
                        {
                            Day = billing.ValuationDate.Day,
                            Month = billing.ValuationDate.Month,
                            Year = billing.ValuationDate.Year
                        },
                    };
                })
                .ToArray()
        };
    }
}

public class ShowImportLineDetailResult
{
    public required Import Import { get; init; }
    public required int Index { get; set; }
}