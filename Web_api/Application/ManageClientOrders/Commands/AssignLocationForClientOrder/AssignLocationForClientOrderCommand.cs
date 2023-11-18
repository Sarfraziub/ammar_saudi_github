using MediatR;

namespace Application.ManageClientOrders.Commands.AssignLocationForClientOrder;

public class AssignLocationForClientOrderCommand : IRequest<Unit>
{
	public long ClientOrderId { get; set; }
	public long LocationId { get; set; }
}


