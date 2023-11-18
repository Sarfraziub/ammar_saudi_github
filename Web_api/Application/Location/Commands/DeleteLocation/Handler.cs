using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.Location.Commands.DeleteLocation;

public class Handler : IRequestHandler<DeleteLocationCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.Locations.FindAsync(request.Id);
		entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


