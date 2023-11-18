using Application.DriverClaims.Queries.GetClientOrdersByDriverClaimId;
using Application.ManageClaims.Commands.CloseClaim;
using Application.ManageClaims.Queries.GetClaimById;
using Application.ManageClaims.Queries.GetClaims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize(Roles = "Admin")]
public class ManageClaimsController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetClaimsVm>> GetClaims([FromQuery] GetClaimsQuery query)
		=> Ok(await Mediator.Send(query).ConfigureAwait(false));

	[HttpGet]
	public async Task<ActionResult<GetClientOrdersByDriverClaimIdVm>> GetClientOrdersByDriverClaimId(
		[FromQuery] GetClientOrdersByDriverClaimIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		return Ok(await Mediator.Send(query).ConfigureAwait(false));
	}
	[HttpGet]
	public async Task<ActionResult<GetClaimByIdDto>> GetClaimById([FromQuery] GetClaimByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		return Ok(await Mediator.Send(query).ConfigureAwait(false));
	}


	[HttpPut]
	public async Task<ActionResult> CloseClaim([FromBody] CloseClaimCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}
