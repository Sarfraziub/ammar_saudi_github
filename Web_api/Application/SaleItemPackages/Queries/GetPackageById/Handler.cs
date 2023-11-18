using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SaleItemPackages.Queries.GetPackageById;

public class Handler : IRequestHandler<GetPackageByIdQuery, GetPackageByIdDto>
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

	public async Task<GetPackageByIdDto> Handle(GetPackageByIdQuery request, CancellationToken cancellationToken)
	{
		var package = await _context.Packages
				.AsNoTracking()
				.Where(c => c.Id == request.Id && c.Active == 1)
				.ProjectTo<GetPackageByIdDto>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync(cancellationToken)
			;
		package.ImageUrl = await _imageStorageService.GetImageURL(package.FileId);
		return package;
	}
}
