using FluentValidation;

namespace Application.SaleItems.Commands.UpdateSaleItem;

public class Validator : AbstractValidator<UpdateSaleItemCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
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


