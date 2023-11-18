using Application.Features.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserNotifications.Commands.ClearNotifications;

public class Handler : IRequestHandler<ClearNotificationsCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;

	public Handler(IApplicationDbContext context, ICurrentUserService currentUserService)
	{
		_context = context;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(ClearNotificationsCommand request, CancellationToken cancellationToken)
	{
		var userNotifications = await _context
			.UserNotifications
			.Where(d =>
				d.Active == 1
				&& d.UserId == _currentUserService.UserId
			)
			.ToListAsync(cancellationToken);
		foreach (var userNotification in userNotifications)
		{
			userNotification.Active = 0;
		}

		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}
