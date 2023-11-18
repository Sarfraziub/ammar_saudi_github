using FluentValidation;

namespace Application.UserNotifications.Commands.ClearNotification;

public class Validator : AbstractValidator<ClearNotificationCommand>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


