using Application.InfluencerVideos.Commands.AddInfluencerVideos;
using Application.InfluencerVideos.Commands.DeleteInfluencerVideos;
using Application.InfluencerVideos.Commands.UpdateInfluencerVideos;
using Application.InfluencerVideos.Queries.GetInfluencerVideoById;
using Application.InfluencerVideos.Queries.GetInfluencerVideos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class InfluencerVideosController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetInfluencerVideosVm>> GetInfluencerVideos()
		=> Ok(await Mediator.Send(new GetInfluencerVideosQuery()).ConfigureAwait(false));


	[HttpGet]
	public async Task<ActionResult<GetInfluencerVideoByIdDto>> GetById([FromQuery] GetInfluencerVideoByIdQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		return Ok(await Mediator.Send(query).ConfigureAwait(false));
	}


	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Add([FromBody] AddInfluencerVideosCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(command).ConfigureAwait(false);
		// return Ok(newId);
		return NoContent();
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Update([FromBody] UpdateInfluencerVideosCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}

	[HttpDelete]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Delete([FromQuery] DeleteInfluencerVideosCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		// return Ok();
		return NoContent();
	}
}
