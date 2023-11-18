using Application.Features.Common.Mappings;
using AutoMapper;
using Domain.DbModel;

namespace Application.Features.ClientOrders.Queries.GetClientOrderImagesById;

public class GetClientOrderImagesByIdDto : IMapFrom<ClientOrderDeliverImage>
{
	public long FileId { get; set; }
	public string Url { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<ClientOrderDeliverImage, GetClientOrderImagesByIdDto>();
	}
}
