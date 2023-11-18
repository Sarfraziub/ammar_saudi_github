using Application.Regions.Commands.AddRegion;
using FluentValidation;

namespace Application.SaleItemPackages.Commands.AddPackage;

public class Validator : AbstractValidator<AddRegionCommand>
{
	public Validator()
	{
		RuleFor(e => e.Name)
			.NotNull().NotEmpty();
		RuleFor(e => e.ArabicName)
			.NotNull().NotEmpty();
	}
}


