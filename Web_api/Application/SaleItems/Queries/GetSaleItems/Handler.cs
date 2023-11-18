using Application.Features.ClientOrders.Queries.GetMyCartOrderDetails;
using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SaleItems.Queries.GetSaleItems;

public class Handler : IRequestHandler<GetSaleItemsQuery, GetSaleItemsVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IImageStorageService _imageStorageService;
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;

	public Handler(IApplicationDbContext context, IMapper mapper, IImageStorageService imageStorageService,
		IMediator mediator)
	{
		_context = context;
		_mapper = mapper;
		_imageStorageService = imageStorageService;
		_mediator = mediator;
	}

	public async Task<GetSaleItemsVm> Handle(GetSaleItemsQuery request, CancellationToken cancellationToken)
	{
		var myCart = await _mediator.Send(new GetMyCartOrderDetailsQuery()
		{
			DeviceId = request.DeviceId
		}, cancellationToken);

		var saleItems = new GetSaleItemsVm
		{
			SaleItems = await _context.SaleItems
				.AsNoTracking()
				.Where(c => c.Active == 1)
				.ProjectTo<GetSaleItemDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
		foreach (var saleItem in saleItems.SaleItems)
		{
			saleItem.ImageUrl = await _imageStorageService.GetImageURL(saleItem.ImageId);

			var si = myCart?.Items?.Where(i => i.SaleItemId == saleItem.Id).SingleOrDefault();
			saleItem.Quantity = si?.SaleItemQuantity ?? 0;
		}

		return saleItems;
	}
}
