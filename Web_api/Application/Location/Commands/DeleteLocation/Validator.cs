using FluentValidation;

namespace Application.Location.Commands.DeleteLocation;

public class Validator : AbstractValidator<DeleteLocationCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


