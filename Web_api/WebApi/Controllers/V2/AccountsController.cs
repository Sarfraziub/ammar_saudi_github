using Application.User.Commands.Access.V2;
using Application.User.Commands.Login.V2;
using Application.User.Commands.SendPhoneCodeToken.V2;
using Microsoft.AspNetCore.Mvc;
using LoginResult = Application.User.Commands.RefreshTokenRequest.LoginResult;

namespace WebApi.Controllers.V2;

[ApiVersion("2.0")]
public class AccountsController : BaseController
{
	[HttpPost]
	[ProducesDefaultResponseType]
	public async Task<ActionResult> Access(
		[FromBody] AccessCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPost]
	[ProducesDefaultResponseType]
	public async Task<ActionResult<LoginResult>> Login(
		[FromBody] LoginCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var jwt = await Mediator.Send(command).ConfigureAwait(false);
		return Ok(jwt);
	}



	[HttpPost]
	[ProducesDefaultResponseType]
	public async Task<ActionResult<LoginResult>> SendOtp(
		[FromBody] SendPhoneCodeConfirmationCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}


