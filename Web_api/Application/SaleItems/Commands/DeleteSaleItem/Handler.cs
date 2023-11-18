using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.SaleItems.Commands.DeleteSaleItem;

public class Handler : IRequestHandler<DeleteSaleItemCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(DeleteSaleItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.SaleItems.FindAsync(request.Id);
		if (entity != null) entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


