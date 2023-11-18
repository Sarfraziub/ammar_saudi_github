using FluentValidation;

namespace Application.Features.ClientOrders.Commands.AddNewItem;

public class Validator : AbstractValidator<AddNewItemCommand>
{
	public Validator()
	{
		RuleFor(e => e.SaleItemId)
			.NotNull().NotEmpty();
		RuleFor(e => e.Quantity)
			.NotNull();
		// RuleFor(e => e.LocationId)
		// 	.NotNull();
	}
}


