using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Drivers.Queries.GetDriverById;

public class Handler : IRequestHandler<GetDriverByIdQuery, GetDriverByIdDto>
{
	private readonly IApplicationDbContext _context;
	private readonly IImageStorageService _imageStorageService;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper, IImageStorageService imageStorageService)
	{
		_context = context;
		_mapper = mapper;
		_imageStorageService = imageStorageService;
	}

	public async Task<GetDriverByIdDto> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
	{
		var entity = await _context.ApplicationUsers
				.AsNoTracking()
				.Where(c => c.Id == request.Id)
				.ProjectTo<GetDriverByIdDto>(_mapper.ConfigurationProvider)
				.SingleAsync(cancellationToken)
			;
		if (entity.IbanImageId != null)
			entity.IbanUrl = await _imageStorageService.GetImageURL(entity.IbanImageId.Value);
		if (entity.ImageId != null)
			entity.ImageUrl = await _imageStorageService.GetImageURL(entity.ImageId.Value);
		if (entity.NationalImageImageId != null)
			entity.NationalIdUrl = await _imageStorageService.GetImageURL(entity.NationalImageImageId.Value);

		return entity;
	}
}
