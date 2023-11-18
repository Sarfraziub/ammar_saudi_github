using FluentValidation;

namespace Application.Drivers.Queries.GetDriverById;

public class Validator : AbstractValidator<GetDriverByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}
