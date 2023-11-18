using Application.Features.ClientOrders.Commands.AddNewItem;
using Application.Features.Common.Interfaces;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.AdjustItemQuantity;

public class Handler : IRequestHandler<AdjustItemQuantityCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMediator _mediator;

	public Handler(IApplicationDbContext context, IMediator mediator)
	{
		_context = context;
		_mediator = mediator;
	}

	public async Task<Unit> Handle(AdjustItemQuantityCommand request, CancellationToken cancellationToken)
	{
		if (request.Id != null)
		{
			var entity = await _context.ClientOrderDetails.SingleAsync(
				w => w.Id == request.Id && w.ClientOrder.ClientOrderStatus == ClientOrderStatuses.New,
				cancellationToken);
			if (request.Quantity > 0)
				entity.Quantity = request.Quantity;
			else
				_context.ClientOrderDetails.Remove(entity);
			await _context.SaveChangesAsync(cancellationToken);
		}
		else if (request.SaleItemId != null)
		{
			await _mediator.Send(
				new AddNewItemCommand
					{ Quantity = request.Quantity
						, SaleItemId = request.SaleItemId.Value
						, DeviceId = request.DeviceId
						, ServiceType = request.ServiceType
					},
				cancellationToken);
		}

		return Unit.Value;
	}
}
