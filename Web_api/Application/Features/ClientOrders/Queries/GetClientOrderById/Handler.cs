using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Queries.GetClientOrderById;

public class Handler : IRequestHandler<GetClientOrderByIdQuery, GetClientOrderByIdDto>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
	{
		_context = context;
		_mapper = mapper;
		_currentUserService = currentUserService;
	}

	public async Task<GetClientOrderByIdDto> Handle(GetClientOrderByIdQuery request,
		CancellationToken cancellationToken)
	{
		var order = await _context.ClientOrders
			.AsNoTracking()
			.Where(c =>
				c.Active == 1
				&& c.Id == request.Id
			)
			.ProjectTo<GetClientOrderByIdDto>(_mapper.ConfigurationProvider)
			.SingleOrDefaultAsync(cancellationToken);

		if (order.FeeType != null)
		{
			if (order.FeeType == FeeTypes.Percentage)
				order.Fee = order.Total * order.FeeValue;
			else
				order.Fee = order.Fee;
		}


		return order;
	}
}
