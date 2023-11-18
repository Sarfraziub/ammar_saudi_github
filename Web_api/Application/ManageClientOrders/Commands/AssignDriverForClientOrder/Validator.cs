using FluentValidation;

namespace Application.ManageClientOrders.Commands.AssignDriverForClientOrder;

public class Validator : AbstractValidator<AssignDriverForClientOrderCommand>
{
	public Validator()
	{
		RuleFor(e => e.ClientOrderId)
			.NotNull().NotEmpty();
		RuleFor(e => e.DriverId)
			.NotNull().NotEmpty();
	}
}


