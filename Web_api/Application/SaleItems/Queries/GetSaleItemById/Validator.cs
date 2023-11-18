using FluentValidation;

namespace Application.SaleItems.Queries.GetSaleItemById;

public class Validator : AbstractValidator<GetSaleItemByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


