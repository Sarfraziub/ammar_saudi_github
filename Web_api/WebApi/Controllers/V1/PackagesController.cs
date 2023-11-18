using Application.SaleItemPackages.Commands.AddPackage;
using Application.SaleItemPackages.Commands.DeletePackage;
using Application.SaleItemPackages.Commands.UpdatePackage;
using Application.SaleItemPackages.Queries.GetPackageById;
using Application.SaleItemPackages.Queries.GetPackages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;


public class PackagesController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetPackagesVm>> GetPackages([FromQuery] GetPackagesQuery query)
		=> Ok(await Mediator.Send(query ).ConfigureAwait(false));


	// [HttpGet]
	// public async Task<ActionResult<GetAllPackagesVm>> GetAllPackages()
	// 	=> Ok(await Mediator.Send(new GetAllPackagesQuery()).ConfigureAwait(false));

	[HttpGet]
	public async Task<ActionResult<GetPackageByIdDto>> GetById([FromQuery] GetPackageByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		return Ok(await Mediator.Send(query).ConfigureAwait(false));
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Add([FromBody] AddPackageCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Update([FromBody] UpdatePackageCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Delete([FromQuery] DeletePackageCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		// return Ok();
		return NoContent();
	}
}
