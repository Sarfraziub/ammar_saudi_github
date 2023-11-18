using Application.Features.ContentSettings.Commands.UpdateContentSettings;
using Application.Features.ContentSettings.Queries.GetContentSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class ContentSettingsController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetContentSettingsDto>> GetContentSettings()
		=> Ok(await Mediator.Send(new GetContentSettingsQuery()).ConfigureAwait(false));

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> UpdateContentSettings([FromBody] UpdateContentSettingsCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}
