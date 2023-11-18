using FluentValidation;

namespace Application.Features.ClientOrders.Commands.UpdateLocationForClientOrder;

public class Validator : AbstractValidator<UpdateLocationForClientOrderCommand>
{
	public Validator()
	{

		RuleFor(e => e.NewLocationId)
			.NotNull().NotEmpty();
	}
}


