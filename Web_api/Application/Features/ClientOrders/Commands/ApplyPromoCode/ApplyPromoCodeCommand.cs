using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Application.Features.ClientOrders.Commands.ApplyPromoCode;

public class ApplyPromoCodeCommand : IRequest<Unit>
{
	public string PromoCode { get; set; }
    [BindNever]
    [JsonIgnore]
    public string? UserAgent { get; set; }
    public string DeviceId { get; set; }

}


