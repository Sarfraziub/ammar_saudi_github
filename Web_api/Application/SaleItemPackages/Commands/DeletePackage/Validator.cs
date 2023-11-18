using FluentValidation;

namespace Application.SaleItemPackages.Commands.DeletePackage;

public class Validator : AbstractValidator<DeletePackageCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


