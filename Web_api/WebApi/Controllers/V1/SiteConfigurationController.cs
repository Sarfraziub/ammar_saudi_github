using Application.Features.SiteConfiguration.Command;
using Application.Features.SiteConfiguration.Query.GetSiteSettings;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1
{
    public class SiteConfigurationController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetSiteSettings()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
            var result = await Mediator.Send(new GetSiteSettingsQuery());
            return Ok(result);
        }

        [HttpPut] 
        public async Task<ActionResult> UpdateSiteSettings([FromBody] UpdateSiteSettingsCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
            await Mediator.Send(command);
            return Ok();
        }
    }
}
