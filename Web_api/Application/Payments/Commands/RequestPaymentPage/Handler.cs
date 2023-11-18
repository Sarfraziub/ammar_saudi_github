using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Application.Features.ClientOrders.Queries.GetClientOrderById;
using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Payments;
using Application.Interface;
using Application.Interface.Context;
using Domain;
using Domain.DbModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Application.Payments.Commands.RequestPaymentPage;

public class Handler : IRequestHandler<RequestPaymentPageCommand, RequestPaytapPageResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IErrorMessagesService _errorMessagesService;
    private readonly IMediator _mediator;
    private readonly IPaymentsService _paymentsService;
    private readonly IRequestContext _requestContext;
    private readonly ICurrencyService _currencyService;

    public Handler(IApplicationDbContext context, ICurrentUserService currentUserService
        , IPaymentsService paymentsService, IMediator mediator, IErrorMessagesService errorMessagesService,
        IRequestContext requestContext, ICurrencyService currencyService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _paymentsService = paymentsService;
        _mediator = mediator;
        _errorMessagesService = errorMessagesService;
        _requestContext = requestContext;
        _currencyService = currencyService;
    }

    public async Task<RequestPaytapPageResponse> Handle(RequestPaymentPageCommand request, CancellationToken cancellationToken)
    {
        var taxOrderSetting = await _context.OrderSettings
            .Where(c => c.Active == 1 && c.OrderSettingType == OrderSettingTypes.Tax)
            .OrderByDescending(o => o.Created)
            .Take(1)
            .SingleAsync(cancellationToken);



        var clientOrder = await _context.ClientOrders
            .Where(x => x.Active == 1 && x.Id == request.ClientOrderId)
            .Where(x=> x.ClientOrderStatus == ClientOrderStatuses.New)
            .Include(i => i.ClientOrderDetails)
            .SingleAsync(cancellationToken);

        if (clientOrder == null) throw new AppBadRequestException("There is no client order");

        clientOrder.ClientId = _currentUserService.UserId;

        var clientOrderDto = await _mediator.Send(new GetClientOrderByIdQuery() { Id = request.ClientOrderId }, cancellationToken);

        if (clientOrderDto.TotalAfterDiscount != clientOrderDto.Total)
            clientOrder.Tax = clientOrderDto.TotalAfterDiscount * taxOrderSetting.Value;
        else
            clientOrder.Tax = clientOrderDto.Total * taxOrderSetting.Value;

        clientOrder.Cost = clientOrderDto.TotalAfterDiscount + clientOrderDto.DeliveryFee + clientOrder.Tax;

        var toCurrency = await _context.Currencies.FirstOrDefaultAsync(x => x.Code.ToLower() == _requestContext.Currency.ToLower(), cancellationToken);
        if (toCurrency == null)
            throw new Exception("Currency not valid");
        var exchangeRate = await _currencyService.GetExchangeRate(1, toCurrency.Id);

        clientOrder.ChargedCurrencyId = toCurrency.Id;
        clientOrder.ChargedPrice = clientOrder.Cost * exchangeRate;
        clientOrder.ExchangeRate = exchangeRate;


        clientOrder.Number = $"{DateTime.Now.ToString("yyyy").Substring(2,2)}" +
                             $"{DateTime.Now.ToString("MM")}" +
                             $"{DateTime.Now.ToString("dd")}" +
                             $"{clientOrder.ClientId}{new Random().Next(100, 100000).ToString().PadLeft(6, '0')}";
        await _context.SaveChangesAsync(cancellationToken);

        var amount = clientOrder.ChargedPrice;
        if (amount <= 0) throw new AppBadRequestException("Amount is 0");

        var cartDescription = $"Payment Try for order number: {clientOrder.Number}";

        await _mediator.Send(
            new AddClientOrderLogCommand
            {
                ClientOrderId = clientOrder.Id,
                Description = "",
                ClientOrderActionLogStatus = ClientOrderActionLogStatuses.RequestPaymentPage
            }, cancellationToken);


        var paymentTry = await _paymentsService.CreatePaymentTry(clientOrder.Id, amount, cartDescription, clientOrder.Number, "", new CustomerDetails());

        paymentTry.PaymentType = request.PaymentType;
        _context.PaymentTries.Add(paymentTry);
        await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Send(
            new AddClientOrderLogCommand
            {
                ClientOrderId = clientOrder.Id,
                Description = "",
                ClientOrderActionLogStatus = ClientOrderActionLogStatuses.RequestPaymentPageReceived
            }, cancellationToken);

        var paytabsPageResponsePage = await _paymentsService.RequestPaymentPage(paymentTry);

        _context.ClientOrderPayments.Add(new ClientOrderPayment()
        {
            ClientOrderId = request.ClientOrderId,
            TransactionReference = paytabsPageResponsePage.TransactionReference,
            Payload = JsonConvert.SerializeObject(_paymentsService.GetPaymentPayload(paymentTry)),
        });

        paymentTry.TransactionReference = paytabsPageResponsePage.TransactionReference;
        paymentTry.Message = paytabsPageResponsePage.Message;
        paymentTry.Trace = paytabsPageResponsePage.Trace;
        paymentTry.CartId = paytabsPageResponsePage.CartId;
        paymentTry.RedirectUrl = paytabsPageResponsePage.RedirectUrl;

        await _context.SaveChangesAsync(cancellationToken);
        return paytabsPageResponsePage;
    }
}


