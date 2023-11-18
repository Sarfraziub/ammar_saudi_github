using Application.Features.ClientOrders.Commands.AddClientOrder;
using Application.Features.ClientOrders.Queries.GetMyCartOrder;
using Application.Features.Common.Interfaces;
using Application.Interface;
using Application.Interface.Context;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.CreateNewOrder;

public class Handler : IRequestHandler<CreateNewOrderCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;
    private readonly IDateTime _dateTime;
    private readonly IRequestContext _requestContext;
    private readonly ICurrencyService _currencyService;

    public Handler(IApplicationDbContext context, 
        ICurrentUserService currentUserService,
        IMediator mediator, 
        IDateTime dateTime, 
        IRequestContext requestContext, 
        ICurrencyService currencyService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mediator = mediator;
        _dateTime = dateTime;
        _requestContext = requestContext;
        _currencyService = currencyService;
    }

    public async Task<Unit> Handle(CreateNewOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            long clientOrderId = 0;
            ClientOrder clientOrder = null;
            DriverFee driverFee = null;
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

            Domain.DbModel.PromotionalLink promotionalLink = null;
            if (request.PromotionalLinkKey != null)
            {
                if(request.PromotionalLinkKey.Length == 1 || request.PromotionalLinkKey.Length == 2)
                {
                    promotionalLink = await _context.PromotionalLinks
                        .FirstOrDefaultAsync(x => x.Status && x.Active == 1 && x.Id == Convert.ToInt32(request.PromotionalLinkKey), cancellationToken);
                }
                else 
                {
                    promotionalLink = await _context.PromotionalLinks
                        .FirstOrDefaultAsync(x => x.Status && x.Active == 1 && x.UniqueId == request.PromotionalLinkKey, cancellationToken);
                }
            }

            if (clientOrder == null)
            {
                var id = await _mediator.Send(new AddClientOrderCommand
                {
                    DeviceId = request.DeviceId,
                    LocationId = request.LocationId,
                    ServiceType = request.ServiceType,
                    PromotionalLinkKey = promotionalLink?.UniqueId
                }, cancellationToken);
                clientOrderId = id;
            }
            else
            {
                clientOrderId = clientOrder.Id;
            
            }

            clientOrder = await _context
                .ClientOrders
                .Include(i => i.ClientOrderDetails)
                .SingleOrDefaultAsync(w => w.Id == clientOrderId, cancellationToken);

            clientOrder.PromotionalLinkId = promotionalLink?.Id;

            foreach (var clientOrderDetail in clientOrder.ClientOrderDetails)
                clientOrderDetail.Active = 0;

            await _context.SaveChangesAsync(cancellationToken);


            foreach (var orderItem in request.OrderItems)
            {
                var saleItem = await _context.SaleItems.FindAsync(orderItem.SaleItemId);
                //saleItem = await _currencyService.ConvertToCurrencyValue(_requestContext.Currency, 1, saleItem);
                //saleItem = await _currencyService.ConvertToCurrencyValue()
                var clientOrderDetail = new ClientOrderDetail
                {
                    ClientOrderId = clientOrderId,
                    SaleItemId = saleItem.Id,
                    Price = saleItem.Price,
                    Quantity = orderItem.Quantity
                };
                _context.ClientOrderDetails.Add(clientOrderDetail);
                // }

                if (orderItem.Quantity > 0)
                    clientOrderDetail.Quantity = orderItem.Quantity;
                else
                    clientOrderDetail.Active = 0; //_context.ClientOrderDetails.Remove(clientOrderDetail);
            }


            await _context.SaveChangesAsync(cancellationToken);

            driverFee = await _context.DriverFees
                .Where(x => x.IsOffer && x.Active == 1 && x.StartDate <= _dateTime.UtcNow && x.EndDate >= _dateTime.UtcNow)
                .OrderByDescending(x => x.Created)
                .FirstOrDefaultAsync(cancellationToken);
            if (driverFee == null)
            {
                driverFee = await _context.DriverFees
                    .Where(w => w.Active == 1 && !w.IsOffer)
                    .OrderByDescending(d => d.Created)
                    .Take(1)
                    .SingleOrDefaultAsync(cancellationToken);
            }
        
            if (driverFee != null)
            {
                clientOrder.DriverFeeId = driverFee.Id;
                if (driverFee.FeeType == FeeTypes.StaticFee)
                {
                    clientOrder.DeliveryFee = driverFee.Value;
                }
                else
                {
                    var getMyCartOrderDto = await _mediator.Send(new GetMyCartOrderQuery(), cancellationToken);
                    clientOrder.DeliveryFee = getMyCartOrderDto.Total * driverFee.Value;
                }
            }
            else
            {
                clientOrder.DeliveryFee = 0;
                clientOrder.DriverFeeId = null;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            throw;
        }
        return Unit.Value;
    }
}