using FluentValidation;

namespace Application.Features.PromoCodes.Commands.UpdatePromoCode;

public class Validator : AbstractValidator<UpdatePromoCodeCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
		RuleFor(e => e.Percentage)
			.NotNull().NotEmpty();
	}
}


