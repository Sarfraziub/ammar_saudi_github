using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Features.ClientOrders.Commands.AddClientOrder;

public class AddClientOrderCommand : IRequest<long>
{
	public long? LocationId { get; set; }
	public string DeviceId { get; set; }
	public ServiceTypes? ServiceType { get; set; }
    public string PromotionalLinkKey { get; set; }


}


