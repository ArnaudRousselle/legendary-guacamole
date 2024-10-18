namespace LegendaryGuacamole.WebApi.Channels;

public record Empty { }

public class AddBilling : WorkspaceQuery<Dtos.Billing, Empty> { }
public class DeleteBilling : WorkspaceQuery<Guid, Empty> { }
public class EditBilling : WorkspaceQuery<Dtos.Billing, Empty> { }
public class ListBillings() : WorkspaceListingQuery<Empty, Dtos.Billing[]> { }