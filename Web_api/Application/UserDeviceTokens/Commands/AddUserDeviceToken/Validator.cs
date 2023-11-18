using FluentValidation;

namespace Application.UserDeviceTokens.Commands.AddUserDeviceToken;

public class Validator : AbstractValidator<AddUserDeviceTokenCommand>
{
	public Validator()
	{
		RuleFor(e => e.DeviceType)
			.NotNull().NotEmpty();
		RuleFor(e => e.RegistrationToken)
			.NotNull().NotEmpty();
	}
}


