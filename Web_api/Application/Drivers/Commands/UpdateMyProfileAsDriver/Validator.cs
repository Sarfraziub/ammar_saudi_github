using FluentValidation;

namespace Application.Drivers.Commands.UpdateMyProfileAsDriver;

public class Validator : AbstractValidator<UpdateMyProfileAsDriverCommand>
{
	public Validator()
	{
		RuleFor(e => e.Name)
			.NotEmpty().NotNull();
		RuleFor(e => e.Iban)
			.NotEmpty().NotNull();
		RuleFor(e => e.BankName)
			.NotEmpty().NotNull();
		RuleFor(e => e.NationalId)
			.NotEmpty().NotNull();
		RuleFor(e => e.NationalImageImageId)
			.NotEmpty().NotNull();
		RuleFor(e => e.IbanImageId)
			.NotEmpty().NotNull();
	}
}
