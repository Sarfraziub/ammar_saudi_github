using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Regions.Commands.AddRegion;

public class AddRegionCommand : IRequest<Unit>, IMapFrom<Region>
{
	public string Name { get; set; }
	public string ArabicName { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<AddRegionCommand, Region>()
			;
	}
}


