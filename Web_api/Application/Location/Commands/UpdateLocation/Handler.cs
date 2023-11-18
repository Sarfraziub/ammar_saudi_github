using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.Location.Commands.UpdateLocation;

public class Handler : IRequestHandler<UpdateLocationCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.Locations.FindAsync(request.Id);
		entity.Name = request.Name;
		entity.ArabicName = request.ArabicName;
		entity.Latitude = request.Latitude;
		entity.Longitude = request.Longitude;
		entity.RegionId = request.RegionId;
		entity.MoreNeeded = request.MoreNeeded;
		entity.LocationType = request.LocationType;
		entity.Description = request.Description;
		entity.ArabicDescription = request.ArabicDescription;
		entity.Url = request.Url;
		entity.DistrictName = request.DistrictName;
		entity.DistrictArabicName = request.DistrictArabicName;
		entity.Active = request.Active;

		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


