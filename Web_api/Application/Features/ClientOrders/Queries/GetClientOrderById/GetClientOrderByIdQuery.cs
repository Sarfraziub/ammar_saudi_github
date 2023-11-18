using MediatR;

namespace Application.Features.ClientOrders.Queries.GetClientOrderById;

public class GetClientOrderByIdQuery : IRequest<GetClientOrderByIdDto>
{
	public long Id { get; set; }
}


