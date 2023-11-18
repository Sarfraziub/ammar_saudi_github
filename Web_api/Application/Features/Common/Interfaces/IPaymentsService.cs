using Application.Features.Common.Models.Payments;
using Domain.DbModel;

namespace Application.Features.Common.Interfaces;

public interface IPaymentsService
{
	Task<RequestPaytapPageResponse> RequestPaymentPage(PaymentTry paymentTry);

	Task<PaymentTry> CreatePaymentTry(long clientOrderId, decimal amount, string cartDescription, string orderReferenceId,
		string language, CustomerDetails customerDetails);

	PaytapsConfigurationsForMobile GetPaytapsConfigurations();
    PaymentPayload GetPaymentPayload(PaymentTry paymentTry);
}


