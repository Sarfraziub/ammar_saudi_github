using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Application.Payments.Commands.ReceivedIpnResponse;
using Application.Payments.Commands.ReceivePaymentResponse;
using Application.Payments.Commands.RequestPaymentPage;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructures.Payments.Paytaps.Configurations;
using Microsoft.Extensions.Options;
using Application.ClientOrderPayments.Commands.ReceiveClientOrderPaymentPayload;
using Application.Features.ClientOrders.Commands.AddClientOrderLog;
using Domain.DbModel;
using System.Threading;
using Application.Features.Common.Interfaces;
using Application.Features.Common.Models.Payments;
using static IdentityModel.ClaimComparer;
using Persistence.Configurations;
using Microsoft.Extensions.Primitives;

namespace WebApi.Controllers.V1;

public class PaymentsController : BaseController
{
	private readonly IMapper _mapper;
	private readonly IPaymentsService _paymentsService;
    private readonly PaytapsConfigurations _config;

    public PaymentsController(IMapper mapper, IPaymentsService paymentsService, IOptions<PaytapsConfigurations> config)
	{
		_mapper = mapper;
		_paymentsService = paymentsService;
        _config = config.Value;
    }

	[HttpGet]
	[Authorize]
	public ActionResult<PaytapsConfigurationsForMobile> GetPaytapsConfigurations()
	{
		return Ok(_paymentsService.GetPaytapsConfigurations());
	}

	[HttpGet]
	public async Task<ActionResult<RequestPaytapPageResponse>> RequestPaymentPage([FromQuery] RequestPaymentPageCommand command)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState.Values.Select(e => e.Errors).ToList());
		var paymentPage = await Mediator.Send(command).ConfigureAwait(false);
		return Ok(paymentPage);
	}

	[HttpPost]
	public async Task<IActionResult> Webhook([FromForm] PaytabsReturnResponse content, [FromQuery] int? version)
	{
		var command = _mapper.Map<ReceivePaymentResponseCommand>(content);
		await Mediator.Send(command).ConfigureAwait(false);
		Console.WriteLine(content);
        if (command.ResponseStatus == "A")
        {
            return Redirect($"{_config.RedirectUrl}/payment-successful?orderNumber={command.CartId}");
        }
        return Redirect($"{_config.RedirectUrl}/payment?errorCode=1");
    }

    [HttpPost]
	//Instant Payment Notification
	public async Task<ActionResult> Ipn([FromBody] dynamic ipn)
    {
        try
        {
            string strPayload = Convert.ToString(ipn);
            if (!strPayload.Contains("\"response_status\":\"A\""))
            {
                return Ok();
            }
            Request.Headers.TryGetValue("Signature", out StringValues signature);
            var isValid = IsValidRequest(JsonSerializer.Serialize(ipn), signature, _config.Serverkey);
            await Mediator.Send(
                new AddClientOrderLogCommand
                {
                    ClientOrderId = 1,
                    Description = $"Signature: {signature}",
                    ClientOrderActionLogStatus = ClientOrderActionLogStatuses.OrderPaid
                });

            await Mediator.Send(
                new AddClientOrderLogCommand
                {
                    ClientOrderId = 1,
                    Description = $"Ipn: {JsonSerializer.Serialize(ipn)}",
                    ClientOrderActionLogStatus = ClientOrderActionLogStatuses.OrderPaid
                });

            await Mediator.Send(
                new AddClientOrderLogCommand
                {
                    ClientOrderId = 1,
                    Description = $"ServerKey: {_config.Serverkey}",
                    ClientOrderActionLogStatus = ClientOrderActionLogStatuses.OrderPaid
                });

            if (!isValid)
                return NotFound("Signature not valid");

            var command = new ReceiveClientOrderPaymentPayloadCommand
            {
                Id = -1,
                PaymentResponse = Newtonsoft.Json.JsonConvert.DeserializeObject(Convert.ToString(ipn)),
                DeviceToken = string.Empty
            };
            await Mediator.Send(command).ConfigureAwait(false);
            
        }
        catch (Exception ex)
        {
            Mediator.Send(
                new AddClientOrderLogCommand
                {
                    ClientOrderId = 1,
                    Description = $"{ex}",
                    ClientOrderActionLogStatus = ClientOrderActionLogStatuses.OrderPaid
                });
        }
        
        return Ok();
    }

	[HttpGet]
	public IActionResult PaymentResult([FromQuery] string result)
	{
        //return Redirect("https://localhost:44310/swagger/index.html");

        return Ok(result);
    }

    private bool IsValidRequest(dynamic payload, string requestSignature, string serverKey)
    {
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(serverKey)))
        {
            byte[] signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            string signature = BitConverter.ToString(signatureBytes).Replace("-", "").ToLower();

            return string.Equals(signature, requestSignature, StringComparison.OrdinalIgnoreCase);
        }
    }
}
