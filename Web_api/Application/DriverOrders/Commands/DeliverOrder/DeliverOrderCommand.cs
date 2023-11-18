using System.Security.Principal;
using MediatR;

namespace Application.DriverOrders.Commands.DeliverOrder;

public class DeliverOrderCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public List<long> Images { get; set; }
}
