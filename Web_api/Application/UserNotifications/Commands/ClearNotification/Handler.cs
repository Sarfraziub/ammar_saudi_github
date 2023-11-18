using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.UserNotifications.Commands.ClearNotification;

public class Handler : IRequestHandler<ClearNotificationCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(ClearNotificationCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.UserNotifications.FindAsync(request.Id);
		entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


