using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class ImportFile : WorkspaceQuery<ImportFileInput, ImportFileResult, ImportFileOutput>
{
    public override ImportFileOutput Map(Workspace workspace, ImportFileResult result)
    => new()
    {
        Tuples = result.Import.Lines
                .Select(l =>
                {
                    var currentBilling = l.SelectedIndex >= 0
                        ? workspace.Billings.First(b => b.Id == l.Matchings[l.SelectedIndex])
                        : null;
                    return new ImportFileOutput.Tuple
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
                        ? new ImportFileOutput.Billing
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

public class ImportFileResult
{
    public required Import Import { get; init; }
}