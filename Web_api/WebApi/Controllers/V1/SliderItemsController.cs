using Application.SliderItems.Commands.AddSliderItem;
using Application.SliderItems.Commands.DeleteSliderItem;
using Application.SliderItems.Commands.UpdateSliderItem;
using Application.SliderItems.Queries.GetSliderItemById;
using Application.SliderItems.Queries.GetSliderItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class SliderItemsController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetSliderItemsVm>> GetSliderItems()
	{
		var list = await Mediator.Send(new GetSliderItemsQuery()).ConfigureAwait(false);
		return Ok(list);
	}

	[HttpGet]
	public async Task<ActionResult<GetSliderItemByIdDto>> GetSliderItemById([FromQuery] GetSliderItemByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var item = await Mediator.Send(query).ConfigureAwait(false);
		if (item != null)
			return Ok(item);
		return NotFound();
	}


	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Add([FromBody] AddSliderItemCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(command).ConfigureAwait(false);
		// return Ok(newId);
		return NoContent();
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Update([FromBody] UpdateSliderItemCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Delete([FromQuery] DeleteSliderItemCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		// return Ok();
		return NoContent();
	}
}


