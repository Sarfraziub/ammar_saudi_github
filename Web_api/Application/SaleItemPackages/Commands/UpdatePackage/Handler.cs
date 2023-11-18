using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.SaleItemPackages.Commands.UpdatePackage;

public class Handler : IRequestHandler<UpdatePackageCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.Packages.FindAsync(request.Id);
		entity.Name = request.Name;
		entity.ArabicName = request.ArabicName;
		entity.Description = request.Description;
		entity.ArabicDescription = request.ArabicDescription;
		entity.Visible = request.Visible;
		entity.FileId = request.FileId;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


