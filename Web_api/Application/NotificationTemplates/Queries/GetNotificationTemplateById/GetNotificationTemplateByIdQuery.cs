using MediatR;

namespace Application.NotificationTemplates.Queries.GetNotificationTemplateById;

public class GetNotificationTemplateByIdQuery : IRequest<GetNotificationTemplateByIdDto>
{
	public long Id { get; set; }
}


