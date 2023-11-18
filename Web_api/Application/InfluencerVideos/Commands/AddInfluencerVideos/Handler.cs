using Application.Features.Common.Interfaces;
using Application.Regions.Commands.AddRegion;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.InfluencerVideos.Commands.AddInfluencerVideos;

public class Handler : IRequestHandler<AddInfluencerVideosCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;

	public Handler(IApplicationDbContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	public async Task<Unit> Handle(AddInfluencerVideosCommand request, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<InfluencerVideo>(request);
		// entity.TeamCount = 0;
		_context.InfluencerVideos.Add(entity);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


