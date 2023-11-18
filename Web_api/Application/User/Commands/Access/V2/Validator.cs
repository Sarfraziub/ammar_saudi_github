using FluentValidation;

namespace Application.User.Commands.Access.V2;

public class Validator : AbstractValidator<AccessCommand>
{
	public Validator()
	{
		RuleFor(e => e.PhoneNumber)
			.NotEmpty()
			.NotNull();
		// .Matches(@"^(009665|9665|\+9665|05|5)(5|0|3|6|4|9|1|8|7)([0-9]{7})$")
		// .Length(10);
		// RuleFor(e => e.Password)
		// 	.NotEmpty()
		// 	;
	}
}

