using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.Models.Dtos;

public class ShowProjectionInput
{
}

public class ShowProjectionOutput
{
    public class Item
    {
        public required ShortDate ValuationDate { get; set; }
        public required decimal Amount { get; set; }
    }

    public required Item[] Items { get; set; }
}
