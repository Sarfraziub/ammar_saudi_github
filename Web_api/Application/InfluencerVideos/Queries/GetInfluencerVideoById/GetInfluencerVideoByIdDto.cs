using Application.Features.Common.Mappings;
using AutoMapper;
using Domain;
using Domain.DbModel;

namespace Application.InfluencerVideos.Queries.GetInfluencerVideoById;

public class GetInfluencerVideoByIdDto: IMapFrom<InfluencerVideo>
{
	public long Id { get; set; }
	public long FileId { get; set; }
	public bool Visible { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public string ImageUrl { get; set; }
	public ContentTypes ContentType { get; set; }

	public void Mapping(Profile profile)
	{
		profile.CreateMap<InfluencerVideo, GetInfluencerVideoByIdDto>();
	}
}
