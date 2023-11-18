using Application.UserNotifications.Commands.ClearNotification;
using Application.UserNotifications.Commands.ClearNotifications;
using Application.UserNotifications.Queries.GetUserNotifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class UserNotificationsController : BaseController
{
	[HttpGet]
	[Authorize]
	public async Task<ActionResult<GetUserNotificationsVm>> GetUserNotifications()
		=> Ok(await Mediator.Send(new GetUserNotificationsQuery()).ConfigureAwait(false));

	[HttpDelete]
	[Authorize]
	public async Task<ActionResult> ClearNotification([FromBody] ClearNotificationCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	[Authorize]
	public async Task<ActionResult> ClearNotifications()
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(new ClearNotificationsCommand()).ConfigureAwait(false);
		return NoContent();
	}
}
