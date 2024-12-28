using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.Models.Dtos;

public class ListRepetitiveBillingsInput
{
}

public class ListRepetitiveBillingsOutput
{
    public class Item
    {
        public required Guid Id { get; set; }
        public required ShortDate NextValuationDate { get; set; }
        public required string Title { get; set; }
        public required decimal Amount { get; set; }
        public required bool IsSaving { get; set; }
        public required Frequence Frequence { get; set; }
    }

    public required Item[] Items { get; set; }
}

