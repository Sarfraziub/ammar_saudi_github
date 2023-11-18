
using Application.Features.LinkGeneration.Command;
using Application.Features.LinkGeneration.Query.GetAllLinkGeneration;
using Application.Features.LinkGeneration.Query.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1
{
    public class LinkGenerationController : BaseController
    {

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add([FromBody] AddLinkGenerationCommand command)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
            var newId = await Mediator.Send(command).ConfigureAwait(false);
            return Ok(new {UniqueId = newId});
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetByUniqueId([FromQuery] GetLinkGenerationByUniqueIdQuery query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
            var linkGeneeration = await Mediator.Send(query);
            return Ok(linkGeneeration);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAll([FromQuery] GetAllLinkGenerationQuery query)
        {
            var promotionalLinks = await Mediator.Send(query);
            return Ok(promotionalLinks);
        }

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
