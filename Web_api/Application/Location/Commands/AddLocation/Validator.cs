using FluentValidation;

namespace Application.Location.Commands.AddLocation;

public class Validator : AbstractValidator<AddLocationCommand>
{
	public Validator()
	{
		RuleFor(e => e.RegionId)
			.NotNull().NotEmpty();
		RuleFor(e => e.Name)
			.NotNull().NotEmpty();
		RuleFor(e => e.ArabicName)
			.NotNull().NotEmpty();
		RuleFor(e => e.Latitude)
			.NotNull().NotEmpty();
		RuleFor(e => e.Longitude)
			.NotNull().NotEmpty();
	}
}


