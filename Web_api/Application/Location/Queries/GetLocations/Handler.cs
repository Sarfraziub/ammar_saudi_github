using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Location.Queries.GetLocations;

public class Handler : IRequestHandler<GetLocationsQuery, LocationsVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<LocationsVm> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
	{
		return new LocationsVm
		{
			Locations = await _context.Locations
				.AsNoTracking()
				.Where(c => c.Active == 1
				            && (request.LocationType == null ||
				                c.LocationType == request.LocationType)
				            && (request.RegionId == null ||
				                c.RegionId == request.RegionId)
				)
				.ProjectTo<GetLocationDto>(_mapper.ConfigurationProvider)
				//.OrderBy(t => t.Title)
				.ToListAsync(cancellationToken)
		};
	}
}


