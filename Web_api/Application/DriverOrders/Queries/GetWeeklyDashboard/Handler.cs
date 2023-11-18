using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverOrders.Queries.GetWeeklyDashboard;

public class Handler : IRequestHandler<GetWeeklyDashboardQuery, GetWeeklyDashboardVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly ICurrentUserService _currentUserService;
	private readonly IDateTime _dateTime;

	public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService,
		IDateTime dateTime)
	{
		_context = context;
		_mapper = mapper;
		_currentUserService = currentUserService;
		_dateTime = dateTime;
	}

	public async Task<GetWeeklyDashboardVm> Handle(GetWeeklyDashboardQuery request, CancellationToken cancellationToken)
	{
		var getWeeklyDashboardVm = new GetWeeklyDashboardVm();
		var sunday = _dateTime.StartOfDay(_dateTime.StartOfWeek());
		for (var i = 0; i < 7; i++)
		{
			var newDate = sunday.AddDays(i);
			var startOfDay = _dateTime.StartOfDay(newDate);
			var endOfDay = _dateTime.EndOfDay(newDate);
			var tempVm = new GetWeeklyDashboardUnpaidVm
			{
				ClientOrders = await _context.ClientOrders
					.AsNoTracking()
					.Where(c =>
						c.Active == 1
						&& c.DriverId == _currentUserService.UserId
						&& c.ClientOrderStatus == ClientOrderStatuses.Delivered
						&& c.DeliveryTime >= startOfDay
						&& c.DeliveryTime <= endOfDay
					)
					// .GroupBy(gb => gb.Created.DayOfWeek)
					.ProjectTo<GetWeeklyDashboardUnpaidDto>(_mapper.ConfigurationProvider)
					.ToListAsync(cancellationToken)
			};

			foreach (var clientOrder in tempVm.ClientOrders)
			{
				if (clientOrder.FeeType == FeeTypes.Percentage)
					clientOrder.Fee = clientOrder.Total * clientOrder.Fee;
				else
					clientOrder.Fee = clientOrder.Fee;
			}

			switch (i)
			{
				case 0:
					getWeeklyDashboardVm.SundayIncome = tempVm.ClientOrders.Sum(s => s.Fee);
					break;
				case 1:
					getWeeklyDashboardVm.MondayIncome = tempVm.ClientOrders.Sum(s => s.Fee);
					break;
				case 2:
					getWeeklyDashboardVm.TuesdayIncome = tempVm.ClientOrders.Sum(s => s.Fee);
					break;
				case 3:
					getWeeklyDashboardVm.WednesdayIncome = tempVm.ClientOrders.Sum(s => s.Fee);
					break;
				case 4:
					getWeeklyDashboardVm.ThursdayIncome = tempVm.ClientOrders.Sum(s => s.Fee);
					break;
				case 5:
					getWeeklyDashboardVm.FridayIncome = tempVm.ClientOrders.Sum(s => s.Fee);
					break;
				case 6:
					getWeeklyDashboardVm.SaturdayIncome = tempVm.ClientOrders.Sum(s => s.Fee);
					break;
			}
		}

		var vm = new GetWeeklyDashboardUnpaidVm
		{
			ClientOrders = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.DriverId == _currentUserService.UserId
					&& c.ClientOrderStatus == ClientOrderStatuses.Delivered
					&& c.DeliveryTime >= _dateTime.StartOfDay(_dateTime.StartOfWeek())
					&& c.DeliveryTime <= _dateTime.EndOfDay(_dateTime.StartOfWeek().AddDays(7))
				)
				.ProjectTo<GetWeeklyDashboardUnpaidDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
		foreach (var clientOrder in vm.ClientOrders)
		{
			if (clientOrder.FeeType == FeeTypes.Percentage)
				clientOrder.Fee = clientOrder.Total * clientOrder.Fee;
			else
				clientOrder.Fee = clientOrder.Fee;
		}

		getWeeklyDashboardVm.Trips = vm.ClientOrders.Count;
		getWeeklyDashboardVm.Unpaid = vm.ClientOrders.Sum(s => s.Fee);
		getWeeklyDashboardVm.ClientOrders = vm.ClientOrders;

		return getWeeklyDashboardVm;

	}
}
