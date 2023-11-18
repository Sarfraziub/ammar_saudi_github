using FluentValidation;

namespace Application.Location.Queries.GetLocationById;

public class Validator : AbstractValidator<GetLocationByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


