using Application.Features.ClientOrders.Queries.GetMyCartOrderDetails;
using Application.Features.Common.Interfaces;
using Application.Interface;
using Application.Interface.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.SaleItems.Queries.GetSaleItemsByPackage;

public class Handler : IRequestHandler<GetSaleItemsByPackageQuery, GetSaleItemsByPackageVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IImageStorageService _imageStorageService;
	private readonly IMapper _mapper;
	private readonly IMediator _mediator;
	private readonly ICurrencyService _currencyService;
    private readonly IRequestContext _requestContext;


    public Handler(
        IApplicationDbContext context, 
        IMapper mapper, 
        IImageStorageService imageStorageService,
		IMediator mediator, 
        ICurrencyService currencyService, 
        IRequestContext requestContext)
	{
		_context = context;
		_mapper = mapper;
		_imageStorageService = imageStorageService;
		_mediator = mediator;
        _currencyService = currencyService;
        _requestContext = requestContext;
    }

	public async Task<GetSaleItemsByPackageVm> Handle(GetSaleItemsByPackageQuery request, CancellationToken cancellationToken)
	{
		var myCart = await _mediator.Send(new GetMyCartOrderDetailsQuery()
		{
			DeviceId = request.DeviceId
		}, cancellationToken);

		var saleItems = new GetSaleItemsByPackageVm
		{
			SaleItems = await _context.SaleItems
				.AsNoTracking()
				.Where(c => c.Active == 1 && c.PackageId == request.PackageId)
				.ProjectTo<GetSaleItemByPackageDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
        saleItems.SaleItems = await _currencyService.ConvertToCurrencyValue<GetSaleItemByPackageDto>(1,_requestContext.Currency, saleItems.SaleItems);

        foreach (var saleItem in saleItems.SaleItems)
		{
            saleItem.ImageUrl = await _imageStorageService.GetImageURL(saleItem.ImageId);
            var si = myCart?.Items?.Where(i => i.SaleItemId == saleItem.Id).FirstOrDefault();
			saleItem.Quantity = si?.SaleItemQuantity ?? 0;
            saleItem.CurrencyCode = _requestContext.Currency;
        }
        return saleItems;
	}
}
