using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.InfluencerVideos.Commands.DeleteInfluencerVideos;

public class Handler : IRequestHandler<DeleteInfluencerVideosCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(DeleteInfluencerVideosCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.InfluencerVideos.FindAsync(request.Id);
		entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


