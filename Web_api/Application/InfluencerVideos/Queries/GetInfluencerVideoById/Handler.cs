using Application.Features.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.InfluencerVideos.Queries.GetInfluencerVideoById;

public class Handler : IRequestHandler<GetInfluencerVideoByIdQuery, GetInfluencerVideoByIdDto>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<GetInfluencerVideoByIdDto> Handle(GetInfluencerVideoByIdQuery request, CancellationToken cancellationToken)
	{
		return await _context.InfluencerVideos
				.AsNoTracking()
				.Where(c => c.Id == request.Id)
				.ProjectTo<GetInfluencerVideoByIdDto>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync(cancellationToken)
			;
	}
}


