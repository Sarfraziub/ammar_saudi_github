using FluentValidation;

namespace Application.Features.DriverFees.Commands.AddDriverFee;

public class Validator : AbstractValidator<AddDriverFeeCommand>
{
	public Validator()
	{
		RuleFor(e => e.Value)
			.NotNull();
		RuleFor(e => e.FeeType)
			.NotNull().NotEmpty();
	}
}


