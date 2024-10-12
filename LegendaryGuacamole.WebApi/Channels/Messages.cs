namespace LegendaryGuacamole.WebApi.Channels;

public class AddBilling : WorkspaceQuery
{
    public required Dtos.Billing Billing { get; init; }
}

public class DeleteBilling : WorkspaceQuery
{
    public required Guid BillingId { get; init; }
}

public class EditBilling : WorkspaceQuery
{
    public required Dtos.Billing Billing { get; init; }
}

public class ListBillings() : WorkspaceQuery<Dtos.Billing[]>
{
}