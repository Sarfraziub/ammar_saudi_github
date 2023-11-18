using FluentValidation;

namespace Application.InfluencerVideos.Commands.DeleteInfluencerVideos;

public class Validator : AbstractValidator<DeleteInfluencerVideosCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


