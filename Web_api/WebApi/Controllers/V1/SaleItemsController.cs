using Application.SaleItems.Commands.AddSaleItem;
using Application.SaleItems.Commands.DeleteSaleItem;
using Application.SaleItems.Commands.UpdateSaleItem;
using Application.SaleItems.Queries.GetSaleItemById;
using Application.SaleItems.Queries.GetSaleItems;
using Application.SaleItems.Queries.GetSaleItemsByPackage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class SaleItemsController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetSaleItemsVm>> GetSaleItems([FromQuery] GetSaleItemsQuery query)
	{
		var list = await Mediator.Send(query).ConfigureAwait(false);
		return Ok(list);
	}

	[HttpGet]
	public async Task<ActionResult<GetSaleItemsByPackageVm>> GetSaleItemsByPackage([FromQuery] GetSaleItemsByPackageQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var item = await Mediator.Send(query).ConfigureAwait(false);
		if (item != null)
			return Ok(item);
		return NotFound();
	}

	[HttpGet]
	public async Task<ActionResult<GetSaleItemByIdDto>> GetById([FromQuery] GetSaleItemByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var item = await Mediator.Send(query).ConfigureAwait(false);
		if (item != null)
			return Ok(item);
		return NotFound();
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Add([FromBody] AddSaleItemCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Update([FromBody] UpdateSaleItemCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Delete([FromQuery] DeleteSaleItemCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		// return Ok();
		return NoContent();
	}
}


