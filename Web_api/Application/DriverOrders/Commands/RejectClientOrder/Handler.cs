using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverOrders.Commands.RejectClientOrder;

public class Handler : IRequestHandler<RejectClientOrderCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IErrorMessagesService _errorMessagesService;
	private readonly IMediator _mediator;

	public Handler(IApplicationDbContext context, ICurrentUserService currentUserService,
		IErrorMessagesService errorMessagesService, IMediator mediator
	)
	{
		_context = context;
		_currentUserService = currentUserService;
		_errorMessagesService = errorMessagesService;
		_mediator = mediator;
	}

	public async Task<Unit> Handle(RejectClientOrderCommand request, CancellationToken cancellationToken)
	{
		var clientOrder = await
			_context.ClientOrders.SingleOrDefaultAsync(s =>
					s.Id == request.ClientOrderId
					&&
					(s.ClientOrderStatus != ClientOrderStatuses.PaymentReceived ||
					 s.ClientOrderStatus != ClientOrderStatuses.WithDriver)
					&& s.DriverId == _currentUserService.UserId
					&& s.Active == 1
				, cancellationToken);

		if (clientOrder == null)
			throw new AppBadRequestException(_errorMessagesService.GetCommonErrorMessageById(4));

		// if (clientOrder.DriverId != null)
		//     throw new AppBadRequestException(_errorMessagesService.GetOrderErrorMessageById(2));

		clientOrder.DriverId = null;
		clientOrder.DriverFee = null;
		clientOrder.ClientOrderStatus = ClientOrderStatuses.PaymentReceived;
        await _context.SaveChangesAsync(cancellationToken);


		await _mediator.Send(
			new AddClientOrderLogCommand
			{
				ClientOrderId = clientOrder.Id, Description = "",
				ClientOrderActionLogStatus = ClientOrderActionLogStatuses.DriverRejectOrder
			}, cancellationToken);


		return Unit.Value;
	}
}
