using MediatR;

namespace Application.Payments.Commands.ReceivedIpnResponse;

public class CustomerDetails
{
    public string name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string street1 { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string zip { get; set; }
    public string ip { get; set; }
}

public class PaymentInfo
{
    public string payment_method { get; set; }
    public string card_type { get; set; }
    public string card_scheme { get; set; }
    public string payment_description { get; set; }
    public int expiryMonth { get; set; }
    public int expiryYear { get; set; }
}

public class PaymentResult
{
    public string response_status { get; set; }
    public string response_code { get; set; }
    public string response_message { get; set; }
    public string cvv_result { get; set; }
    public string avs_result { get; set; }
    public DateTime transaction_time { get; set; }
}

public class ReceivedIpnResponseCommand : IRequest<Unit>
{
    public string tran_ref { get; set; }
    public int merchant_id { get; set; }
    public int profile_id { get; set; }
    public string cart_id { get; set; }
    public string cart_description { get; set; }
    public string cart_currency { get; set; }
    public string cart_amount { get; set; }
    public string tran_currency { get; set; }
    public string tran_total { get; set; }
    public string tran_type { get; set; }
    public string tran_class { get; set; }
    public CustomerDetails customer_details { get; set; }
    public ShippingDetails shipping_details { get; set; }
    public PaymentResult payment_result { get; set; }
    public PaymentInfo payment_info { get; set; }
    public string ipn_trace { get; set; }
}

public class ShippingDetails
{
    public string name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string street1 { get; set; }
    public string city { get; set; }
    public string country { get; set; }
    public string zip { get; set; }
}




