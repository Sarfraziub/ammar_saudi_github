using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.Location.Queries.GetLocationById;

public class GetLocationByIdDto : IMapFrom<Domain.DbModel.Location>
{
	public long Id { get; set; }
	public string Region { get; set; }
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
    public string? DistrictName { get; set; }
    public string? DistrictArabicName { get; set; }
    public int Active { get; set; }
    public List<LocationImagesDto> ImageUrls { get; set; }
	public void Mapping(Profile profile)
	{
		profile.CreateMap<Domain.DbModel.Location, GetLocationByIdDto>()
			.ForMember(d => d.Region, opts => opts.MapFrom(s => s.Region.Name))
			;
	}
}

public class LocationImagesDto
{
	public long Id { get; set; }
	public string Url { get; set; }
}


