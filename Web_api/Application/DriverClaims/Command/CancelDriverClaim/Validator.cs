using FluentValidation;

namespace Application.DriverClaims.Command.CancelDriverClaim;

public class Validator : AbstractValidator<CancelDriverClaimCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


