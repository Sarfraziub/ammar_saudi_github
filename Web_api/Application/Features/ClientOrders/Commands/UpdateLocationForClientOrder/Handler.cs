using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Interfaces;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.UpdateLocationForClientOrder;

public class Handler : IRequestHandler<UpdateLocationForClientOrderCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMediator _mediator;
	private readonly ICurrentUserService _currentUserService;

	public Handler(IApplicationDbContext context, IMediator mediator, ICurrentUserService currentUserService)
	{
		_context = context;
		_mediator = mediator;
		_currentUserService = currentUserService;
	}

	public async Task<Unit> Handle(UpdateLocationForClientOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = await _context.ClientOrders
			.SingleOrDefaultAsync(w =>
					w.ClientId == _currentUserService.UserId
					&& w.ClientOrderStatus == ClientOrderStatuses.New
				, cancellationToken);

		entity.LocationId = request.NewLocationId;
		await _context.SaveChangesAsync(cancellationToken);

		await _mediator.Send(
			new AddClientOrderLogCommand
			{
				ClientOrderId = entity.Id, Description = "",
				ClientOrderActionLogStatus = ClientOrderActionLogStatuses.ChangeLocation
			}, cancellationToken);

		return Unit.Value;
	}
}


