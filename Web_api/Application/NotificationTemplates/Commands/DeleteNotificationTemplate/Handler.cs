using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.NotificationTemplates.Commands.DeleteNotificationTemplate;

public class Handler : IRequestHandler<DeleteNotificationTemplateCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(DeleteNotificationTemplateCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.NotificationTemplates.FindAsync(request.Id);
		entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


