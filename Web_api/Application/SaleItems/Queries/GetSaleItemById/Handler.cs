using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SaleItems.Queries.GetSaleItemById;

public class Handler : IRequestHandler<GetSaleItemByIdQuery, GetSaleItemByIdDto>
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

	public async Task<GetSaleItemByIdDto> Handle(GetSaleItemByIdQuery request, CancellationToken cancellationToken)
	{
		var saleItem = await _context.SaleItems
				.AsNoTracking()
				//.Include(i => i.HarvestType)
				.Where(c => c.Id == request.Id && c.Active == 1)
				.ProjectTo<GetSaleItemByIdDto>(_mapper.ConfigurationProvider)
				//.FindAsync(request.Id)
				.SingleOrDefaultAsync(cancellationToken)
			;
		saleItem.ImageUrl = await _imageStorageService.GetImageURL(saleItem.ImageId);
		return saleItem;
	}
}


