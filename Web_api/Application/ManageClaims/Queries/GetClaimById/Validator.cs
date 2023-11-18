using FluentValidation;

namespace Application.ManageClaims.Queries.GetClaimById;

public class Validator : AbstractValidator<GetClaimByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


