using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Location.Queries.GetLocationById;

public class Handler : IRequestHandler<GetLocationByIdQuery, GetLocationByIdDto>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly IImageStorageService _imageStorageService;

	public Handler(IApplicationDbContext context, IMapper mapper, IImageStorageService imageStorageService)
	{
		_context = context;
		_mapper = mapper;
		_imageStorageService = imageStorageService;
	}

	public async Task<GetLocationByIdDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
	{
		var location = await _context.Locations
			// .AsNoTracking()
			.Where(c => c.Id == request.Id && c.Active == 1)
			.ProjectTo<GetLocationByIdDto>(_mapper.ConfigurationProvider)
			.SingleOrDefaultAsync(cancellationToken);

		var locationImages = await _context
			.LocationImages
			// .AsNoTracking()
			.Where(s => s.LocationId == location.Id && s.Active == 1)
			.ToListAsync(cancellationToken);

		location.ImageUrls = new List<LocationImagesDto>();
		foreach (var locationImage in locationImages)
		{
			location.ImageUrls.Add(new LocationImagesDto()
				{ Id = locationImage.Id, Url = await _imageStorageService.GetImageURL(locationImage.FileId) });
		}

		return location;
		;
	}
}
