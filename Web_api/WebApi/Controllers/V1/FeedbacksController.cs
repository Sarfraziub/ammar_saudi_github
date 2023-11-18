using Application.RatesAndFeedbacks.Commands.ShowHideFeedback;
using Application.RatesAndFeedbacks.Queries.GetActiveFeedbacks;
using Application.RatesAndFeedbacks.Queries.GetAllFeedbacks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class FeedbacksController : BaseController
{
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<GetAllFeedbacksVm>> GetFeedbacks([FromQuery] GetAllFeedbacksQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		return Ok(await Mediator.Send(query).ConfigureAwait(false));
	}

	[HttpGet]
	[Authorize]
	public async Task<ActionResult<GetActiveFeedbacksVm>> GetActiveFeedbacks([FromQuery] GetActiveFeedbacksQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		return Ok(await Mediator.Send(query).ConfigureAwait(false));
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> ShowHideFeedback([FromBody] ShowHideFeedbackCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		await Mediator.Send(command).ConfigureAwait(false);
		return NoContent();
	}
}

