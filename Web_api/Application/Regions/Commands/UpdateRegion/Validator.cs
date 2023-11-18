using FluentValidation;

namespace Application.Regions.Commands.UpdateRegion;

public class Validator : AbstractValidator<UpdateRegionCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
		RuleFor(e => e.Name)
			.NotNull().NotEmpty();
		RuleFor(e => e.ArabicName)
			.NotNull().NotEmpty();
	}
}


