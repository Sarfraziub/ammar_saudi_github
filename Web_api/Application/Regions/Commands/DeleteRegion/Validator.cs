using FluentValidation;

namespace Application.Regions.Commands.DeleteRegion;

public class Validator : AbstractValidator<DeleteRegionCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


