using Application.Features.ClientOrders.Queries.GetClientOrderById;
using Application.Features.Common.Interfaces;
using Application.Interface.Context;
using Application.Interface;
using AutoMapper;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.ClientOrderPayments.Commands.SendClientOrderPaymentPayload;

public class Handler : IRequestHandler<SendClientOrderPaymentPayloadCommand, long>
{
	private readonly IApplicationDbContext _context;
	private readonly IMapper _mapper;
	private readonly ICurrentUserService _currentUserService;
	private readonly IMediator _mediator;
    private readonly IRequestContext _requestContext;
    private readonly ICurrencyService _currencyService;
    public Handler(IApplicationDbContext context
        , IMapper mapper
        , ICurrentUserService currentUserService
        , IMediator mediator
		, IRequestContext requestContext
		,ICurrencyService currencyService
		)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
        _mediator = mediator;
        _requestContext = requestContext;
        _currencyService = currencyService;
    }

    public async Task<long> Handle(SendClientOrderPaymentPayloadCommand request, CancellationToken cancellationToken)
	{
		var taxOrderSetting = await _context.OrderSettings
			.Where(c => c.Active == 1 && c.OrderSettingType == OrderSettingTypes.Tax)
        .OrderByDescending(o => o.Created)
        .Take(1)
        .SingleAsync(cancellationToken);

        var payload = JsonConvert.DeserializeObject<PaytabsPayloadModel>(request.Payload);
        
        var entity = new ClientOrderPayment()
		{
			ClientOrderId = request.ClientOrderId,
			Payload = request.Payload,
			CartId = payload.PtCartId
		};
		_context.ClientOrderPayments.Add(entity);

		var clientOrder = await _context.ClientOrders
			.Where(x => x.Id == entity.ClientOrderId)
			.Include(i => i.ClientOrderDetails)
			.SingleAsync(cancellationToken);
		clientOrder.ClientId = _currentUserService.UserId;


		var clientOrderDto = await _mediator.Send(new GetClientOrderByIdQuery() { Id = clientOrder.Id }, cancellationToken);
		decimal tax = 0;
		if (clientOrderDto.TotalAfterDiscount != clientOrderDto.Total)
			tax = clientOrderDto.TotalAfterDiscount * taxOrderSetting.Value;
		else
			tax = clientOrderDto.Total * taxOrderSetting.Value;
		clientOrder.Tax = tax;
		clientOrder.Cost = clientOrderDto.TotalAfterDiscount + clientOrderDto.DeliveryFee + tax;

        var toCurrency = await _context.Currencies.FirstOrDefaultAsync(x => x.Code.ToLower() == _requestContext.Currency.ToLower(), cancellationToken);
        if (toCurrency == null)
            throw new Exception("Currency not valid");
        var exchangeRate = await _currencyService.GetExchangeRate(1, toCurrency.Id);

        clientOrder.ChargedCurrencyId = toCurrency.Id;
        clientOrder.ChargedPrice = clientOrder.Cost * exchangeRate;
        clientOrder.ExchangeRate = exchangeRate;

        await _context.SaveChangesAsync(cancellationToken);
		return entity.Id;
	}
}


