using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverOrders.Queries.GetUnassignedDriverOrders;

public class Handler : IRequestHandler<GetUnassignedDriverOrdersQuery, GetUnassignedDriverOrdersVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetUnassignedDriverOrdersVm> Handle(GetUnassignedDriverOrdersQuery request,
		CancellationToken cancellationToken)
	{
		var myCartVm = new GetUnassignedDriverOrdersVm
		{
			ClientOrders = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.ClientOrderStatus == ClientOrderStatuses.PaymentReceived
					&& c.DriverId == null
					&& (string.IsNullOrEmpty(request.Number) ||
					    c.Number.Contains(request.Number))
					&& (request.StartDate == null ||
					    c.Created >= request.StartDate)
					&& (request.EndDate == null ||
					    c.Created >= request.EndDate)
					&& c.LocationId != null
				)
				.ProjectTo<GetUnassignedDriverOrdersDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
		return myCartVm;
	}
}
