using Application.Location.Commands.AddLocation;
using Application.Location.Commands.AddLocationImage;
using Application.Location.Commands.DeleteLocation;
using Application.Location.Commands.RemoveLocationImage;
using Application.Location.Commands.UpdateLocation;
using Application.Location.Queries.GetLocationById;
using Application.Location.Queries.GetLocations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class LocationsController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<LocationsVm>> GetLocations([FromQuery] GetLocationsQuery query)
	{
		var list = await Mediator.Send(query).ConfigureAwait(false);
		return Ok(list);
	}

	[HttpGet]
	public async Task<ActionResult<GetLocationByIdDto>> GetById([FromQuery] GetLocationByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var item = await Mediator.Send(query).ConfigureAwait(false);
		if (item != null)
			return Ok(item);
		return NotFound();
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Add([FromBody] AddLocationCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(command).ConfigureAwait(false);
		// return Ok(newId);
		return NoContent();
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> UpdateLocation([FromBody] UpdateLocationCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> AddLocationImage([FromBody] AddLocationImageCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Delete([FromQuery] DeleteLocationCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		// return Ok();
		return NoContent();
	}
	[HttpDelete]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> RemoveLocationImage([FromQuery] RemoveLocationImageCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		// return Ok();
		return NoContent();
	}
}


