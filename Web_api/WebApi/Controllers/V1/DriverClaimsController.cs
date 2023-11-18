using Application.DriverClaims.Command.CancelDriverClaim;
using Application.DriverClaims.Command.RequestDriverClaim;
using Application.DriverClaims.Queries.GetClientOrdersByDriverClaimId;
using Application.DriverClaims.Queries.GetDriverClaims;
using Application.DriverClaims.Queries.GetUnclaimedClientOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize]
public class DriverClaimsController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<GetDriverClaimsVm>> GetDriverClaims()
        =>
            Ok(await Mediator.Send(new GetDriverClaimsQuery()).ConfigureAwait(false));


    [HttpGet]
    public async Task<ActionResult<GetClientOrdersByDriverClaimIdVm>> GetClientOrdersByDriverClaimId(
        [FromQuery] GetClientOrdersByDriverClaimIdQuery query)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        return Ok(await Mediator.Send(query).ConfigureAwait(false));
    }

    [HttpGet]
    public async Task<ActionResult<GetUnclaimedClientOrdersVm>> GetUnclaimedClientOrders()
        =>
            Ok(await Mediator.Send(new GetUnclaimedClientOrdersQuery()).ConfigureAwait(false));



    [HttpPost]
    public async Task<ActionResult> Add()
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        var newId = await Mediator.Send(new RequestDriverClaimCommand()).ConfigureAwait(false);
        // return Ok(newId);
        return NoContent();
    }


    [HttpDelete]
    public async Task<ActionResult> Delete([FromQuery] CancelDriverClaimCommand command)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        await Mediator.Send(command).ConfigureAwait(false);
        // return Ok();
        return NoContent();
    }


}
