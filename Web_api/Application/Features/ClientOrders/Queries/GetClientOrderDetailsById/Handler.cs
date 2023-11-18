using Application.Features.Common.Interfaces;
using Application.Interface;
using Application.Interface.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Queries.GetClientOrderDetailsById;

public class Handler : IRequestHandler<GetClientOrderDetailsByIdQuery, GetClientOrderDetailsByIdVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IImageStorageService _imageStorageService;
	private readonly IMapper _mapper;
    private readonly ICurrencyService _currencyService;
    private readonly IRequestContext _requestContext;

    public Handler(IApplicationDbContext context, 
        IMapper mapper,
		IImageStorageService imageStorageService, 
        ICurrencyService currencyService, 
        IRequestContext requestContext)
	{
		_context = context;
		_mapper = mapper;
		_imageStorageService = imageStorageService;
        _currencyService = currencyService;
        _requestContext = requestContext;
    }

	public async Task<GetClientOrderDetailsByIdVm> Handle(GetClientOrderDetailsByIdQuery request,
		CancellationToken cancellationToken)
	{
		var vm = new GetClientOrderDetailsByIdVm
		{
			ClientOrderDetails = await _context.ClientOrderDetails
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.ClientOrder.Active == 1
					&& c.ClientOrderId == request.Id
				)
				.ProjectTo<GetClientOrderDetailsByIdDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};

        vm.ClientOrderDetails = await _currencyService.ConvertToCurrencyValue(1, _requestContext.Currency, vm.ClientOrderDetails);
        foreach (var item in vm.ClientOrderDetails)
		{
			item.SaleItemImageUrl = await _imageStorageService.GetImageURL(item.SaleItemImageId);
			vm.Total += item.SaleItemPrice;
			vm.Number = item.Number;
		}

		return vm;
	}
}


