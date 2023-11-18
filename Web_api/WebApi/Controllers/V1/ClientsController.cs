using Application.Features.Clients.Queries.GetClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize(Roles = "Admin")]
public class ClientsController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetClientsVm>> GetClients([FromQuery] GetClientsQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var list = await Mediator.Send(query).ConfigureAwait(false);
		return Ok(list);
	}
}


