using FluentValidation;

namespace Application.SliderItems.Commands.DeleteSliderItem;

public class Validator : AbstractValidator<DeleteSliderItemCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


