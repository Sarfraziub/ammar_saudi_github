using FluentValidation;

namespace Application.DriverOrders.Commands.RejectClientOrder;

public class Validator : AbstractValidator<RejectClientOrderCommand>
{
	public Validator()
	{
		RuleFor(e => e.ClientOrderId)
			.NotNull();
	}
}


