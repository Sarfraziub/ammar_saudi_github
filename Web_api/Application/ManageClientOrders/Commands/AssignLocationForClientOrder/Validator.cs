using FluentValidation;

namespace Application.ManageClientOrders.Commands.AssignLocationForClientOrder;

public class Validator : AbstractValidator<AssignLocationForClientOrderCommand>
{
	public Validator()
	{
		RuleFor(e => e.ClientOrderId)
			.NotNull().NotEmpty();
		RuleFor(e => e.LocationId)
			.NotNull().NotEmpty();
	}
}


