using Application.Drivers.Queries.GetMyProfile;
using Application.Features.Clients.Commands.UpdateClientAddress;
using Application.User.Commands.UpdateMyProfile;
using Application.User.Queries.GetMyProfile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize]
public class ProfilesController : BaseController
{
	[HttpGet]
    [Authorize(Roles = ("Admin, User, Finance, Driver"))]
    public async Task<ActionResult<GetMyProfileDto>> GetMyProfile()
	{
		return Ok(await Mediator.Send(new GetMyProfileQuery()).ConfigureAwait(false));
	}
	
	[HttpGet]
	[Authorize(Roles = ("Driver"))]
	public async Task<ActionResult<GetDriverProfileDto>> GetDriverProfile()
	{
		return Ok(await Mediator.Send(new GetDriverProfileQuery()).ConfigureAwait(false));
	}


	[HttpPut]
    [Authorize(Roles = ("Admin, User, Finance, Driver"))]
    public async Task<ActionResult> UpdateMyProfile([FromBody] UpdateMyProfileCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
    [Authorize(Roles = ("Admin, User, Finance, Driver"))]
    public async Task<ActionResult> UpdateClientAddress([FromBody] UpdateClientAddressCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}

