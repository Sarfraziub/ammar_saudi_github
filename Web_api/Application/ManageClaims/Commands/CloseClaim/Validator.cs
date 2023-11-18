using FluentValidation;

namespace Application.ManageClaims.Commands.CloseClaim;

public class Validator : AbstractValidator<CloseClaimCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
		RuleFor(e => e.ReceiptId)
			.NotNull().NotEmpty();
	}
}


