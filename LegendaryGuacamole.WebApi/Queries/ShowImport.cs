using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class ShowImport : WorkspaceQuery<ShowImportInput, ShowImportResult, ShowImportOutput>
{
    public override ShowImportOutput Map(Workspace workspace, ShowImportResult result)
    => new()
    {
        Tuples = result.Import.Lines
                .Select(l =>
                {
                    var currentBilling = l.SelectedIndex >= 0
                        ? workspace.Billings.First(b => b.Id == l.Matchings[l.SelectedIndex])
                        : null;
                    return new ShowImportOutput.Tuple
                    {
                        ImportLine = new()
                        {
                            Id = l.Id,
                            Amount = l.Amount,
                            Title = l.Name,
                            ValuationDate = new()
                            {
                                Day = l.Date.Day,
                                Month = l.Date.Month,
                                Year = l.Date.Year,
                            }
                        },
                        CurrentBilling = currentBilling != null
                        ? new ShowImportOutput.Billing
                        {
                            BillingId = currentBilling.Id,
                            Title = currentBilling.Title,
                            ValuationDate = new()
                            {
                                Day = currentBilling.ValuationDate.Day,
                                Month = currentBilling.ValuationDate.Month,
                                Year = currentBilling.ValuationDate.Year,
                            }
                        }
                        : null,
                        MatchingCount = l.Matchings.Length
                    };
                })
                .ToArray()
    };
}

public class ShowImportResult
{
    public required Import Import { get; init; }
}