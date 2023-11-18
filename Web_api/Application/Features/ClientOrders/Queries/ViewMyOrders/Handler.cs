using Application.Features.Common.Interfaces;
using Application.Interface.Context;
using Application.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Queries.ViewMyOrders;

public class Handler : IRequestHandler<ViewMyOrdersQuery, ViewMyOrdersVm>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IMapper _mapper;
    private readonly ICurrencyService _currencyService;
    private readonly IRequestContext _requestContext;
    public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, ICurrencyService currencyService, IRequestContext requestContext)
	{
		_context = context;
		_mapper = mapper;
		_currentUserService = currentUserService;
        _currencyService = currencyService;
        _requestContext = requestContext;
    }

	public async Task<ViewMyOrdersVm> Handle(ViewMyOrdersQuery request, CancellationToken cancellationToken)
	{
		var myCartVm = new ViewMyOrdersVm
		{
			ClientOrders = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.ClientId == _currentUserService.UserId
					&& (string.IsNullOrEmpty(request.Number) ||
					    c.Number.Contains(request.Number))
					&& (request.StartDate == null ||
					    c.Created >= request.StartDate)
					&& (request.EndDate == null ||
					    c.Created >= request.EndDate)
                    && ((request.ClientOrderStatus == null && c.ClientOrderStatus != ClientOrderStatuses.New) ||
                        c.ClientOrderStatus == request.ClientOrderStatus)
				)
				.OrderByDescending(o=> o.Created)
				.ProjectTo<ViewMyOrderDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
        return myCartVm;
	}
}


