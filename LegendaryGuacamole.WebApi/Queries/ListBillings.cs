using LegendaryGuacamole.Models.Dtos;
using LegendaryGuacamole.WebApi.Channels;
using LegendaryGuacamole.WebApi.Models;

namespace LegendaryGuacamole.WebApi.Queries;

public class ListBillings : WorkspaceQuery<ListBillingsInput, ListBillingsResult, ListBillingsOutput>
{
    public override ListBillingsOutput Map(Workspace workspace, ListBillingsResult evt)
    => new()
    {
        Items = workspace.Billings
        .Where(b =>
        {
            if (!(Input.WithChecked ?? false) && b.Checked)
                return false;
            if (Input.StartDate != null && b.ValuationDate < new DateOnly(Input.StartDate.Year, Input.StartDate.Month, Input.StartDate.Day))
                return false;
            if (Input.EndDate != null && b.ValuationDate > new DateOnly(Input.EndDate.Year, Input.EndDate.Month, Input.EndDate.Day))
                return false;
            if (Input.Amount.HasValue && (Math.Abs(b.Amount - Input.Amount.Value) >= (Input.DeltaAmount ?? 0) + 0.001m))
                return false;
            if (!string.IsNullOrEmpty(Input.Title) && b.Title.IndexOf(Input.Title, StringComparison.CurrentCultureIgnoreCase) < 0)
                return false;
            return true;
        })
        .OrderBy(n => n.ValuationDate)
        .ThenBy(n => n.Id)
        .Select(n => new ListBillingsOutput.Item
        {
            Id = n.Id,
            ValuationDate = new()
            {
                Year = n.ValuationDate.Year,
                Month = n.ValuationDate.Month,
                Day = n.ValuationDate.Day
            },
            Title = n.Title,
            Amount = n.Amount,
            Checked = n.Checked,
            Comment = n.Comment,
            IsSaving = n.IsSaving
        })
        .ToArray()
    };
}

public class ListBillingsResult
{
}
