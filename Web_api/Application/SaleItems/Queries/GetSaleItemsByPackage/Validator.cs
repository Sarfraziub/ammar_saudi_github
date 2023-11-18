using FluentValidation;

namespace Application.SaleItems.Queries.GetSaleItemsByPackage;

public class Validator : AbstractValidator<GetSaleItemsByPackageQuery>
{
	public Validator()
	{
		RuleFor(e => e.PackageId)
			.NotNull().NotEmpty();
	}
}


