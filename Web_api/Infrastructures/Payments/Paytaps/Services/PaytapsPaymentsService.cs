using Application.Features.Common.Exceptions;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Payments;
using Application.Interface.Context;
using AutoMapper;
using Domain;
using Domain.DbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructures.Payments.Paytaps.Services;

public class PaytapsPaymentsService : IPaymentsService
{
	private readonly IApplicationDbContext _context;
	private readonly IActionContextAccessor _actionAccessor;
    private readonly Configurations.PaytapsConfigurations _configMobile;
    private readonly Configurations.PaytapsWebConfiguration _configWeb;
	private readonly IMapper _mapper;
	private readonly IUrlHelperFactory _urlHelperFactory;
	private readonly IRequestContext _requestContext;

    public PaytapsPaymentsService(
        IOptions<Configurations.PaytapsConfigurations> config,
        IOptions<Configurations.PaytapsWebConfiguration> configWeb,
        IMapper mapper,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionAccessor
,
        IApplicationDbContext context,
        IRequestContext requestContext)
    {
        _mapper = mapper;
        _urlHelperFactory = urlHelperFactory;
        _actionAccessor = actionAccessor;
        _configMobile = config.Value;
        _configWeb = configWeb.Value;
        _context = context;
        _requestContext = requestContext;
    }

    public async Task<RequestPaytapPageResponse> RequestPaymentPage(PaymentTry paymentTry)
	{
        var client = new RestClient(_configWeb.Enndpoint);
        var request = new RestRequest("payment/request");

        request.AddHeader("authorization", _configWeb.Serverkey);

		var paymentPayload = _mapper.Map<PaymentPayload>(paymentTry);

        paymentPayload.PaymentMethods = paymentTry.PaymentType switch
        {
            PaymentTypes.Visa or PaymentTypes.Mastercard or PaymentTypes.Mada => new List<string>()
            {
                "creditcard",
                "amex",
                "mada",
                "urpay",
                "sadad",
                "unionpay",
                "stcpay",
                "valu",
                "fawry",
                "aman",
                "meezaqr"
            },
            PaymentTypes.ApplyPay => new List<string>() { "applepay" },
            _ => paymentPayload.PaymentMethods
        };

        var body = JsonConvert.SerializeObject(paymentPayload);

		request.AddParameter("text/plain", body, ParameterType.RequestBody);
        var response = await client.ExecutePostAsync(request);

        if (!response.IsSuccessful) return new RequestPaytapPageResponse();
		if (response.Content == null) return new RequestPaytapPageResponse();

		var paymentPage = JsonConvert.DeserializeObject<RequestPaytapPageResponse>(response.Content);
		if (paymentPage != null && paymentPage.IsSuccessful()) return paymentPage;

		throw new AppBadRequestException("error while request payment page");
	}

	public async Task<PaymentTry> CreatePaymentTry(long clientOrderId, decimal amount, string cartDescription, string orderReferenceId, string language, CustomerDetails customerDetails)
	{
		if (_actionAccessor.ActionContext == null) return null;

		var urlHelper = _urlHelperFactory.GetUrlHelper(_actionAccessor.ActionContext);
		var callbackUrl = urlHelper.ActionLink("Ipn", "Payments");
		var returnUrl = urlHelper.ActionLink("Webhook", "Payments");
		var currency = await _context.Currencies.FirstOrDefaultAsync(x => x.Code.ToLower() == _requestContext.Currency.ToLower());
		if (currency == null)
			throw new Exception("Currency not valid");

		return new PaymentTry
		{
			ProfileId = _configWeb.ProfileId,
			TransactionClass = _configWeb.TransactionClass,
			TransactionType = _configWeb.TransactionType,
			Callback = callbackUrl,
			Return = returnUrl,
			Currency = currency.Code.ToLower(),
			ClientOrderId = clientOrderId,
			Amount = amount,
			CartDescription = cartDescription,
			OrderReferenceId = orderReferenceId,
			//Language = language,
			//Street = customerDetails.Street,
			//City = customerDetails.City,
			//State = customerDetails.City,
			//Country = customerDetails.Country,
			//ZipCode = customerDetails.ZipCode
		};
	}

	public PaytapsConfigurationsForMobile GetPaytapsConfigurations()
	{
		return new PaytapsConfigurationsForMobile()
		{
			ProfileId = _configMobile.ProfileId,
			Enndpoint = _configMobile.Enndpoint,
			ServerKey = _configMobile.Serverkey,
			ClientKey = _configMobile.ClientKey,
		};
	}

	public PaymentTry CreatePaymentPayload(decimal amount, string cartDescription, string orderReferenceId)
	{
		if (_actionAccessor.ActionContext == null) return null;

		var urlHelper = _urlHelperFactory.GetUrlHelper(_actionAccessor.ActionContext);
		var callbackUrl = urlHelper.ActionLink("Webhook", "Payments");
		var returnUrl = urlHelper.ActionLink("Ipn", "Payments");

        var paymentTry = new PaymentTry();

		var paymentPayload = _mapper.Map<PaymentPayload>(paymentTry);
		paymentPayload.ProfileId = _configMobile.ProfileId;
		paymentPayload.TransactionClass = _configMobile.TransactionClass;
		paymentPayload.TransactionType = _configMobile.TransactionType;
		paymentPayload.Callback = callbackUrl;
		paymentPayload.Return = returnUrl;
		paymentPayload.Currency = _configMobile.Currency;

		return paymentTry;
	}

    public PaymentPayload GetPaymentPayload(PaymentTry paymentTry)
    {
        return _mapper.Map<PaymentPayload>(paymentTry);
    }
}


