using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverClaims.Command.CancelDriverClaim;

public class Handler : IRequestHandler<CancelDriverClaimCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMediator _mediator;

	public Handler(IApplicationDbContext context,IMediator mediator)
	{
		_context = context;
		_mediator = mediator;
	}

	public async Task<Unit> Handle(CancelDriverClaimCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.DriverClaims.FindAsync(request.Id);
		entity.Active = 0;
		await _context.SaveChangesAsync(cancellationToken);

		var driverClientOrders = await _context.ClientOrders
			.Where(c =>
				c.Active == 1
				&& c.DriverClaimId == request.Id
			)
			.ToListAsync(cancellationToken);

		foreach (var clientOrder in driverClientOrders)
		{
			clientOrder.DriverClaimId = null;

			await _mediator.Send(
				new AddClientOrderLogCommand
				{
					ClientOrderId = clientOrder.Id, Description = "",
					ClientOrderActionLogStatus = ClientOrderActionLogStatuses.DriverCancelClaim
				}, cancellationToken);
		}
		return Unit.Value;
	}
}


