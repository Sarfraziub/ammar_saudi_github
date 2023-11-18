using FluentValidation;

namespace Application.User.Commands.LockoutAccount;

public class Validator : AbstractValidator<LockoutAccountCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotEmpty()
			.NotNull();
	}
}


