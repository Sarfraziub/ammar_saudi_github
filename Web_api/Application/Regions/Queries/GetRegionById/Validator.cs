using FluentValidation;

namespace Application.Regions.Queries.GetRegionById;

public class Validator : AbstractValidator<GetRegionByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


