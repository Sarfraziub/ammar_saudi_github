using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverClaims.Queries.GetUnclaimedClientOrders;

public class Handler : IRequestHandler<GetUnclaimedClientOrdersQuery, GetUnclaimedClientOrdersVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly ICurrentUserService _currentUserService;

	public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
	{
		_context = context;
		_mapper = mapper;
		_currentUserService = currentUserService;
	}

	public async Task<GetUnclaimedClientOrdersVm> Handle(GetUnclaimedClientOrdersQuery request, CancellationToken cancellationToken)
	{
		var vm = new GetUnclaimedClientOrdersVm
		{
			ClientOrders = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.DriverId == _currentUserService.UserId
					&& c.ClientOrderStatus == ClientOrderStatuses.Delivered
					&& c.DriverClaimId == null
				)
				.ProjectTo<GetUnclaimedClientOrderDto>(_mapper.ConfigurationProvider)
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


