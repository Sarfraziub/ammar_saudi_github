using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.Regions.Queries.GetRegionById;

public class GetRegionByIdDto : IMapFrom<Region>
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string ArabicName { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<Region, GetRegionByIdDto>();
	}
}


