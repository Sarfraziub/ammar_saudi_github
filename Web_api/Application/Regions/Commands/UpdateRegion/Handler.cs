using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.Regions.Commands.UpdateRegion;

public class Handler : IRequestHandler<UpdateRegionCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.Regions.FindAsync(request.Id);
		entity.Name = request.Name;
		entity.ArabicName = request.ArabicName;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


