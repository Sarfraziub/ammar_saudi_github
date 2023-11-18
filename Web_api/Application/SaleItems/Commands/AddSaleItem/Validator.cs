using FluentValidation;

namespace Application.SaleItems.Commands.AddSaleItem;

public class Validator : AbstractValidator<AddSaleItemCommand>
{
	public Validator()
	{
		RuleFor(e => e.Name)
			.NotNull().NotEmpty();
		RuleFor(e => e.ArabicName)
			.NotNull().NotEmpty();
		RuleFor(e => e.Specifications)
			.NotNull().NotEmpty();
		RuleFor(e => e.ArabicSpecifications)
			.NotNull().NotEmpty();
		RuleFor(e => e.Price)
			.NotNull().NotEmpty();
	}
}


