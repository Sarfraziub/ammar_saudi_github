using FluentValidation;

namespace Application.Features.PromoCodes.Commands.AddPromoCode;

public class Validator : AbstractValidator<AddPromoCodeCommand>
{
	public Validator()
	{
		RuleFor(e => e.Code)
			.NotNull().NotEmpty();
		// RuleFor(e => e.Expiry)
		// 	.NotNull().NotEmpty();
		RuleFor(e => e.Percentage)
			.NotNull().NotEmpty();
	}
}


