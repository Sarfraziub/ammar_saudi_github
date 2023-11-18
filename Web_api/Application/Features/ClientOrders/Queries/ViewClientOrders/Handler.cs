using Application.Features.Common.Interfaces;
using Application.Interface;
using Application.Interface.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Queries.ViewClientOrders;

public class Handler : IRequestHandler<ViewClientOrdersQuery, ViewClientOrdersVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly ICurrencyService _currencyService;
	private readonly IRequestContext _requestContext;

	public Handler(IApplicationDbContext context, 
        IMapper mapper, 
        ICurrencyService currencyService, 
        IRequestContext requestContext)
	{
		_context = context;
		_mapper = mapper;
        _currencyService = currencyService;
        _requestContext = requestContext;
    }

	public async Task<ViewClientOrdersVm> Handle(ViewClientOrdersQuery request, CancellationToken cancellationToken)
	{
		var myCartVm = new ViewClientOrdersVm
		{
			ClientOrders = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& (string.IsNullOrEmpty(request.Number) ||
					    c.Number.Contains(request.Number))
					&& (request.StartDate == null ||
					    c.Created >= request.StartDate)
					&& (request.EndDate == null ||
					    c.Created >= request.EndDate)
					&& (request.ClientOrderStatus == null ||
					    c.ClientOrderStatus == request.ClientOrderStatus)
				)
				.ProjectTo<ViewClientOrderDto>(_mapper.ConfigurationProvider)
				.OrderByDescending(o=>o.Created)
				.ToListAsync(cancellationToken)
		};

        myCartVm.ClientOrders = await _currencyService.ConvertToCurrencyValue(1, _requestContext.Currency, myCartVm.ClientOrders);


        // foreach (var item in myCartVm.Items)
        // {
        // 	item.SaleItemImageUrl = await _imageStorageService.GetImageURL(item.SaleItemImageId);
        // 	myCartVm.Total += item.SaleItemPrice;
        // 	myCartVm.Number = item.Number;
        // }

        return myCartVm;
	}
}


