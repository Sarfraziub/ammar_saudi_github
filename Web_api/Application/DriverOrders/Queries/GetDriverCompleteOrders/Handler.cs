using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverOrders.Queries.GetDriverCompleteOrders;

public class Handler : IRequestHandler<GetTodaysDriverCompleteOrdersQuery, GetTodaysDriverCompleteOrdersVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly ICurrentUserService _currentUserService;
	private readonly IDateTime _dateTime;

	public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService, IDateTime dateTime)
	{
		_context = context;
		_mapper = mapper;
		_currentUserService = currentUserService;
		_dateTime = dateTime;
	}

	public async Task<GetTodaysDriverCompleteOrdersVm> Handle(GetTodaysDriverCompleteOrdersQuery request, CancellationToken cancellationToken)
	{
		var vm = new GetTodaysDriverCompleteOrdersVm
		{
			ClientOrders = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.DriverId == _currentUserService.UserId
					&& c.ClientOrderStatus == ClientOrderStatuses.Delivered
					&& c.DeliveryTime  >= _dateTime.StartOfDay(DateTime.Now)
					&& c.DeliveryTime  <= _dateTime.EndOfDay(DateTime.Now)
				)
				.ProjectTo<GetTodaysDriverCompleteOrderDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
		foreach (var clientOrder in vm.ClientOrders)
		{
			if (clientOrder.FeeType == FeeTypes.Percentage)
				clientOrder.Fee = clientOrder.Total * clientOrder.Fee;
			else
				clientOrder.Fee = clientOrder.Fee;
		}
		return vm;
	}
}


