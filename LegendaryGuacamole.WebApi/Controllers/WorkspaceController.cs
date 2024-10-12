using LegendaryGuacamole.WebApi.Channels;
using Microsoft.AspNetCore.Mvc;

namespace LegendaryGuacamole.WebApi.Controllers;

[ApiController]
public class WorkspaceController(WorkspaceChannel channel) : Controller
{
    [HttpGet]
    [Route("GetBillings")]
    public async Task<Dtos.Billing[]> GetBillings()
    {
        ListBillings list = new();
        await channel.QueryAsync(list);
        var billings = await list.Result;
        return billings ?? [];
    }

    [HttpPut]
    [Route("AddBilling")]
    public async Task AddBilling(Dtos.Billing billing)
    {
        AddBilling addBilling = new()
        {
            Billing = billing
        };
        await channel.QueryAsync(addBilling);
        await addBilling.Result;
    }
}