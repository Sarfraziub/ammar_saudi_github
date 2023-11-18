using Application.Drivers.Commands.ChangeDriverActiveStatus;
using Application.Drivers.Commands.UpdateMyProfileAsDriver;
using Application.Drivers.Queries.GetDriverActiveStatus;
using Application.Drivers.Queries.GetDriverById;
using Application.Drivers.Queries.GetDrivers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class DriversController : BaseController
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<GetDriversVm>> GetDrivers([FromQuery] GetDriversQuery query)
    {
        return Ok(await Mediator.Send(query).ConfigureAwait(false));
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<GetDriverByIdDto>> GetDriverById([FromQuery] GetDriverByIdQuery query)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        return Ok(await Mediator.Send(query).ConfigureAwait(false));
    }


    [HttpGet]
    [Authorize(Roles = "Driver")]
    public async Task<ActionResult<bool>> GetDriverActiveStatus()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        return Ok(await Mediator.Send(new GetDriverActiveStatusQuery()).ConfigureAwait(false));
    }

    [HttpPut]
    [Authorize(Roles = "Driver")]
    public async Task<ActionResult> ChangeDriverActiveStatus()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        return Ok(await Mediator.Send(new ChangeDriverActiveStatusCommand()).ConfigureAwait(false));
    }

    [HttpPut]
    [Authorize(Roles = "Driver")]
    public async Task<ActionResult> UpdateMyProfileAsDriver(
        [FromBody] UpdateMyProfileAsDriverCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        return Ok(await Mediator.Send(command).ConfigureAwait(false));
    }
}
