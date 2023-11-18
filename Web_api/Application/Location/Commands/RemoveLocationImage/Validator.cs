using FluentValidation;

namespace Application.Location.Commands.RemoveLocationImage;

public class Validator : AbstractValidator<RemoveLocationImageCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


