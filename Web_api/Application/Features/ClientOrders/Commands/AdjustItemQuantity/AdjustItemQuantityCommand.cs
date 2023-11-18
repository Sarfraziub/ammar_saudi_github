using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Features.ClientOrders.Commands.AdjustItemQuantity;

public class AdjustItemQuantityCommand : IRequest<Unit>
{
	public long? Id { get; set; }
	public long? SaleItemId { get; set; }
	public int Quantity { get; set; }
	public string DeviceId { get; set; }
	public ServiceTypes? ServiceType { get; set; }


}


