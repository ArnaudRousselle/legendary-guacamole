namespace LegendaryGuacamole.WebApi.Channels;

public record Empty { }

public class AddBilling : WorkspaceQuery<Dtos.AddBillingInput, Guid> { }
public class DeleteBilling : WorkspaceQuery<Guid, Empty> { }
public class EditBilling : WorkspaceQuery<Dtos.EditBillingInput, Empty> { }
public class ListBillings() : WorkspaceQuery<Empty, Dtos.Billing[]> { }