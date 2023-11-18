using FluentValidation;

namespace Application.InfluencerVideos.Commands.UpdateInfluencerVideos;

public class Validator : AbstractValidator<UpdateInfluencerVideosCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
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


