using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.Common.Interfaces;
using Application.Interface;
using Application.Interface.Context;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ClientOrders.Commands.AddClientOrder;

public class Handler : IRequestHandler<AddClientOrderCommand, long>
{
	private readonly IApplicationDbContext _context;
	private readonly ICurrentUserService _currentUserService;
	private readonly IMediator _mediator;
	private readonly ICurrencyService _currencyService;
    private readonly IRequestContext _requestContext;


    public Handler(IApplicationDbContext context, 
        ICurrentUserService currentUserService,
		IMediator mediator, 
        ICurrencyService currencyService, 
        IRequestContext requestContext)
	{
		_context = context;
		_currentUserService = currentUserService;
		_mediator = mediator;
        _currencyService = currencyService;
        _requestContext = requestContext;
    }

	public async Task<long> Handle(AddClientOrderCommand request, CancellationToken cancellationToken)
	{
    
        Domain.DbModel.PromotionalLink promotionalLink = null;
        if (request.PromotionalLinkKey != null)
        {
            promotionalLink = await _context.PromotionalLinks
                .FirstOrDefaultAsync(x => x.Status && x.Active == 1 && x.UniqueId == request.PromotionalLinkKey, cancellationToken);
        }

        var clientOrder = new ClientOrder
        {
            ClientId = _currentUserService.UserId,
            Number = $"{DateTime.Now.ToString("yyyy").Substring(2, 2)}" +
                     $"{DateTime.Now.ToString("MM")}" +
                     $"{DateTime.Now.ToString("dd")}" +
                     $"{_currentUserService.UserId}{new Random().Next(100, 100000).ToString().PadLeft(6, '0')}",
            ClientOrderStatus = ClientOrderStatuses.New,
            LocationId = request.LocationId,
            DeviceId = request.DeviceId,
            // DeliveryFee = driverFee.Value,
            ServiceType = request.ServiceType,
            // DriverFeeId = driverFee.Id
            PromotionalLinkId = promotionalLink?.Id,
            ChargedCurrencyId = (await _currencyService.GetCurrencyByCode(_requestContext.Currency)).Id
        };
        _context.ClientOrders.Add(clientOrder);
        await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Send(
            new AddClientOrderLogCommand
            {
                ClientOrderId = clientOrder.Id, Description = "",
                ClientOrderActionLogStatus = ClientOrderActionLogStatuses.CreateOrder
            }, cancellationToken);
        return clientOrder.Id;
        
	}
}


