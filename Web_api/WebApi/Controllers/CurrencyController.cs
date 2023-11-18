using Application.Features.Currency.Query;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    
    public class CurrencyController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetAllCurrency([FromQuery] GetAllCurrencyQuery query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
            return Ok(await Mediator.Send(query));
        }
    }
}
