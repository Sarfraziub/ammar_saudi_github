using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverOrders.Queries.GetUnpaidDetails;

public class Handler : IRequestHandler<GetUnpaidDetailsQuery, GetUnpaidClaimsQueryVm>
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

	public async Task<GetUnpaidClaimsQueryVm> Handle(GetUnpaidDetailsQuery request, CancellationToken cancellationToken)
	{
		var start = _dateTime.StartOfDay(DateTime.Now);
		var end = _dateTime.EndOfDay(DateTime.Now);
		var vm = new GetUnpaidDetailsVm
		{
			ClientOrders = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.DriverId == _currentUserService.UserId
					&& c.ClientOrderStatus == ClientOrderStatuses.Delivered
					&& c.DriverClaim.DriverClaimStatus != DriverClaimStatuses.Completed
					&& c.DeliveryTime >= start
					&& c.DeliveryTime <= end
				)
				.ProjectTo<GetUnpaidDetailsDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
		var getUnpaidClaimsQueryVm = new GetUnpaidClaimsQueryVm();
		foreach (var clientOrder in vm.ClientOrders)
		{
			if (clientOrder.FeeType == FeeTypes.Percentage)
				getUnpaidClaimsQueryVm.TodayTotal += clientOrder.Total * clientOrder.Fee;
			else
				getUnpaidClaimsQueryVm.TodayTotal += clientOrder.Fee;
		}


		vm = new GetUnpaidDetailsVm
		{
			ClientOrders = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.DriverId == _currentUserService.UserId
					&& c.ClientOrderStatus == ClientOrderStatuses.Delivered
					&& c.DriverClaim.DriverClaimStatus != DriverClaimStatuses.Completed
				)
				.ProjectTo<GetUnpaidDetailsDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
		foreach (var clientOrder in vm.ClientOrders)
		{
			if (clientOrder.FeeType == FeeTypes.Percentage)
				getUnpaidClaimsQueryVm.Total += clientOrder.Total * clientOrder.Fee;
			else
				getUnpaidClaimsQueryVm.Total += clientOrder.Fee;
		}

		return getUnpaidClaimsQueryVm;
	}
}
