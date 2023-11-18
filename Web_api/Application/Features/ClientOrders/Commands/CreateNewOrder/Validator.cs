using FluentValidation;

namespace Application.Features.ClientOrders.Commands.CreateNewOrder;

public class Validator : AbstractValidator<CreateNewOrderCommand>
{
	public Validator()
	{
		RuleFor(e => e.OrderItems)
			.NotNull().NotEmpty();
		RuleFor(e => e.ServiceType)
			.NotNull();
		// RuleFor(e => e.LocationId)
		// 	.NotNull();
	}
}


