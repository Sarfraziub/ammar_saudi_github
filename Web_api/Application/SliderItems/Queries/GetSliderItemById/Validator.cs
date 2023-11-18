using FluentValidation;

namespace Application.SliderItems.Queries.GetSliderItemById;

public class Validator : AbstractValidator<GetSliderItemByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


