using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.InfluencerVideos.Commands.UpdateInfluencerVideos;

public class UpdateInfluencerVideosCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public long FileId { get; set; }
	public bool Visible { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public ContentTypes ContentType { get; set; }

}
