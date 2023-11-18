using Application.NotificationTemplates.Commands.AddNotificationTemplate;
using Application.NotificationTemplates.Commands.DeleteNotificationTemplate;
using Application.NotificationTemplates.Commands.SendNotificationTemplate;
using Application.NotificationTemplates.Commands.UpdateNotificationTemplate;
using Application.NotificationTemplates.Queries.GetNotificationTemplateById;
using Application.NotificationTemplates.Queries.GetNotificationTemplates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize(Roles = "Admin")]
public class NotificationTemplatesController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetNotificationTemplatesVm>> GetNotificationTemplates()
	{
		var list = await Mediator.Send(new GetNotificationTemplatesQuery()).ConfigureAwait(false);
		// if (list != null && list.Regions.Count > 0)
		return Ok(list);
		// return NotFound();
	}

	[HttpGet]
	public async Task<ActionResult<GetNotificationTemplateByIdDto>> GetNotificationTemplateById(
		[FromQuery] GetNotificationTemplateByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var item = await Mediator.Send(query).ConfigureAwait(false);
		if (item != null)
			return Ok(item);
		return NotFound();
	}


	[HttpPost]
	public async Task<ActionResult> AddNotificationTemplate([FromBody] AddNotificationTemplateCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(command).ConfigureAwait(false);
		// return Ok(newId);
		return NoContent();
	}

	[HttpPost]
	public async Task<ActionResult> SendNotificationTemplate([FromBody] SendNotificationTemplateCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(command).ConfigureAwait(false);
		// return Ok(newId);
		return NoContent();
	}

	[HttpPut]
	public async Task<ActionResult> UpdateNotificationTemplate([FromBody] UpdateNotificationTemplateCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	public async Task<ActionResult> DeleteNotificationTemplate([FromQuery] DeleteNotificationTemplateCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		// return Ok();
		return NoContent();
	}
}


