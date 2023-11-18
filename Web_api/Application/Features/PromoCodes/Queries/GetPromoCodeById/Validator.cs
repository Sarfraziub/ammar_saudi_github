using FluentValidation;

namespace Application.Features.PromoCodes.Queries.GetPromoCodeById;

public class Validator : AbstractValidator<GetPromoCodeByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


