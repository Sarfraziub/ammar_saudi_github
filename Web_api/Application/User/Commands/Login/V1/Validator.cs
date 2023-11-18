using FluentValidation;

namespace Application.User.Commands.Login.V1;

public class Validator : AbstractValidator<LoginCommand>
{
	public Validator()
	{
		RuleFor(e => e.PhoneNumber)
			.NotEmpty()
			.NotNull();

		RuleFor(e => e.Token)
			.NotEmpty();
			//.Length(6);
		RuleFor(e => e.DeviceId)
			.NotEmpty().NotNull();
	}
}


