using FluentValidation;

namespace Application.DriverClaims.Queries.GetClientOrdersByDriverClaimId;

public class Validator : AbstractValidator<GetClientOrdersByDriverClaimIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


