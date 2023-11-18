using Application.UserDeviceTokens.Commands.AddUserDeviceToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize]
public class UserDeviceTokensController : BaseController
{
	[HttpPost]
	public async Task<ActionResult> AddUserDeviceToken([FromBody] AddUserDeviceTokenCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}


