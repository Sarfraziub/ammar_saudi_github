using Application.Features.PromoCodes.Commands.AddPromoCode;
using Application.Features.PromoCodes.Commands.DeletePromoCode;
using Application.Features.PromoCodes.Commands.UpdatePromoCode;
using Application.Features.PromoCodes.Queries.GetPromoCodeById;
using Application.Features.PromoCodes.Queries.GetPromoCodes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize(Roles = "Admin")]
public class PromoCodesController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetPromoCodesVm>> GetPromoCodes()
	{
		var list = await Mediator.Send(new GetPromoCodesQuery()).ConfigureAwait(false);
		return Ok(list);
	}

	[HttpGet]
	public async Task<ActionResult<GetPromoCodeByIdDto>> GetPromoCodeById([FromQuery] GetPromoCodeByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var item = await Mediator.Send(query).ConfigureAwait(false);
		if (item != null)
			return Ok(item);
		return NotFound();
	}

	[HttpPost]
	public async Task<ActionResult> Add([FromBody] AddPromoCodeCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
	public async Task<ActionResult> Update([FromBody] UpdatePromoCodeCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	public async Task<ActionResult> Delete([FromQuery] DeletePromoCodeCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}


