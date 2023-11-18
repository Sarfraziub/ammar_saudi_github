using MediatR;

namespace Application.Features.ClientOrders.Commands.CancelPromoCode;

public class CancelPromoCodeCommand : IRequest<Unit>
{
	// public string PromoCode { get; set; }
	public string DeviceId { get; set; }
}


