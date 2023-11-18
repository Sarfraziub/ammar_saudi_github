using Application.User.Commands.DriverAccess;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class DriverAccountsController : BaseController
{
	[HttpPost]
	[ProducesDefaultResponseType]
	public async Task<ActionResult> Access(
		[FromBody] DriverAccessCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}


