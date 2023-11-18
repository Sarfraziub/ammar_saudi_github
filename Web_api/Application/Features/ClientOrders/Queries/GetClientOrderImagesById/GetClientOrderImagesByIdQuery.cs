using MediatR;

namespace Application.Features.ClientOrders.Queries.GetClientOrderImagesById;

public class GetClientOrderImagesByIdQuery :  IRequest<GetClientOrderImagesByIdVm>
{
	public long Id { get; set; }
}
