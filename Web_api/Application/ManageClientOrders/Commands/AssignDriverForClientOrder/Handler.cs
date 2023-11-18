using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Firebase;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ManageClientOrders.Commands.AssignDriverForClientOrder;

public class Handler : IRequestHandler<AssignDriverForClientOrderCommand, Unit>
{
	private readonly IApplicationDbContext _context;
	private readonly IMediator _mediator;
	private readonly IErrorMessagesService _errorMessagesService;
	private readonly ICurrentUserService _currentUserService;
	private readonly IFirebaseService _firebaseService;
	private readonly IDateTime _dateTime;

	public Handler(
		IApplicationDbContext context
		, IMediator mediator
		, IErrorMessagesService errorMessagesService
		, ICurrentUserService currentUserService
		, IFirebaseService firebaseService
        , IDateTime dateTime)
	{
		_context = context;
		_mediator = mediator;
		_errorMessagesService = errorMessagesService;
		_currentUserService = currentUserService;
		_firebaseService = firebaseService;
        _dateTime = dateTime;
    }

	public async Task<Unit> Handle(AssignDriverForClientOrderCommand request, CancellationToken cancellationToken)
	{
        var driverFee = await _context.DriverFees
            .Where(x => x.IsOffer && x.Active == 1 && x.StartDate <= _dateTime.UtcNow && x.EndDate >= _dateTime.UtcNow)
            .OrderByDescending(x => x.Created)
            .FirstOrDefaultAsync(cancellationToken);
        if (driverFee == null)
        {
            driverFee = await _context.DriverFees
                .Where(w => w.Active == 1 && !w.IsOffer)
                .OrderByDescending(d => d.Created)
                .FirstOrDefaultAsync(cancellationToken);
        }

        if (driverFee == null)
			throw new AppBadRequestException(_errorMessagesService.GetLookupErrorMessageById(2));

		var clientOrder = await _context.ClientOrders
			.SingleOrDefaultAsync(w =>
					w.LocationId != null
					&& w.Id == request.ClientOrderId
					&& w.ClientOrderStatus == ClientOrderStatuses.PaymentReceived
				, cancellationToken);

		clientOrder.DriverId = request.DriverId;
		clientOrder.DriverFeeId = driverFee.Id;
		clientOrder.DriverAssignedOn = _dateTime.UtcNow;
		await _context.SaveChangesAsync(cancellationToken);

		await _mediator.Send(
			new AddClientOrderLogCommand
			{
				ClientOrderId = clientOrder.Id, Description = "",
				ClientOrderActionLogStatus = ClientOrderActionLogStatuses.AssignDriver
			}, cancellationToken);


		var userDeviceToken = await _context
			.UserDeviceTokens
			.Where(w => w.UserId == request.DriverId)
			.OrderByDescending(o => o.Created)
			.Take(1)
			.SingleOrDefaultAsync(cancellationToken);

		if (userDeviceToken != null)
		{
			var firebaseMessage = new FirebaseMessage()
			{
				// Title = "New Order has been assigned to you",
				// Body = $"Order Number: {clientOrder.Number}",
				Title = "تعيين طلب جديد لك",
				Body = $"رقم الطلب: {clientOrder.Number}",
				Data = new Dictionary<string, string>()
				{
					{ "locationUrl", clientOrder.Location != null ? clientOrder.Location.Url : null },
					{ "clientOrderId", clientOrder.Id.ToString() },

				},
			};
			var message = await _firebaseService.SendUser(firebaseMessage, userDeviceToken.RegistrationToken);

		}

		return Unit.Value;
	}
}
