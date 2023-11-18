using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ManageClientOrders.Commands.AssignLocationForClientOrder;

public class Handler : IRequestHandler<AssignLocationForClientOrderCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMediator _mediator;

	public Handler(IApplicationDbContext context, IMediator mediator)
	{
		_context = context;
		_mediator = mediator;
	}

	public async Task<Unit> Handle(AssignLocationForClientOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.ClientOrders
			.SingleOrDefaultAsync(w =>
					w.LocationId == null &&
					w.Id == request.ClientOrderId
					&& w.ClientOrderStatus == ClientOrderStatuses.PaymentReceived
				, cancellationToken);

		entity.LocationId = request.LocationId;
		await _context.SaveChangesAsync(cancellationToken);

		await _mediator.Send(
			new AddClientOrderLogCommand
			{
				ClientOrderId = entity.Id, Description = "",
				ClientOrderActionLogStatus = ClientOrderActionLogStatuses.AssignLocation
			}, cancellationToken);

		return Unit.Value;
	}
}


