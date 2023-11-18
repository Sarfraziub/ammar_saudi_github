using Newtonsoft.Json;

namespace Application.Features.Common.Models.Payments;

public class CustomerDetails
{
	[JsonProperty("name")] public string Name { get; set; }
	[JsonProperty("email")] public string Email { get; set; }
	[JsonProperty("phone")] public string Phone { get; set; }
	[JsonProperty("street1")] public string Street { get; set; }
	[JsonProperty("city")] public string City { get; set; }
	[JsonProperty("state")] public string State { get; set; }
	[JsonProperty("country")] public string Country { get; set; }
	[JsonProperty("zip")] public string ZipCode { get; set; }
}