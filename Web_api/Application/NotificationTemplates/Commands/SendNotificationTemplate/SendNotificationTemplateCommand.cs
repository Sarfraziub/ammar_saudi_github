using MediatR;

namespace Application.NotificationTemplates.Commands.SendNotificationTemplate;

public class SendNotificationTemplateCommand : IRequest<Unit>
{
	public long Id { get; set; }
}

