using MediatR;

namespace Application.UserNotifications.Commands.ClearNotification;

public class ClearNotificationCommand : IRequest<Unit>
{
	public long Id { get; set; }
}
