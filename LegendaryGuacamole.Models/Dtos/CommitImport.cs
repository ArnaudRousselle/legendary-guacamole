namespace LegendaryGuacamole.Models.Dtos;

public class CommitImportInput
{
}

public class CommitImportOutput
{
    public required Guid[] BillingsIds { get; set; }
}