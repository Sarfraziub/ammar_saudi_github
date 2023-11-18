using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Queries.GetClientOrdersByClientId;

public class Handler : IRequestHandler<GetClientOrdersByClientIdQuery, GetClientOrdersByClientIdVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetClientOrdersByClientIdVm> Handle(GetClientOrdersByClientIdQuery request,
		CancellationToken cancellationToken)
	{
		var vm = new GetClientOrdersByClientIdVm
		{
			ClientOrders = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.ClientId == request.ClientId
					&& (string.IsNullOrEmpty(request.Number) ||
					    c.Number.Contains(request.Number))
					&& (request.StartDate == null ||
					    c.Created >= request.StartDate)
					&& (request.EndDate == null ||
					    c.Created >= request.EndDate)
					&& (request.ClientOrderStatus == null ||
					    c.ClientOrderStatus == request.ClientOrderStatus)
				)
				.ProjectTo<GetClientOrdersByClientIdDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};

		return vm;
	}
}


