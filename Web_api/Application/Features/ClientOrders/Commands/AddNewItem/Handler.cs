using Application.Features.ClientOrders.Commands.AddClientOrder;
using Application.Features.Common.Interfaces;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.AddNewItem;

public class Handler : IRequestHandler<AddNewItemCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IMediator _mediator;

	public Handler(IApplicationDbContext context, ICurrentUserService currentUserService,
		IMediator mediator)
	{
		_context = context;
		_currentUserService = currentUserService;
		_mediator = mediator;
	}

	public async Task<Unit> Handle(AddNewItemCommand request, CancellationToken cancellationToken)
	{
		long clientOrderId = 0;
		ClientOrder clientOrder;
		if (_currentUserService.UserId != null)
		{
			clientOrder = await _context
				.ClientOrders
				.Where(co =>
						co.ClientId == _currentUserService.UserId
						&& co.ClientOrderStatus == ClientOrderStatuses.New
						&& co.Active == 1
					// && co.LocationId == request.LocationId
				).SingleOrDefaultAsync(cancellationToken);
		}
		else
		{
			clientOrder = await _context
				.ClientOrders
				.Where(co =>
						co.DeviceId == request.DeviceId.Trim()
						&& co.ClientOrderStatus == ClientOrderStatuses.New
						&& co.Active == 1
					// && co.LocationId == request.LocationId
				).SingleOrDefaultAsync(cancellationToken);
		}

		if (clientOrder == null)
		{
			var id = await _mediator.Send(new AddClientOrderCommand
			{
				DeviceId = request.DeviceId,
				LocationId = request.LocationId,
				ServiceType = request.ServiceType
			}, cancellationToken);
			clientOrderId = id;
		}
		else
		{
			clientOrderId = clientOrder.Id;
		}

		var saleItem = await _context.SaleItems.FindAsync(request.SaleItemId);

		var clientOrderDetail = await _context
			.ClientOrderDetails
			.Where(w =>
				w.ClientOrder.ClientId == _currentUserService.UserId
				&& w.ClientOrderId == clientOrderId
				&& w.SaleItemId == request.SaleItemId
				&& w.Active == 1
			).SingleOrDefaultAsync(cancellationToken);

		if (clientOrderDetail == null)
		{
			clientOrderDetail = new ClientOrderDetail
			{
				ClientOrderId = clientOrderId,
				SaleItemId = saleItem.Id,
				Price = saleItem.Price,
				Quantity = request.Quantity
			};
			_context.ClientOrderDetails.Add(clientOrderDetail);
		}

		if (request.Quantity > 0)
			clientOrderDetail.Quantity = request.Quantity;
		else
			clientOrderDetail.Active = 0;  //_context.ClientOrderDetails.Remove(clientOrderDetail);

		await _context.SaveChangesAsync(cancellationToken);

		return Unit.Value;
	}
}
