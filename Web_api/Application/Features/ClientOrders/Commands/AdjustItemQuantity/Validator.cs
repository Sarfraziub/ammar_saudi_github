using FluentValidation;

namespace Application.Features.ClientOrders.Commands.AdjustItemQuantity;

public class Validator : AbstractValidator<AdjustItemQuantityCommand>
{
	public Validator()
	{
		// RuleFor(e => e.Id)
		// 	.NotNull().NotEmpty();
		RuleFor(e => e.Quantity)
			.NotNull();
	}
}


