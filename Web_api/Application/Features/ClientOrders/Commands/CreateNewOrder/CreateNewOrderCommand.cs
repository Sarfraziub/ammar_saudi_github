using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Features.ClientOrders.Commands.CreateNewOrder;

public class CreateNewOrderCommand : IRequest<Unit>
{
	public List<OrderItem> OrderItems { get; set; }
	public long? LocationId { get; set; }
	public string DeviceId { get; set; }
	public ServiceTypes? ServiceType { get; set; }
	public string? PromotionalLinkKey { get; set; }
}
