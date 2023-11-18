using FluentValidation;

namespace Application.SaleItemPackages.Commands.UpdatePackage;

public class Validator : AbstractValidator<UpdatePackageCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
		RuleFor(e => e.Name)
			.NotNull().NotEmpty();
		RuleFor(e => e.ArabicName)
			.NotNull().NotEmpty();
		RuleFor(e => e.Visible)
			.NotNull();
	}
}


