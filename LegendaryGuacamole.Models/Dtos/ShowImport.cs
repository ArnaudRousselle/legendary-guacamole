using LegendaryGuacamole.Models.Common;

namespace LegendaryGuacamole.Models.Dtos;

public class ShowImportInput
{
    public string FilePath { get; set; } = "";
}

public class ShowImportOutput
{
    public class ImportLine
    {
        public required string Id { get; set; }
        public required ShortDate ValuationDate { get; set; }
        public required decimal Amount { get; set; }
        public required string Title { get; set; }
    }

    public class Billing
    {
        public required Guid BillingId { get; set; }
        public required ShortDate ValuationDate { get; set; }
        public required string Title { get; set; }
    }

    public class Tuple
    {
        public required ImportLine ImportLine { get; set; }
        public required Billing? CurrentBilling { get; set; }
        public required int MatchingCount { get; set; }
    }

    public required Tuple[] Tuples { get; set; }
}