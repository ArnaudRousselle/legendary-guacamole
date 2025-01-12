using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.Models.Dtos;

public class ShowImportLineDetailInput
{
    public required string ImportLineId { get; set; }
}

public class ShowImportLineDetailOutput
{
    public class Billing
    {
        public required Guid Id { get; set; }
        public required ShortDate ValuationDate { get; set; }
        public required string Title { get; set; }
    }
    public required ShortDate Date { get; set; }
    public required decimal Amount { get; set; }
    public required string Title { get; set; }
    public required Billing[] Candidates { get; set; }
    public required int SelectedIndex { get; set; }
}