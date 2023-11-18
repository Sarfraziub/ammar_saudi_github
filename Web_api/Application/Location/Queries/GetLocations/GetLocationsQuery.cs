using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.Location.Queries.GetLocations;

public class GetLocationsQuery : IRequest<LocationsVm>
{
	public long? RegionId { get; set; }
	public LocationTypes? LocationType { get; set; }
}


