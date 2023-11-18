using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Location.Commands.UpdateLocation;

public class UpdateLocationCommand : IRequest<Unit>
{
	public long Id { get; set; }
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

}


