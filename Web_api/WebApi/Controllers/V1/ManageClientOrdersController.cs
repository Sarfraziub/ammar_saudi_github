using Application.DriverOrders.Queries.GetDriverOrders;
using Application.DriverOrders.Queries.GetUnassignedDriverOrders;
using Application.Features.ClientOrders.Queries.GetClientOrderDetailsById;
using Application.Features.ClientOrders.Queries.GetClientOrdersByClientId;
using Application.Features.ClientOrders.Queries.ViewClientOrders;
using Application.ManageClientOrders.Commands.AssignDriverForClientOrder;
using Application.ManageClientOrders.Commands.AssignLocationForClientOrder;
using Application.ManageClientOrders.Commands.UnassignDriverForClientOrder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize(Roles = "Admin")]
public class ManageClientOrdersController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<ViewClientOrdersVm>> ViewClientOrders([FromQuery] ViewClientOrdersQuery query)
	{
		var list = await Mediator.Send(query).ConfigureAwait(false);
		return Ok(list);
	}

	[HttpGet]
	public async Task<ActionResult<GetClientOrderDetailsByIdVm>> GetClientOrderDetailsById(
		[FromQuery] GetClientOrderDetailsByIdQuery query)
	{
		var list = await Mediator.Send(query).ConfigureAwait(false);
		return Ok(list);
	}

	[HttpGet]
	public async Task<ActionResult<GetDriverOrdersVm>> GetUnassignedDriver([FromQuery] GetUnassignedDriverOrdersQuery query)
	{
		return Ok(await Mediator.Send(query).ConfigureAwait(false));
	}
	[HttpGet]
	public async Task<ActionResult<GetClientOrdersByClientIdVm>> GetClientOrdersByClientId(
		[FromQuery] GetClientOrdersByClientIdQuery query)
	{
		var list = await Mediator.Send(query).ConfigureAwait(false);
		return Ok(list);
	}


	[HttpPut]
	public async Task<ActionResult> AssignLocationForClientOrder([FromBody] AssignLocationForClientOrderCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
	public async Task<ActionResult> AssignDriverForClientOrder([FromBody] AssignDriverForClientOrderCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
	
	[HttpPut]
	public async Task<ActionResult> UnassignDriverForClientOrder([FromBody] UnassignDriverForClientOrderCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}


