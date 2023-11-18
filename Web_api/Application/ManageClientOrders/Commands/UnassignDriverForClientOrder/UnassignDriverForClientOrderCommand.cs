using MediatR;

namespace Application.ManageClientOrders.Commands.UnassignDriverForClientOrder;

public class UnassignDriverForClientOrderCommand : IRequest<Unit>
{
	public long ClientOrderId { get; set; }
	// public long DriverId { get; set; }
}


