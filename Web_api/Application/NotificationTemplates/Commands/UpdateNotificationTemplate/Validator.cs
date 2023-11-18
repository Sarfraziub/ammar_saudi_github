using FluentValidation;

namespace Application.NotificationTemplates.Commands.UpdateNotificationTemplate;

public class Validator : AbstractValidator<UpdateNotificationTemplateCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
		RuleFor(e => e.Title)
			.NotNull().NotEmpty();
		RuleFor(e => e.Body)
			.NotNull().NotEmpty();
	}
}


