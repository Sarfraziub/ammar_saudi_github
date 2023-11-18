using MediatR;

namespace Application.Features.ClientOrders.Queries.GetClientOrderDetailsById;

public class GetClientOrderDetailsByIdQuery : IRequest<GetClientOrderDetailsByIdVm>
{
	public long Id { get; set; }
}


