using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SliderItems.Queries.GetSliderItemById;

public class Handler : IRequestHandler<GetSliderItemByIdQuery, GetSliderItemByIdDto>
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

	public async Task<GetSliderItemByIdDto> Handle(GetSliderItemByIdQuery request, CancellationToken cancellationToken)
	{
		var dto = await _context.SliderItems
				.AsNoTracking()
				.Where(c => c.Id == request.Id && c.Active == 1)
				.ProjectTo<GetSliderItemByIdDto>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync(cancellationToken)
			;
		dto.ImageUrl = await _imageStorageService.GetImageURL(dto.ImageId);
		return dto;
	}
}


