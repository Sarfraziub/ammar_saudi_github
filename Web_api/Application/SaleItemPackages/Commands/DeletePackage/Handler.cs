using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.SaleItemPackages.Commands.DeletePackage;

public class Handler : IRequestHandler<DeletePackageCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(DeletePackageCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.Regions.FindAsync(request.Id);
		entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


