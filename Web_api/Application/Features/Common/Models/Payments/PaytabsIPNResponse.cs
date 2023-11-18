#nullable enable
using Newtonsoft.Json;

namespace Application.Features.Common.Models.Payments;

public class PaytabsIPNResponse
{
	[JsonProperty("merchant_id")] public int merchant_id { get; set; }
	[JsonProperty("profile_id")] public int profile_id { get; set; }

	[JsonProperty("tran_ref")] public string? tran_ref { get; set; }
	[JsonProperty("tran_type")] public string? tran_type { get; set; }

	[JsonProperty("cart_amount")] public float cart_amount { get; set; }
	[JsonProperty("cart_currency")] public string? cart_currency { get; set; }
	[JsonProperty("cart_id")] public string? cart_id { get; set; }
	[JsonProperty("cart_description")] public string? cart_description { get; set; }

	[JsonProperty("tran_class")] public string? tran_class { get; set; }
	[JsonProperty("tran_currency")] public string? tran_currency { get; set; }
	[JsonProperty("tran_total")] public float tran_total { get; set; }

	[JsonProperty("payment_result")] public PaymentResult? payment_result { get; set; }

	//public CustomerDetails customer_details;
	//public Payment_Info payment_info;
}


