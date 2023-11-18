using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;

namespace Application.InfluencerVideos.Commands.AddInfluencerVideos;

public class AddInfluencerVideosCommand : IRequest<Unit>, IMapFrom<InfluencerVideo>
{
	public long FileId { get; set; }
	public bool Visible { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public ContentTypes ContentType { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<AddInfluencerVideosCommand, InfluencerVideo>()
			;
	}
}
