using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Features.ClientOrders.Commands.AddNewItem;

public class AddNewItemCommand : IRequest<Unit>
{
	public long SaleItemId { get; set; }
	public int Quantity { get; set; }
	public long? LocationId { get; set; }
	public string DeviceId { get; set; }
	public ServiceTypes? ServiceType { get; set; }

}


