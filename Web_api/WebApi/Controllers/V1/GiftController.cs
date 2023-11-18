using Application.Features.Gift.Command.AddGift;
using Application.Features.Gift.Command.DeleteGift;
using Application.Features.Gift.Query.GetGiftByClientOrderId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1
{
    public class GiftController : BaseController
    {

        [HttpPost]
        [Authorize(Roles = "User, Admin, Guest")]
        public async Task<ActionResult> Add([FromBody] AddGiftCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
            var giftId = await Mediator.Send(command);
            return Ok(new {GiftId = giftId});
        }

        [HttpDelete]
        [Authorize(Roles = "User, Admin, Guest")]
        public async Task<ActionResult> Delete([FromQuery] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());

            await Mediator.Send(new DeleteGiftCommand { Id = id });
            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = "User, Admin, Guest")]
        public async Task<ActionResult> GetGiftByClientOrderId([FromQuery] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());

            var result = await Mediator.Send(new GetGiftByIdQuery() { Id = id });
            return Ok(result);
        }
    }
}

