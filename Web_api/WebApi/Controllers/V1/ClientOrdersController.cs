using System.Text.Json;
using Application.ClientOrderPayments.Commands.ReceiveClientOrderPaymentPayload;
using Application.ClientOrderPayments.Commands.SendClientOrderPaymentPayload;
using Application.Features.ClientOrders.Commands.AddNewItem;
using Application.Features.ClientOrders.Commands.AdjustItemQuantity;
using Application.Features.ClientOrders.Commands.ApplyPromoCode;
using Application.Features.ClientOrders.Commands.CancelClientOrder;
using Application.Features.ClientOrders.Commands.CancelPromoCode;
using Application.Features.ClientOrders.Commands.CreateNewOrder;
using Application.Features.ClientOrders.Commands.DeleteItem;
using Application.Features.ClientOrders.Commands.UpdateGuestUserOrder;
using Application.Features.ClientOrders.Commands.UpdateLocationForClientOrder;
using Application.Features.ClientOrders.Queries.GetClientOrderById;
using Application.Features.ClientOrders.Queries.GetClientOrderCountByPromotionalId;
using Application.Features.ClientOrders.Queries.GetClientOrderDetailsById;
using Application.Features.ClientOrders.Queries.GetClientOrderImagesById;
using Application.Features.ClientOrders.Queries.GetClientOrderReportByPromotionalId;
using Application.Features.ClientOrders.Queries.GetMyCartOrder;
using Application.Features.ClientOrders.Queries.GetMyCartOrderDetails;
using Application.Features.ClientOrders.Queries.ViewMyOrders;
using Application.RatesAndFeedbacks.Commands.SetRateAndFeedback;
using DocumentFormat.OpenXml.InkML;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.V1;

public class ClientOrdersController : BaseController
{
	[HttpGet]
	[Authorize]
	public async Task<ActionResult<GetClientOrderByIdDto>> GetClientOrderById([FromQuery] GetClientOrderByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var list = await Mediator.Send(query).ConfigureAwait(false);
		return Ok(list);
	}

	[HttpGet]
	// [Authorize]
	public async Task<ActionResult<GetMyCartOrderDto>> GetMyCartOrder([FromQuery] GetMyCartOrderQuery query)
		=> Ok(await Mediator.Send(query).ConfigureAwait(false));


	[HttpGet]
	// [Authorize]
	public async Task<ActionResult<GetMyCartOrderDetailsVm>> GetMyCartOrderDetails(
		[FromQuery] GetMyCartOrderDetailsQuery query)
		=> Ok(await Mediator.Send(query).ConfigureAwait(false));


	[HttpGet]
	[Authorize]
	public async Task<ActionResult<ViewMyOrdersVm>> ViewMyOrders([FromQuery] ViewMyOrdersQuery query)
	{
		var list = await Mediator.Send(query).ConfigureAwait(false);
		return Ok(list);
	}

	[HttpGet]
	[Authorize]
	public async Task<ActionResult<GetClientOrderDetailsByIdVm>> GetClientOrderDetailsById(
		[FromQuery] GetClientOrderDetailsByIdQuery query)
	{
		var list = await Mediator.Send(query).ConfigureAwait(false);
		return Ok(list);
	}

	//

	[Authorize]
	[HttpGet]
	public async Task<ActionResult<GetClientOrderImagesByIdVm>> GetClientOrderImagesById(
		[FromQuery] GetClientOrderImagesByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var list = await Mediator.Send(query).ConfigureAwait(false);
		return Ok(list);
	}

	[HttpPost]
	// [Authorize]
	public async Task<ActionResult> AddNewItem([FromBody] AddNewItemCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPost]
	public async Task<ActionResult> CreateNewOrder([FromBody] CreateNewOrderCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
	// [Authorize]
	public async Task<ActionResult> AdjustItemQuantity([FromBody] AdjustItemQuantityCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
	[Authorize]
	public async Task<ActionResult> UpdateLocationForClientOrder([FromBody] UpdateLocationForClientOrderCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	// [Authorize]
	public async Task<ActionResult> DeleteItem([FromQuery] DeleteItemCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	// [Authorize]
	public async Task<ActionResult> CancelClientOrder([FromQuery] CancelClientOrderCommand command)
	{
		// if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}


	[HttpPut]
	[Authorize]
	public async Task<ActionResult> ApplyPromoCode([FromBody] ApplyPromoCodeCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        
        //if (command.UserAgent != null && HttpContext.Request.Headers.TryGetValue(HeaderNames.UserAgent, out var userAgent))
        //{
        //    command.UserAgent = userAgent;
        //}
		
        await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	// [Authorize]
	public async Task<ActionResult> CancelPromoCode([FromQuery] CancelPromoCodeCommand command)
	{
		// if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}


	[HttpPut]
	[Authorize]
	public async Task<ActionResult> AddRateAndFeedback([FromBody] SetRateAndFeedbackCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	//calling from mobile to set payload
	[HttpPost]
	[Authorize]
	public async Task<ActionResult> SendClientOrderPaymentPayload(
		[FromBody] SendClientOrderPaymentPayloadCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		return Ok(await Mediator.Send(command).ConfigureAwait(false));
	}

	//calling after payment done in mobile
	[HttpPut]
	[Authorize]
	public async Task<ActionResult> ReceiveClientOrderPaymentPayload(
		[FromBody] ReceiveClientOrderPaymentPayloadCommand command)
	{
        //if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		//await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

    [HttpGet]
    [Authorize]
    [SwaggerResponse(StatusCodes.Status200OK, "Get client order by promotional link id", typeof(PagedList<GetClientOrderReportByPromotionalIdResponse>))]
    public async Task<ActionResult> GetClientOrderReportByPromotionalId([FromQuery] GetClientOrderReportByPromotionalIdQuery query)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        var result = await Mediator.Send(query).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    [SwaggerResponse(StatusCodes.Status200OK, "Get client orders count by promotional link id", typeof(GetClientOrderCountByPromotionalIdQuery))]
    public async Task<ActionResult> GetClientOrderCountByPromotionalId([FromQuery] GetClientOrderCountByPromotionalIdQuery query)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        var result = await Mediator.Send(query).ConfigureAwait(false);
        return Ok(result);
    }

	[HttpPost]
    [Authorize]
    public async Task<ActionResult> UpdateGuestUserOrder([FromBody] UpdateGuestUserOrderCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        var result = await Mediator.Send(command).ConfigureAwait(false);
        return Ok();
    }


}
