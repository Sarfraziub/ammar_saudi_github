using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.HomePageIcons.Queries.GetHomePageIcons;

public class Handler : IRequestHandler<GetHomePageIconsQuery, GetHomePageIconsVm>
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

	public async Task<GetHomePageIconsVm> Handle(GetHomePageIconsQuery request, CancellationToken cancellationToken)
	{
		var vm = new GetHomePageIconsVm
		{
			HomePageIcons = await _context.HomePageIcons
				.AsNoTracking()
				.Where(c => c.Active == 1)
				.ProjectTo<GetHomePageIconDto>(_mapper.ConfigurationProvider)
				.OrderBy(t => t.Order)
				.ToListAsync(cancellationToken)
		};

		// foreach (var item in vm.HomePageIcons.Where(item => item.FileId != 0))
		// {
		// 	//item.ImageUrl = await _imageStorageService.GetImageURL(item.FileId);
		// }
		// foreach (var item in vm.HomePageIcons.Where(item => item.FileId != 0))
		// {
		// 	//item.ImageUrl = await _imageStorageService.GetImageURL(item.FileId);
		// 	item.ImageUrl = "";
		// }
		return vm;
	}
}
