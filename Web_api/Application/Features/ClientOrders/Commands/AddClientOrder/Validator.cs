using FluentValidation;

namespace Application.Features.ClientOrders.Commands.AddClientOrder;

public class Validator : AbstractValidator<AddClientOrderCommand>
{
	public Validator()
	{
		// RuleFor(e => e.LocationId)
		// 	.NotNull().NotEmpty();
	}
}


