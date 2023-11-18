using Application.HomePageIcons.Queries.GetHomePageIcons;
using FluentValidation;

namespace Application.HomePageIcons.Commands.UpdateHomePageIcon;

public class Validator : AbstractValidator<UpdateHomePageIconCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
		RuleFor(e => e.Title)
			.NotNull().NotEmpty();
		RuleFor(e => e.ArabicTitle)
			.NotNull().NotEmpty();
		// RuleFor(e => e.FileId)
		// 	.NotNull().NotEmpty();
		RuleFor(e => e.Order)
			.NotNull().NotEmpty();
		RuleFor(e => e.Visible)
			.NotNull();
	}
}


