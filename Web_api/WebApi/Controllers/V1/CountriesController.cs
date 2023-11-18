using Application.Countries.Queries.GetCountries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.V1;

[Authorize]
public class CountriesController : BaseController
{
	[HttpGet]
	public async Task<ActionResult<GetCountriesVm>> GetCountries()
	{
		return Ok(await Mediator.Send(new GetCountriesQuery()).ConfigureAwait(false));
	}
}

