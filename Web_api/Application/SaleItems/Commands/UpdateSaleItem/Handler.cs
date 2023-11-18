using Application.Features.Common.Interfaces;
using MediatR;

namespace Application.SaleItems.Commands.UpdateSaleItem;

public class Handler : IRequestHandler<UpdateSaleItemCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(UpdateSaleItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.SaleItems.FindAsync(request.Id);
		if (entity == null) return Unit.Value;
		entity.Name = request.Name;
		entity.ArabicName = request.ArabicName;
		entity.Specifications = request.Specifications;
		entity.ArabicSpecifications = request.ArabicSpecifications;
		entity.Price = request.Price;
		entity.ImageId = request.ImageId;
		entity.PackageId = request.PackageId;
        await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


