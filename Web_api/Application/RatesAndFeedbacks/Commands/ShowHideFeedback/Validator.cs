using FluentValidation;

namespace Application.RatesAndFeedbacks.Commands.ShowHideFeedback;

public class Validator : AbstractValidator<ShowHideFeedbackCommand>
{
	public Validator()
	{
		RuleFor(e => e.ClientOrderId)
			.NotNull().NotEmpty();
		RuleFor(e => e.HideFeedback)
			.NotNull();
	}
}


