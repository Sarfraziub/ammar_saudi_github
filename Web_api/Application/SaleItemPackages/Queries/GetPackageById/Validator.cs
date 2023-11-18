using FluentValidation;

namespace Application.SaleItemPackages.Queries.GetPackageById;

public class Validator : AbstractValidator<GetPackageByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


