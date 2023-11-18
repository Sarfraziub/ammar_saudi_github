using FluentValidation;

namespace Application.RatesAndFeedbacks.Commands.SetRateAndFeedback;

public class Validator : AbstractValidator<SetRateAndFeedbackCommand>
{
	public Validator()
	{
		RuleFor(e => e.ClientOrderId)
			.NotNull().NotEmpty();
		RuleFor(e => e.Rate)
			.NotNull().NotEmpty();
	}
}


