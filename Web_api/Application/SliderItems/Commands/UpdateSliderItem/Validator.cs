using FluentValidation;

namespace Application.SliderItems.Commands.UpdateSliderItem;

public class Validator : AbstractValidator<UpdateSliderItemCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
		RuleFor(e => e.Name)
			.NotNull().NotEmpty();
		RuleFor(e => e.ImageId)
			.NotNull().NotEmpty();
		RuleFor(e => e.Visible)
			.NotNull();
		RuleFor(e => e.Order)
			.NotNull().NotEmpty();
	}
}


