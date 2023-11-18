using FluentValidation;

namespace Application.InfluencerVideos.Queries.GetInfluencerVideoById;

public class Validator : AbstractValidator<GetInfluencerVideoByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


