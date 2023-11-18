using FluentValidation;

namespace Application.ManageClientOrders.Commands.UnassignDriverForClientOrder;

public class Validator : AbstractValidator<UnassignDriverForClientOrderCommand>
{
	public Validator()
	{
		RuleFor(e => e.ClientOrderId)
			.NotNull().NotEmpty();
		// RuleFor(e => e.DriverId)
		// 	.NotNull().NotEmpty();
	}
}


