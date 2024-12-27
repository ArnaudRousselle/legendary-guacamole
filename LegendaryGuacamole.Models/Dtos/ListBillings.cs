using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.Models.Dtos;

public class ListBillingsInput
{
    public required ShortDate? StartDate { get; set; }
    public required ShortDate? EndDate { get; set; }
    public required decimal? Amount { get; set; }
    public required decimal? DeltaAmount { get; set; }
    public required string? Title { get; set; }
    public required bool? WithChecked { get; set; }
}

public class ListBillingsOutput
{
    public class Item
    {
        public required Guid Id { get; set; }
        public required ShortDate ValuationDate { get; set; }
        public required string Title { get; set; }
        public required decimal Amount { get; set; }
        public required bool Checked { get; set; }
        public required string Comment { get; set; }
        public required bool IsSaving { get; set; }
    }

    public required Item[] Items { get; set; }
}

