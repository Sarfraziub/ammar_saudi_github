using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.PromoCodes.Commands.DeletePromoCode;

public class Handler : IRequestHandler<DeletePromoCodeCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(DeletePromoCodeCommand request, CancellationToken cancellationToken)
	{
		var clientOrders = await _context.ClientOrders
			.Where(w => w.Active == 1 && w.PromoCodeId == request.Id)
			.ToListAsync(cancellationToken);
		if (clientOrders.Count != 0) throw new AppBadRequestException("Cant delete");
		var entity = await _context.PromoCodes.FindAsync(request.Id);
		entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


