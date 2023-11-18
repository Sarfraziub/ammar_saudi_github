using Application.User.Commands.AdminAccess.V2;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V2;

[ApiVersion("2.0")]
public class AdminAccountsController : BaseController
{
	[HttpPost]
	[ProducesDefaultResponseType]
	public async Task<ActionResult> Access(
		[FromBody] AdminAccessCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}


