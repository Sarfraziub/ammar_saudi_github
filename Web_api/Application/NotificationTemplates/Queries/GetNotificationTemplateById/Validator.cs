using FluentValidation;

namespace Application.NotificationTemplates.Queries.GetNotificationTemplateById;

public class Validator : AbstractValidator<GetNotificationTemplateByIdQuery>
{
	public Validator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty();
	}
}


