using MediatR;

namespace Application.Features.ClientOrders.Queries.GetMyCartOrderDetails;

public class GetMyCartOrderDetailsQuery : IRequest<GetMyCartOrderDetailsVm>
{
	public string DeviceId { get; set; }

}


