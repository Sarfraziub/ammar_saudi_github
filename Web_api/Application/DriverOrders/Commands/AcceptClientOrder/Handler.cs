using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.DriverOrders.Commands.AcceptClientOrder;

public class Handler : IRequestHandler<AcceptClientOrderCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IErrorMessagesService _errorMessagesService;
    private readonly IMediator _mediator;
    private readonly IDateTime _dateTime;

    public Handler(IApplicationDbContext context, ICurrentUserService currentUserService,
        IErrorMessagesService errorMessagesService, IMediator mediator, IDateTime dateTime)
    {
        _context = context;
        _currentUserService = currentUserService;
        _errorMessagesService = errorMessagesService;
        _mediator = mediator;
        _dateTime = dateTime;
    }

    public async Task<Unit> Handle(AcceptClientOrderCommand request, CancellationToken cancellationToken)
    {
        DriverFee driverFee = null;
        var clientOrder = await
            _context.ClientOrders.SingleOrDefaultAsync(s =>
                s.Id == request.ClientOrderId
                && s.ClientOrderStatus == ClientOrderStatuses.PaymentReceived
                && (s.DriverId == null || s.DriverId==_currentUserService.UserId)
                && s.Active == 1
                , cancellationToken);

        if (clientOrder == null)
            throw new AppBadRequestException(_errorMessagesService.GetCommonErrorMessageById(4));

        driverFee = await _context.DriverFees
            .Where(x => x.IsOffer && x.Active == 1 && x.StartDate <= _dateTime.UtcNow && x.EndDate >= _dateTime.UtcNow)
            .OrderByDescending(x => x.Created)
            .FirstOrDefaultAsync(cancellationToken);
        if (driverFee == null)
        {
            driverFee = await _context.DriverFees
                .Where(w => w.Active == 1 && !w.IsOffer)
                .OrderByDescending(d => d.Created)
                .SingleOrDefaultAsync(cancellationToken);
        }

        clientOrder.DriverFeeId = driverFee?.Id;
        clientOrder.DriverId = _currentUserService.UserId;
        clientOrder.ClientOrderStatus = ClientOrderStatuses.WithDriver;

        await _context.SaveChangesAsync(cancellationToken);


        await _mediator.Send(
            new AddClientOrderLogCommand
            {
                ClientOrderId = clientOrder.Id, Description = "",
                ClientOrderActionLogStatus = ClientOrderActionLogStatuses.DriverAcceptOrder
            }, cancellationToken);


        return Unit.Value;
    }
}
