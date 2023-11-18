using MediatR;

namespace Application.ManageClientOrders.Commands.AssignDriverForClientOrder;

public class AssignDriverForClientOrderCommand : IRequest<Unit>
{
	public long ClientOrderId { get; set; }
	public long DriverId { get; set; }
}


