using FluentValidation;

namespace Application.Features.ClientOrders.Commands.DeleteItem;

public class Validator : AbstractValidator<DeleteItemCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


