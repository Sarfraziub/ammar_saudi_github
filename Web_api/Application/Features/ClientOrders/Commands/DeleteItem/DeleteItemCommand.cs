using MediatR;

namespace Application.Features.ClientOrders.Commands.DeleteItem;

public class DeleteItemCommand : IRequest<Unit>
{
	public long Id { get; set; }
}


