using FluentValidation;

namespace Application.InfluencerVideos.Commands.AddInfluencerVideos;

public class Validator : AbstractValidator<AddInfluencerVideosCommand>
{
	public Validator()
	{
		RuleFor(e => e.FileId)
			.NotNull().NotEmpty();
		RuleFor(e => e.Visible)
			.NotNull().NotEmpty();
		RuleFor(e => e.Title)
			.NotNull().NotEmpty();
		RuleFor(e => e.Content)
			.NotNull().NotEmpty();
	}
}


