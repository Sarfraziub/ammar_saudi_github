using FluentValidation;

namespace Application.NotificationTemplates.Commands.SendNotificationTemplate;

public class Validator : AbstractValidator<SendNotificationTemplateCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


