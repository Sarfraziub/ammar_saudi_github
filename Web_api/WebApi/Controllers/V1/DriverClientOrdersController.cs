using Application.DriverOrders.Commands.AcceptClientOrder;
using Application.DriverOrders.Commands.DeliverOrder;
using Application.DriverOrders.Commands.RejectClientOrder;
using Application.DriverOrders.Queries.GetDriverCompleteOrders;
using Application.DriverOrders.Queries.GetDriverOrders;
using Application.DriverOrders.Queries.GetFees;
using Application.DriverOrders.Queries.GetTodayDashboard;
using Application.DriverOrders.Queries.GetUnassignedDriverOrders;
using Application.DriverOrders.Queries.GetUnpaidDetails;
using Application.DriverOrders.Queries.GetWeeklyDashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize(Roles = "Driver")]
public class DriverClientOrdersController : BaseController
{

	[HttpGet]
	public async Task<ActionResult<GetTodayDashboardVm>> GetTodayDashboard()
		=> Ok(await Mediator.Send(new GetTodayDashboardQuery()).ConfigureAwait(false));
	[HttpGet]
	public async Task<ActionResult<GetWeeklyDashboardVm>> GetWeeklyDashboard()
		=> Ok(await Mediator.Send(new GetWeeklyDashboardQuery()).ConfigureAwait(false));

    [HttpGet]
    public async Task<ActionResult<GetDriverOrdersVm>> GetDriverOrders([FromQuery] GetDriverOrdersQuery query)
	    => Ok(await Mediator.Send(query).ConfigureAwait(false));


    [HttpGet]
    public async Task<ActionResult<GetTodaysDriverCompleteOrdersVm>> GetDriverCompleteOrders([FromQuery] GetTodaysDriverCompleteOrdersQuery query)
	    => Ok(await Mediator.Send(query).ConfigureAwait(false));

    [HttpGet]
    public async Task<ActionResult<GetUnpaidClaimsQueryVm>> GetUnpaidDetails()
	    => Ok(await Mediator.Send(new GetUnpaidDetailsQuery()).ConfigureAwait(false));


    [HttpPut]
    public async Task<ActionResult> AcceptClientOrder([FromBody] AcceptClientOrderCommand command)
    {
	    if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
	    await Mediator.Send(command).ConfigureAwait(false);
	    return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> RejectClientOrder([FromBody] RejectClientOrderCommand command)
    {
	    if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
	    await Mediator.Send(command).ConfigureAwait(false);
	    return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> DeliverOrder([FromBody] DeliverOrderCommand command)
    {
	    if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
	    await Mediator.Send(command).ConfigureAwait(false);
	    return NoContent();
    }


    [HttpGet]
    public async Task<ActionResult<GetFeesDto>> GetFees()
	    => Ok(await Mediator.Send(new GetFeesQuery()).ConfigureAwait(false));

    [HttpGet]
    public async Task<ActionResult<GetDriverOrdersVm>> GetUnassignedDriver([FromQuery] GetUnassignedDriverOrdersQuery query)
    => Ok(await Mediator.Send(query).ConfigureAwait(false));

}
