using FluentValidation;

namespace Application.SliderItems.Commands.AddSliderItem;

public class Validator : AbstractValidator<AddSliderItemCommand>
{
	public Validator()
	{
		RuleFor(e => e.Name)
			.NotNull().NotEmpty();
		RuleFor(e => e.ImageId)
			.NotNull().NotEmpty();
		RuleFor(e => e.Visible)
			.NotNull().NotEmpty();
		RuleFor(e => e.Order)
			.NotNull().NotEmpty();
	}
}


