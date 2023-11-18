using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Location.Commands.AddLocation;

public class AddLocationCommand : IRequest<Unit>, IMapFrom<Domain.DbModel.Location>
{
	public long RegionId { get; set; }
	public string Name { get; set; }
	public string ArabicName { get; set; }
	public double Longitude { get; set; }
	public double Latitude { get; set; }

	public LocationTypes LocationType { get; set; }
	public bool MoreNeeded { get; set; }
	public string Description { get; set; }
	public string ArabicDescription { get; set; }
	public string Url { get; set; }

	public List<long> Images { get; set; }

    public string? DistrictName { get; set; }
    public string? DistrictArabicName { get; set; }

    public void Mapping(Profile profile)
	{
		profile.CreateMap<AddLocationCommand, Domain.DbModel.Location>()
			;
	}
}


