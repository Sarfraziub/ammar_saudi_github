using FluentValidation;

namespace Application.SaleItems.Commands.DeleteSaleItem;

public class Validator : AbstractValidator<DeleteSaleItemCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


