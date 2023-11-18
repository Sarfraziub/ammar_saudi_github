using Domain.DbModel;
using MediatR;

namespace Application.Features.ClientOrders.Queries.GetClientOrdersByClientId;

public class GetClientOrdersByClientIdQuery : IRequest<GetClientOrdersByClientIdVm>
{
	public long ClientId { get; set; }
	public string Number { get; set; }
	public ClientOrderStatuses? ClientOrderStatus { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}


