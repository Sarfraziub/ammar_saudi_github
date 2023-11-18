using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.Regions.Commands.DeleteRegion;

public class Handler : IRequestHandler<DeleteRegionCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(DeleteRegionCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.Regions.FindAsync(request.Id);
		entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


