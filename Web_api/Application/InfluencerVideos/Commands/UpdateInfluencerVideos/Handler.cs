using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.InfluencerVideos.Commands.UpdateInfluencerVideos;

public class Handler : IRequestHandler<UpdateInfluencerVideosCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateInfluencerVideosCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.InfluencerVideos.FindAsync(request.Id);
		entity.FileId = request.FileId;
		entity.Content = request.Content;
		entity.Title = request.Title;
		entity.Visible = request.Visible;
		entity.ContentType = request.ContentType;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


