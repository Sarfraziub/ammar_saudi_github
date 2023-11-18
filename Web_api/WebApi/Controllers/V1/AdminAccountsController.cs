using Application.User.Commands.AdminAccess.V1;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[ApiVersion("1.0")]
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


