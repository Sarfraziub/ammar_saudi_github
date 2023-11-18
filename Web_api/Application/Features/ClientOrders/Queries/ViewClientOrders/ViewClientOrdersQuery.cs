using Domain.DbModel;
using MediatR;

namespace Application.Features.ClientOrders.Queries.ViewClientOrders;

public class ViewClientOrdersQuery : IRequest<ViewClientOrdersVm>
{
	public string Number { get; set; }
	public ClientOrderStatuses? ClientOrderStatus { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}


