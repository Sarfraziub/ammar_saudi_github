using FluentValidation;

namespace Application.Regions.Commands.AddRegion;

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


