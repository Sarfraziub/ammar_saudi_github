using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Interfaces;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.CancelClientOrder;

public class Handler : IRequestHandler<CancelClientOrderCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IMediator _mediator;

	public Handler(IApplicationDbContext context, ICurrentUserService currentUserService, IMediator mediator)
	{
		_context = context;
		_currentUserService = currentUserService;
		_mediator = mediator;
	}

	public async Task<Unit> Handle(CancelClientOrderCommand request, CancellationToken cancellationToken)
	{
		List<ClientOrder> clientOrders;
		if (_currentUserService.UserId != null)
			clientOrders = await _context.ClientOrders.Where(c =>
					c.Active == 1
					&& c.ClientOrderStatus == ClientOrderStatuses.New
					&& c.ClientId == _currentUserService.UserId)
				.Include(i=>i.ClientOrderDetails)
				.ToListAsync(cancellationToken);
		else
			clientOrders = await _context.ClientOrders.Where(c =>
					c.Active == 1
					&& c.ClientOrderStatus == ClientOrderStatuses.New
					&& c.DeviceId == request.DeviceId)
				.Include(i=>i.ClientOrderDetails)
				.ToListAsync(cancellationToken);

		foreach (var clientOrder in clientOrders)
		{
			clientOrder.Active = 0;
			await _context.SaveChangesAsync(cancellationToken);

			foreach (var clientOrderDetail in clientOrder.ClientOrderDetails)
			{
				clientOrderDetail.Active = 0;
			}
			await _context.SaveChangesAsync(cancellationToken);


			await _mediator.Send(
				new AddClientOrderLogCommand
				{
					ClientOrderId = clientOrder.Id, Description = "",
					ClientOrderActionLogStatus = ClientOrderActionLogStatuses.CancelOrder
				}, cancellationToken);

		}

		return Unit.Value;
	}
}
