using FluentValidation;

namespace Application.DriverOrders.Commands.AcceptClientOrder;

public class Validator : AbstractValidator<AcceptClientOrderCommand>
{
	public Validator()
	{
		RuleFor(e => e.ClientOrderId)
			.NotNull();
	}
}


