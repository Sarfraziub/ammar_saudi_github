using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RatesAndFeedbacks.Queries.GetActiveFeedbacks;

public class Handler : IRequestHandler<GetActiveFeedbacksQuery, GetActiveFeedbacksVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetActiveFeedbacksVm> Handle(GetActiveFeedbacksQuery request, CancellationToken cancellationToken)
	{
		return new GetActiveFeedbacksVm
		{
			Feedbacks = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.HideFeedback == false
				)
				.ProjectTo<GetActiveFeedbackDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
	}
}


