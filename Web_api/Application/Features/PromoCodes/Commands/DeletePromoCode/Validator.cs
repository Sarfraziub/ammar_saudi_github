using FluentValidation;

namespace Application.Features.PromoCodes.Commands.DeletePromoCode;

public class Validator : AbstractValidator<DeletePromoCodeCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


