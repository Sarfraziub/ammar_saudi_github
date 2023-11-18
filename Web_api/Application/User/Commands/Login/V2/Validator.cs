using FluentValidation;

namespace Application.User.Commands.Login.V2;

public class Validator : AbstractValidator<LoginCommand>
{
	public Validator()
	{
		RuleFor(e => e.PhoneNumber)
			.NotEmpty()
			.NotNull();

		RuleFor(e => e.Token)
			.NotEmpty()
			.Length(4);
		RuleFor(e => e.DeviceId)
			.NotEmpty().NotNull();
	}
}


