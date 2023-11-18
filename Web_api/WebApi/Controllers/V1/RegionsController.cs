using Application.Regions.Commands.AddRegion;
using Application.Regions.Commands.DeleteRegion;
using Application.Regions.Commands.UpdateRegion;
using Application.Regions.Queries.GetRegionById;
using Application.Regions.Queries.GetRegions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class RegionsController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<RegionsVm>> GetRegions()
		=> Ok(await Mediator.Send(new GetRegionsQuery()).ConfigureAwait(false));


	[HttpGet]
	public async Task<ActionResult<GetRegionByIdDto>> GetById([FromQuery] GetRegionByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		return Ok(await Mediator.Send(query).ConfigureAwait(false));
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Add([FromBody] AddRegionCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(command).ConfigureAwait(false);
		// return Ok(newId);
		return NoContent();
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Update([FromBody] UpdateRegionCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Delete([FromQuery] DeleteRegionCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		// return Ok();
		return NoContent();
	}
}
