using Application.Payments.Commands.ReceivedIpnResponse;
using MediatR;
using Newtonsoft.Json;

namespace Application.ClientOrderPayments.Commands.ReceiveClientOrderPaymentPayload;

public class ReceiveClientOrderPaymentPayloadCommand : IRequest<Unit>
{
	public long Id { get; set; }
	public dynamic PaymentResponse { get; set; }
	public string DeviceToken { get; set; }
}





// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class BillingDetails
{
    [JsonProperty("addressLine")]
    public string AddressLine { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("countryCode")]
    public string CountryCode { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("zip")]
    public string Zip { get; set; }
}

public class PaytabsPaymentResponseModel
{
    [JsonProperty("tran_total")]
    public string TranTotal { get; set; }

    [JsonProperty("billingDetails")]
    public BillingDetails BillingDetails { get; set; }

    [JsonProperty("paymentResult")]
    public PaymentResult PaymentResult { get; set; }

    [JsonProperty("transactionReference")]
    public string TransactionReference { get; set; }

    [JsonProperty("cartCurrency")]
    public string CartCurrency { get; set; }

    [JsonProperty("cartDescription")]
    public string CartDescription { get; set; }

    [JsonProperty("cartID")]
    public string CartID { get; set; }

    [JsonProperty("tran_currency")]
    public string TranCurrency { get; set; }

    [JsonProperty("payResponseReturn")]
    public string PayResponseReturn { get; set; }

    [JsonProperty("isPending")]
    public bool IsPending { get; set; }

    [JsonProperty("isOnHold")]
    public bool IsOnHold { get; set; }

    [JsonProperty("transactionType")]
    public string TransactionType { get; set; }

    [JsonProperty("isAuthorized")]
    public bool IsAuthorized { get; set; }

    [JsonProperty("trace")]
    public string Trace { get; set; }

    [JsonProperty("cartAmount")]
    public string CartAmount { get; set; }

    [JsonProperty("shippingDetails")]
    public ShippingDetails ShippingDetails { get; set; }

    [JsonProperty("merchantId")]
    public string MerchantId { get; set; }

    [JsonProperty("profileId")]
    public string ProfileId { get; set; }

    [JsonProperty("isProcessed")]
    public bool IsProcessed { get; set; }

    [JsonProperty("serviceId")]
    public string ServiceId { get; set; }

    [JsonProperty("paymentInfo")]
    public PaymentInfo PaymentInfo { get; set; }

    [JsonProperty("isSuccess")]
    public bool IsSuccess { get; set; }
}

public class PaymentInfo
{
    [JsonProperty("cardScheme")]
    public string CardScheme { get; set; }

    [JsonProperty("cardType")]
    public string CardType { get; set; }

    [JsonProperty("expiryMonth")]
    public string ExpiryMonth { get; set; }

    [JsonProperty("expiryYear")]
    public string ExpiryYear { get; set; }

    [JsonProperty("paymentDescription")]
    public string PaymentDescription { get; set; }

    [JsonProperty("payment_method")]
    public string PaymentMethod { get; set; }
}

public class PaymentResult
{
    [JsonProperty("responseCode")]
    public string ResponseCode { get; set; }

    [JsonProperty("responseMessage")]
    public string ResponseMessage { get; set; }

    [JsonProperty("responseStatus")]
    public string ResponseStatus { get; set; }

    [JsonProperty("transactionTime")]
    public DateTime TransactionTime { get; set; }
}

public class Root
{
    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("data")]
    public PaytabsPaymentResponseModel PaytabsPaymentResponse { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }
}

public class ShippingDetails
{
    [JsonProperty("addressLine")]
    public string AddressLine { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("countryCode")]
    public string CountryCode { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("zip")]
    public string Zip { get; set; }
}

