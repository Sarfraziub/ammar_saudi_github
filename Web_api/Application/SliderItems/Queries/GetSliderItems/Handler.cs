using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SliderItems.Queries.GetSliderItems;

public class Handler : IRequestHandler<GetSliderItemsQuery, GetSliderItemsVm>
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

	public async Task<GetSliderItemsVm> Handle(GetSliderItemsQuery request, CancellationToken cancellationToken)
	{
		var vm = new GetSliderItemsVm
		{
			SliderItems = await _context.SliderItems
				.AsNoTracking()
				.Where(c => c.Active == 1)
				.ProjectTo<GetSliderItemDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
		foreach (var saleItem in vm.SliderItems)
			saleItem.ImageUrl = await _imageStorageService.GetImageURL(saleItem.ImageId);
		return vm;
	}
}


