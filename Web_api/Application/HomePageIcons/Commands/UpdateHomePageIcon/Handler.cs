using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.HomePageIcons.Commands.UpdateHomePageIcon;

public class Handler : IRequestHandler<UpdateHomePageIconCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateHomePageIconCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.HomePageIcons.FindAsync(request.Id);
		entity.Title = request.Title;
		entity.ArabicTitle = request.ArabicTitle;
		// entity.FileId = request.FileId;
		entity.Visible = request.Visible;
		entity.Order = request.Order;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


