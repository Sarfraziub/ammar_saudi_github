using Application.Files.Commands.Upload;
using Application.Files.Queries.GetPhotoUrl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

public class FilesController : BaseController
{
	[HttpPost]
	[Authorize]
	public async Task<IActionResult> Upload(IFormFile file)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var newId = await Mediator.Send(new UploadCommand { File = file }).ConfigureAwait(false);
		return Ok(newId);
	}

	[HttpGet]
	[Authorize]
	public async Task<ActionResult> GetFileUrl([FromQuery] GetFileUrlQuery query)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var item = await Mediator.Send(query).ConfigureAwait(false);
		if (item != null)
			return Ok(item);
		return NotFound();
	}
}


