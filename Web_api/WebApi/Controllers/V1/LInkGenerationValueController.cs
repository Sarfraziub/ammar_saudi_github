using Application.Features.LinkGenerationValue.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1
{
    public class LInkGenerationValueController : BaseController
    {

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add([FromBody] AddLinkGenerationValueCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
            var newId = await Mediator.Send(command).ConfigureAwait(false);
            return Ok();
        }

        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> GetById([FromQuery] GetPromotionalLinkByIdQuery query)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        //    var promotionalLink = await Mediator.Send(query);
        //    return Ok(promotionalLink);
        //}

        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> GetAll([FromQuery] GetPromotionalLinksQuery query)
        //{
        //    var promotionalLinks = await Mediator.Send(query);
        //    return Ok(promotionalLinks);
        //}

        //[HttpPut]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> Update([FromQuery] PromotionalLinkUpdateCommand request)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        //    await Mediator.Send(request);
        //    return Ok();
        //}

        //[HttpDelete]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> Delete([FromQuery] PromotionalLinkDeleteCommand request)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        //    await Mediator.Send(request);
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("{name}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> IsUniqueNameExist([FromRoute] string name, [FromQuery] IsUniqueNameExistQuery query)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
        //    query.UniqueName = name;
        //    var isExist = await Mediator.Send(query).ConfigureAwait(false);
        //    return Ok(isExist);
        //}
    }
}
