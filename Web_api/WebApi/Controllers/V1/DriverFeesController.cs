using Application.Features.DriverFees.Commands.AddDriverFee;
using Application.Features.DriverFees.Commands.UpdateDriverFee;
using Application.Features.DriverFees.Queries.GetDriverFeeSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize(Roles = "Admin")]
public class DriverFeesController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<GetDriverFeeSettingsDto>> GetDriverFeeSettings()
    {
        return Ok(await Mediator.Send(new GetDriverFeeSettingsQuery()).ConfigureAwait(false));
    }

    [HttpPost]
    public async Task<ActionResult> AddDriverFee([FromBody] AddDriverFeeCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        var newId = await Mediator.Send(command).ConfigureAwait(false);
        return NoContent();
    }

    [HttpPut("UpdateDriverFee")]
    public async Task<ActionResult> UpdateDriverFee([FromBody] UpdateDriverFeeCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        var newId = await Mediator.Send(command).ConfigureAwait(false);
        return NoContent();
    }
}
