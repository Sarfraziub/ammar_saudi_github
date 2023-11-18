using FluentValidation;

namespace Application.NotificationTemplates.Commands.AddNotificationTemplate;

public class Validator : AbstractValidator<AddNotificationTemplateCommand>
{
	public Validator()
	{
		RuleFor(e => e.Title)
			.NotNull().NotEmpty();
		RuleFor(e => e.Body)
			.NotNull().NotEmpty();
	}
}


