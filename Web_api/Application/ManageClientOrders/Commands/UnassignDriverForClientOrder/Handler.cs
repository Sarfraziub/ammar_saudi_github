using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Interfaces;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ManageClientOrders.Commands.UnassignDriverForClientOrder;

public class Handler : IRequestHandler<UnassignDriverForClientOrderCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IErrorMessagesService _errorMessagesService;

    public Handler(IApplicationDbContext context, IMediator mediator, IErrorMessagesService errorMessagesService)
    {
        _context = context;
        _mediator = mediator;
        _errorMessagesService = errorMessagesService;
    }

    public async Task<Unit> Handle(UnassignDriverForClientOrderCommand request, CancellationToken cancellationToken)
    {
        // var driverFee = await _context.DriverFees.Where(w => w.Active == 1).OrderByDescending(d => d.Created).Take(1)
        //     .SingleOrDefaultAsync(cancellationToken);
        // if (driverFee == null)
        //     throw new AppBadRequestException(_errorMessagesService.GetLookupErrorMessageById(2));

        var entity = await _context.ClientOrders
            .SingleOrDefaultAsync(w =>
                    // w.LocationId == null &&
                    w.Id == request.ClientOrderId
                    && (w.ClientOrderStatus == ClientOrderStatuses.PaymentReceived || w.ClientOrderStatus == ClientOrderStatuses.WithDriver)
                    && w.LocationId != null
                , cancellationToken);

        entity.DriverId = null;
        // entity.DriverFeeId = null;
        await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Send(
            new AddClientOrderLogCommand
            {
                ClientOrderId = entity.Id, Description = "",
                ClientOrderActionLogStatus = ClientOrderActionLogStatuses.UnassignDriver
            }, cancellationToken);

        return Unit.Value;
    }
}
