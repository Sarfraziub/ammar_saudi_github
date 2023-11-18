using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.NotificationTemplates.Commands.UpdateNotificationTemplate;

public class Handler : IRequestHandler<UpdateNotificationTemplateCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateNotificationTemplateCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.NotificationTemplates.FindAsync(request.Id);
		entity.Title = request.Title;
		entity.Body = request.Body;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


