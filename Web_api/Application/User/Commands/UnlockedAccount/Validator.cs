using FluentValidation;

namespace Application.User.Commands.UnlockedAccount;

public class Validator : AbstractValidator<UnlockedAccountCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotEmpty()
			.NotNull();
	}
}


