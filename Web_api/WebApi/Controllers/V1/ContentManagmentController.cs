using Application.Features.ContentManagement.Command.UpdateContentManagment;
using Application.Features.ContentManagement.Queries;
using Application.Features.ContentManagement.Queries.GetContentManagment;
using Application.Features.ContentSettings.Commands.UpdateContentSettings;
using Application.Features.DriverFees.Queries.GetDriverFeeSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1
{

    public class ContentManagmentController : BaseController
    {

        [HttpGet] 
        public async Task<ActionResult<GetContentManagementDto>> GetContentManagements()
        {
            return Ok(await Mediator.Send(new GetContentManagementQuery()).ConfigureAwait(false));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult> UpdateContentManagment([FromBody] UpdateContentManagmentCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
            var newId = await Mediator.Send(command).ConfigureAwait(false);
            return NoContent();
        }
    }
}
