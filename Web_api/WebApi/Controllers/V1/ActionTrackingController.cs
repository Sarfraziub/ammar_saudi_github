using Application.Features.ActionTracking.Command.AddActionTrackingHistory;
using Application.Features.ActionTracking.Command.AddTracking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1
{
    public class ActionTrackingController : BaseController
    {
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> AddActionTrackingKey([FromBody] AddActionTrackingCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
            await Mediator.Send(command);
            return Ok();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,User,Guest")]
        public async Task<ActionResult> LogAction([FromBody] AddActionTrackingHistoryCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
            await Mediator.Send(command);
            return Ok();
        }
    }
}
