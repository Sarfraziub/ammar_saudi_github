using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverOrders.Queries.GetTodayDashboard;

public class Handler : IRequestHandler<GetTodayDashboardQuery, GetTodayDashboardVm>
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

	public async Task<GetTodayDashboardVm> Handle(GetTodayDashboardQuery request, CancellationToken cancellationToken)
	{
		var start = _dateTime.StartOfDay(DateTime.Now);
		var end = _dateTime.EndOfDay(DateTime.Now);
		var vm = new GetTodayDashboardUnpaidVm
		{
			ClientOrders = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.DriverId == _currentUserService.UserId
					&& c.ClientOrderStatus == ClientOrderStatuses.Delivered
					&& c.DeliveryTime  >= start
					&& c.DeliveryTime  <= end
				)
				.ProjectTo<GetTodayDashboardUnpaidDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
		foreach (var clientOrder in vm.ClientOrders)
		{
			if (clientOrder.FeeType == FeeTypes.Percentage)
				clientOrder.Fee = clientOrder.Total * clientOrder.Fee;
			else
				clientOrder.Fee = clientOrder.Fee;
		}

		return new GetTodayDashboardVm()
		{
			Trips = vm.ClientOrders.Count,
			Unpaid = vm.ClientOrders.Sum(s=>s.Fee),
			ClientOrders = vm.ClientOrders
		};
	}
}


