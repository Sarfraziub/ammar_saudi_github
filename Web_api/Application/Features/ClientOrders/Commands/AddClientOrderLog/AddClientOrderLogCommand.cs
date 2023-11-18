using Application.Features.Common.Mappings;
using AutoMapper;
using Domain.DbModel;
using MediatR;

namespace Application.Features.ClientOrders.Commands.AddClientOrderLog;

public class AddClientOrderLogCommand : IRequest<Unit>, IMapFrom<ClientOrderLog>
{
	public long ClientOrderId { get; set; }
	public ClientOrderActionLogStatuses ClientOrderActionLogStatus { get; set; }
	public string Description { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<AddClientOrderLogCommand, ClientOrderLog>()
			;
	}
}


