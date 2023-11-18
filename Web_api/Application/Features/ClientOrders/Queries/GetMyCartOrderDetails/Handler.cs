using Application.Features.Common.Interfaces;
using Application.Interface;
using Application.Interface.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Queries.GetMyCartOrderDetails;

public class Handler : IRequestHandler<GetMyCartOrderDetailsQuery, GetMyCartOrderDetailsVm>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IImageStorageService _imageStorageService;
	private readonly IMapper _mapper;
	private readonly ICurrencyService _currencyService;
	private readonly IRequestContext _requestContext;

	public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService,
		IImageStorageService imageStorageService, ICurrencyService currencyService, IRequestContext requestContext)
	{
		_context = context;
		_mapper = mapper;
		_currentUserService = currentUserService;
		_imageStorageService = imageStorageService;
        _currencyService = currencyService;
        _requestContext = requestContext;
    }

	public async Task<GetMyCartOrderDetailsVm> Handle(GetMyCartOrderDetailsQuery request,
		CancellationToken cancellationToken)
	{
		GetMyCartOrderDetailsVm vm;
		if (_currentUserService.UserId != null)
			vm = new GetMyCartOrderDetailsVm
			{
				Items = await _context.ClientOrderDetails
					.AsNoTracking()
					.Where(c =>
						c.Active == 1
						&& c.ClientOrder.Active == 1
						&& c.ClientOrder.ClientId == _currentUserService.UserId
						&& c.ClientOrder.ClientOrderStatus == ClientOrderStatuses.New
					)
					.ProjectTo<GetMyCartOrderDetailDto>(_mapper.ConfigurationProvider)
					.ToListAsync(cancellationToken)
			};
		else if (request.DeviceId != null)
			vm = new GetMyCartOrderDetailsVm
			{
				Items = await _context.ClientOrderDetails
					.AsNoTracking()
					.Where(c =>
						c.Active == 1
						&& c.ClientOrder.Active == 1
						&& c.ClientOrder.DeviceId == request.DeviceId
						&& c.ClientOrder.ClientOrderStatus == ClientOrderStatuses.New
					)
					.ProjectTo<GetMyCartOrderDetailDto>(_mapper.ConfigurationProvider)
					.ToListAsync(cancellationToken)
			};
		else
		{
			vm = new GetMyCartOrderDetailsVm();
		}

        vm.Items = await _currencyService.ConvertToCurrencyValue(1, _requestContext.Currency, vm.Items);
		if (vm.Items != null)
            foreach (var item in vm.Items)
            {
                item.SaleItemImageUrl = await _imageStorageService.GetImageURL(item.SaleItemImageId);
                item.CurrencyCode = _requestContext.Currency;
            }
        // myCartVm.Total += item.SaleItemPrice;
        // myCartVm.Number = item.Number;
        return vm;
	}
}
