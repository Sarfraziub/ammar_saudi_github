using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SaleItemPackages.Queries.GetPackages;

public class Handler : IRequestHandler<GetPackagesQuery, GetPackagesVm>
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

	public async Task<GetPackagesVm> Handle(GetPackagesQuery request, CancellationToken cancellationToken)
	{
		var packages = new GetPackagesVm
		{
			Packages = await _context.Packages
				.AsNoTracking()
				.Where(c => c.Active == 1
				            && (request.Visible == null || c.Visible == request.Visible)
				)
				.ProjectTo<GetPackageDto>(_mapper.ConfigurationProvider)
				.OrderBy(t => t.Id)
				.ToListAsync(cancellationToken)
		};
		foreach (var package in packages.Packages)
		{
			if (package.FileId != null)
				package.ImageUrl = await _imageStorageService.GetImageURL(package.FileId.Value);
		}

		return packages;
	}
}
