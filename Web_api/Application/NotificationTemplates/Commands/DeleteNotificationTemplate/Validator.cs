using FluentValidation;

namespace Application.NotificationTemplates.Commands.DeleteNotificationTemplate;

public class Validator : AbstractValidator<DeleteNotificationTemplateCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


