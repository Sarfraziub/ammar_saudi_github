using Application.Drivers.Commands.AddDriver;
using Application.Drivers.Commands.UpdateDriver;
using Application.User.Commands.AddNewAdmin;
using Application.User.Commands.LockoutAccount;
using Application.User.Commands.UnlockedAccount;
using Application.User.Queries.GetAdmins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class AdminsController : BaseController
{
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> GetAdmins()
	{
		var list = await Mediator.Send(new GetAdminsQuery()).ConfigureAwait(false);
		return Ok(list);
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> AddNewAdmin([FromBody] AddNewAdminCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPost]
	public async Task<ActionResult> AddDriver([FromBody] AddDriverCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> UpdateDriver([FromBody] UpdateDriverCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
	[Authorize(Roles = "Admin,Driver")]
	public async Task<ActionResult> LockoutAccount([FromBody] LockoutAccountCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
	[Authorize(Roles = "Admin,Driver")]
	public async Task<ActionResult> UnlockedAccount([FromBody] UnlockedAccountCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}
