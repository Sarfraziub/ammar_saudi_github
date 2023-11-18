using FluentValidation;

namespace Application.User.Commands.SendPhoneCodeToken.V1;

public class Validator : AbstractValidator<SendPhoneCodeConfirmationCommand>
{
	public Validator()
	{
		RuleFor(v => v.PhoneNumber)
			.NotEmpty()
			.NotNull();

		// RuleFor(v => v.PhoneNumber)
		//     .MaximumLength(10)
		//     .Matches(@"^(009665|9665|\+9665|05|5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$")
		//     .NotEmpty();
	}
}


