using Newtonsoft.Json;

namespace Application.Features.Common.Models.Payments;

public class RequestPaytapPageResponse
{
	[JsonProperty("tran_ref")] public string TransactionReference { get; set; }
	[JsonProperty("cart_id")] public string CartId { get; set; }
	[JsonProperty("redirect_url")] public string RedirectUrl { get; set; }
	[JsonProperty("trace")] public string Trace { get; set; }
	[JsonProperty("message")] public string Message { get; set; }

	public bool IsSuccessful()
	{
		return RedirectUrl != null;
	}
}


