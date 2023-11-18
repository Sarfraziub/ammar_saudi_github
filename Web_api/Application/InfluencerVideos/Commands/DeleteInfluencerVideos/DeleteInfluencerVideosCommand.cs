using MediatR;

namespace Application.InfluencerVideos.Commands.DeleteInfluencerVideos;

public class DeleteInfluencerVideosCommand : IRequest<Unit>
{
	public long Id { get; set; }
}
