using FluentValidation;

namespace Application.DriverOrders.Commands.DeliverOrder;

public class Validator : AbstractValidator<DeliverOrderCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull();
		RuleFor(e => e.Images)
			.NotNull().NotEmpty();
	}
}


