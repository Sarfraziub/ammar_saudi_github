using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.RatesAndFeedbacks.Queries.GetAllFeedbacks;

public class Handler : IRequestHandler<GetAllFeedbacksQuery, GetAllFeedbacksVm>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetAllFeedbacksVm> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
	{
		return new GetAllFeedbacksVm
		{
			Feedbacks = await _context.ClientOrders
				.AsNoTracking()
				.Where(c =>
					c.Active == 1
					&& c.HideFeedback == request.HideFeedback
				)
				.ProjectTo<GetAllFeedbackDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken)
		};
	}
}


