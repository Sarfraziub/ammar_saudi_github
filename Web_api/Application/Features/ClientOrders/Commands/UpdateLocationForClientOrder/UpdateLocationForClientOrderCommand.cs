using MediatR;

namespace Application.Features.ClientOrders.Commands.UpdateLocationForClientOrder;

public class UpdateLocationForClientOrderCommand : IRequest<Unit>
{
	// public long ClientOrderId { get; set; }
	public long NewLocationId { get; set; }
}


