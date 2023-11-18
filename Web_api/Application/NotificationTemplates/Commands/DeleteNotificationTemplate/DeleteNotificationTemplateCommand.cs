using MediatR;

namespace Application.NotificationTemplates.Commands.DeleteNotificationTemplate;

public class DeleteNotificationTemplateCommand : IRequest<Unit>
{
	public long Id { get; set; }
}


