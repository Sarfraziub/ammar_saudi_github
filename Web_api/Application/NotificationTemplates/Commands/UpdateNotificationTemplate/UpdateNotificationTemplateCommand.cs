using MediatR;

namespace Application.NotificationTemplates.Commands.UpdateNotificationTemplate;

public class UpdateNotificationTemplateCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public string Title { get; set; }
	public string Body { get; set; }
}


