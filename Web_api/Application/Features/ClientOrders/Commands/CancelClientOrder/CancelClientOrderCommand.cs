using MediatR;

namespace Application.Features.ClientOrders.Commands.CancelClientOrder;

public class CancelClientOrderCommand : IRequest<Unit>
{
	public string DeviceId { get; set; }
}


