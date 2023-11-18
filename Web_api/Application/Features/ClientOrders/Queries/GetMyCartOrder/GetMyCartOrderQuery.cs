using MediatR;

namespace Application.Features.ClientOrders.Queries.GetMyCartOrder;

public class GetMyCartOrderQuery : IRequest<GetMyCartOrderDto>
{
	public string DeviceId { get; set; }
}


