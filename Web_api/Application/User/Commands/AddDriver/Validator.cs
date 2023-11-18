using FluentValidation;

namespace Application.User.Commands.AddDriver;

public class Validator : AbstractValidator<AddDriverCommand>
{
	public Validator()
	{
		RuleFor(e => e.PhoneNumber)
			.NotEmpty();
	}
}


