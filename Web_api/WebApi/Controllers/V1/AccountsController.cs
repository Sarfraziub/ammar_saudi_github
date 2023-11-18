using Application.User.Commands.Access.V1;
using Application.User.Commands.AccessGuestUser;
using Application.User.Commands.Login.V1;
using Application.User.Commands.RefreshTokenRequest;
using Application.User.Commands.SendPhoneCodeToken.V1;
using Application.User.Commands.SendWhatsAppCodeToken;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using LoginResult = Application.User.Commands.RefreshTokenRequest.LoginResult;

namespace WebApi.Controllers.V1;



[ApiVersion("1.0")]
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
    public async Task<ActionResult<LoginResult>> AccessGuestUser([FromBody] AccessGuestUserCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        var result = await Mediator.Send(command).ConfigureAwait(false);
        return Ok(result);
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

    [HttpPost]
	[ProducesDefaultResponseType]
    public async Task<ActionResult<LoginResult>> SendWhatsAppOtp(
		[FromBody] SendWhatsAppCodeTokenCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
    


    [HttpPost]
	[Authorize]
	public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequestModel request)
	{
		try
		{
			if (string.IsNullOrWhiteSpace(request.RefreshToken)) return Unauthorized();

			var command = Mapper.Map<RefreshTokenRequestCommand>(request);
			command.Role = CurrentUserService.Role;
			command.Username = CurrentUserService.Username;
			command.AccessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
			var loginResult = await Mediator.Send(command).ConfigureAwait(false);
			return Ok(loginResult);
		}
		catch (SecurityTokenException e)
		{
			return
				Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
		}
	}
}


