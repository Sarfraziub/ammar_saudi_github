using FluentValidation;

namespace Application.User.Commands.UpdateDriver;

public class Validator : AbstractValidator<UpdateDriverCommand>
{
	public Validator()
	{
		RuleFor(e => e.DriverId)
			.NotEmpty().NotNull();
		RuleFor(e => e.Iban)
			.NotEmpty().NotNull();
		RuleFor(e => e.BankName)
			.NotEmpty().NotNull();
		RuleFor(e => e.NationalId)
			.NotEmpty().NotNull();
	}
}


