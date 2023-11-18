using Application.Features.Common.Interfaces;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.DeleteItem;

public class Handler : IRequestHandler<DeleteItemCommand, Unit>
{
	private readonly IApplicationDbContext _context;

	public Handler(IApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.ClientOrderDetails.SingleAsync(s=>s.Id == request.Id && s.ClientOrder.ClientOrderStatus == ClientOrderStatuses.New, cancellationToken: cancellationToken);
		entity.Active = 0;
		// _context.ClientOrderDetails.Remove(entity);
		await _context.SaveChangesAsync(cancellationToken);
		return Unit.Value;
	}
}


