using MediatR;

namespace Application.InfluencerVideos.Queries.GetInfluencerVideoById;

public class GetInfluencerVideoByIdQuery: IRequest<GetInfluencerVideoByIdDto>
{
	public long Id { get; set; }
}


