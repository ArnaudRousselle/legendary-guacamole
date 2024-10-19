
namespace LegendaryGuacamole.WebApi.Channels;

public class AddBilling : WorkspaceQuery<Dtos.AddBillingInput, Guid> { }
public class DeleteBilling : WorkspaceQuery<Guid, bool> { }
public class EditBilling : WorkspaceQuery<Dtos.EditBillingInput, bool> { }
public class ListBillings() : WorkspaceQuery<Dtos.Billing[]> { }