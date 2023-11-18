using FluentValidation;

namespace Application.Features.ClientOrders.Commands.ApplyPromoCode;

public class Validator : AbstractValidator<ApplyPromoCodeCommand>
{
	public Validator()
	{
		RuleFor(e => e.PromoCode)
			.NotNull();
	}
}


