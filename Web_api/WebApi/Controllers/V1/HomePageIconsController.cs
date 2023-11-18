using Application.HomePageIcons.Commands.UpdateHomePageIcon;
using Application.HomePageIcons.Queries.GetHomePageIcons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class HomePageIconsController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetHomePageIconsVm>> GetHomePageIcons()
		=> Ok(await Mediator.Send(new GetHomePageIconsQuery()).ConfigureAwait(false));

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> UpdateHomePageIcon([FromBody] UpdateHomePageIconCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}
